# 36丨HttpClientFactory：管理向外请求的最佳实践

HttpClient请求管道模型:
![](https://ftp.bmp.ovh/imgs/2020/07/542cc254dfa208f3.png)

三种创建HttpClient模式：
- 工厂模式
- 命名客户端模式
- 类型化客户端模式


## 工厂模式
注册：
```
services.AddHttpClient();
```
使用：
```csharp
#region OrderServiceClient.cs
public class OrderServiceClient
{
    IHttpClientFactory _httpClientFactory;

    public OrderServiceClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }


    public async Task<string> Get()
    {
        var client = _httpClientFactory.CreateClient();

        //使用client发起HTTP请求
        var val= await client.GetStringAsync("http://localhost:5003/OrderService");
        return val;
    }
}
#endregion
```

## 命名客户端模式
注册：
```csharp
services.AddSingleton<RequestIdDelegatingHandler>();
services.AddHttpClient("NamedOrderServiceClient", client =>
{
    client.DefaultRequestHeaders.Add("client-name", "namedclient");
    client.BaseAddress = new Uri("http://localhost:5003");
})
//管道
//.AddHttpMessageHandler(provider => provider.GetService<RequestIdDelegatingHandler>())
;
services.AddScoped<NamedOrderServiceClient>();
```
使用：
```csharp
#region NamedOrderServiceClient.cs
public class NamedOrderServiceClient
{
    IHttpClientFactory _httpClientFactory;

    const string _clientName = "NamedOrderServiceClient";  //定义客户端名称

    public NamedOrderServiceClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }


    public async Task<string> Get()
    {
        var client = _httpClientFactory.CreateClient(_clientName); //使用客户端名称获取客户端

        //使用client发起HTTP请求,这里使用相对路径来访问
        return await client.GetStringAsync("/OrderService");
    }
}
#endregion
```