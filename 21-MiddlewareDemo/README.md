

# 21丨中间件：掌控请求处理过程的关键



Startup.Configure:

```c#
app.Use(async (context, next) =>
{
	await context.Response.WriteAsync("Hello World!");
});
```

对特定的路径 指定中间件:

```c#
//对特定的路径 指定中间件
app.Map("/abc", abcBuilder =>
{
    abcBuilder.Use(async (context, next) =>
    {
        //await context.Response.WriteAsync("Hello");//已经改变herder后续不可以在操作header，否则报错
        await next();
        await context.Response.WriteAsync("Hello2");
    });
});
```

带判断逻辑：

```c#

app.MapWhen(context =>
{
    //请求地址参数值中包含abc时
    return context.Request.Query.Keys.Contains("abc");
}, builder =>
{
    // run 中间件执行的末端，后续不在执行
    builder.Run(async context =>
    {
        await context.Response.WriteAsync("new abc");
    });

});
```

创建中间件约定方式：类中包含 InvokeAsync/Invoke函数即可

自定义中间件：

