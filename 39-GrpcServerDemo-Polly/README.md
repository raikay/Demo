# 39丨Polly：用失败重试机制提升服务可用性

**Polly 的能力**
- 失败重试
- 服务熔断
- 超时处理
- 舱壁隔离//限流
- 缓存策略
- 失败降级//不可用时返回一个友好结果
- 组合策略

**Polly 使用步骤**
- 定义要处理的异常类型或返回值
- 定义要处理动作（重试、熔断、降级响应等）
- 使用定义的策略来执行代码

**适合失败重试的场景**
- 服务“失败”是短暂的，可自愈的
- 服务是幂等的，重复调用不会有副作用

**场景举例**
- 网络闪断
- 部分服务节点异常

**最佳实践**
- 设置失败重试次数
- 设置带有步长策略的失败等待间隔
- 设置降级响应
- 设置断路器

polly针对httpclientfactry提供了一个瞬时错误策略，可以基于这个策略设置一些重试、处理的逻辑。  

Nuget:
```
Grpc.AspNetCore
Microsoft.Extensions.DependencyInjection
Polly
Microsoft.Extensions.Http.Polly
```

重试策略
```c#
//HttpRequestException、500、408会执行该策略 
//.AddTransientHttpErrorPolicy(p=>p.RetryAsync(5)) //重试5次
//循环重试，每次时间递增
//.AddTransientHttpErrorPolicy(p => p.WaitAndRetryForeverAsync(i => TimeSpan.FromSeconds(i * 3)))
//重试20次，每次间隔2秒
.AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(20,i => TimeSpan.FromSeconds(2)))



services.AddGrpcClient<OrderGrpc.OrderGrpcClient>(options =>
{
    options.Address = new Uri("http://localhost:5002");
})
.AddTransientHttpErrorPolicy(p=>p.RetryAsync(5));
```

自定义策略：
```c#
#region 自定义策略
var reg = services.AddPolicyRegistry();
//retryforever策略
reg.Add("retryforever", Policy.HandleResult<HttpResponseMessage>(message =>
{
    //当响应码是201时，执行这个策略
    return message.StatusCode == System.Net.HttpStatusCode.Created;
})
    //.Fallback(HttpResponseMessage)//可以返回我们定义好的response
    
    .RetryForeverAsync()//重试
    
);
//为orderclient添加retryforever策略
services.AddHttpClient("orderclient").AddPolicyHandlerFromRegistry("retryforever"); 
#endregion
```


根据不同的请求参数定义不同的处理策略:  
```c#
//根据不同的请求参数定义不同的处理策略
services.AddHttpClient("orderclientv2").AddPolicyHandlerFromRegistry((registry, message) =>
{
    //是get 返回retryforever策略（重试、熔断、缓存...），不是get返回空的，不执行任何策略
    return message.Method == HttpMethod.Get ? registry.Get<IAsyncPolicy<HttpResponseMessage>>("retryforever") : Policy.NoOpAsync<HttpResponseMessage>();
});
```
# 40丨Polly：熔断慢请求避免雪崩效应

**策略的类型**
- 被动策略（异常处理、结果处理）
- 主动策略（超时处理、断路器、舱壁隔离、缓存）

**组合策略**
- 降级响应
- 失败重试
- 断路器
- 舱壁隔离

当服务发生熔断是，策略会抛出异常：熔断异常

## 熔断
熔断:  
```c#
//熔断
services.AddHttpClient("orderclientv3").AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>().CircuitBreakerAsync(
    handledEventsAllowedBeforeBreaking: 10,//重试10次后熔断
    durationOfBreak: TimeSpan.FromSeconds(10), //熔断时间10秒
    onBreak: (r, t) => { },//发生熔断触发的事件
    onReset: () => { },//熔断恢复事件
    onHalfOpen: () => { }//熔断恢复之前验证服务是否可用事件
    ));
```
高级熔断，不是根据次数，是根据比例
```c#
//高级熔断，不是根据次数，是根据比例
services.AddHttpClient("orderclientv3").AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>().AdvancedCircuitBreakerAsync(
    failureThreshold: 0.8, //错误达到80%熔断
    samplingDuration: TimeSpan.FromSeconds(10),//采样时间范围：10秒内错误达到80%
    minimumThroughput: 100,//最小吞吐量：请求量达到100/s并且10s内错误达到10%，小于这个吞吐量不触发
    durationOfBreak: TimeSpan.FromSeconds(20), //熔断时间20s
    onBreak: (r, t) => { },
    onReset: () => { },
    onHalfOpen: () => { }));
```

组合策略：
```c#
#region 组合策略
//组合策略中的 熔断
var breakPolicy = Policy<HttpResponseMessage>.Handle<HttpRequestException>().AdvancedCircuitBreakerAsync(
    failureThreshold: 0.8,
    samplingDuration: TimeSpan.FromSeconds(10),
    minimumThroughput: 100,
    durationOfBreak: TimeSpan.FromSeconds(20),
    onBreak: (r, t) => { },
    onReset: () => { },
    onHalfOpen: () => { });

//组合策略中的 返回自定义response
var message = new HttpResponseMessage()
{
    Content = new StringContent("{}")
};
var fallback = Policy<HttpResponseMessage>.Handle<BrokenCircuitException>().FallbackAsync(message);

////组合:达到熔断条件后熔断，熔断后抛出异常，然后返回（breakPolicy）自定义友好的响应
//var fallbackBreak = Policy.WrapAsync(fallback, breakPolicy);

//组合策略中的 重试  重试3次，每次等1s
var retry = Policy<HttpResponseMessage>.Handle<Exception>().WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(1));
//组合：熔断后 重试（retry）3次，如果3次仍然失败，返回自定义响应
var fallbackBreak = Policy.WrapAsync(fallback, retry, breakPolicy);
//把组合策略给 httpv3 客户端
services.AddHttpClient("httpv3").AddPolicyHandler(fallbackBreak); 
#endregion
```

## 限流：

```c#
#region 限流
var bulk = Policy.BulkheadAsync<HttpResponseMessage>(
    maxParallelization: 30,//最大并发数
    maxQueuingActions: 20,//最大队列数：超过最大并发后，其他的进入队列，如果队列也超出会抛出异常
    onBulkheadRejectedAsync: contxt => Task.CompletedTask
    );

var message2 = new HttpResponseMessage()
{
    Content = new StringContent("{}")
};
var fallback2 = Policy<HttpResponseMessage>.Handle<BulkheadRejectedException>().FallbackAsync(message);
//限流异常后，返回自定义响应
var fallbackbulk = Policy.WrapAsync(fallback2, bulk);
services.AddHttpClient("httpv4").AddPolicyHandler(fallbackbulk);
#endregion
```