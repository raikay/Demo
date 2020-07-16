# 20丨结构化日志组件Serilog：记录对查询分析友好的日志



Nuget:

```
Serilog.AspNetCore
```

appsetting添加 Serilog

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Error",
        "System": "Information"
      }
    }
  },
  "AllowedHosts": "*"
}

```



Program.Main函数中添加Logger

```c#
#region Serilog
var logConfig = new ConfigurationBuilder()
    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??                 "Production"}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(logConfig)
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .WriteTo.Console(new Serilog.Formatting.Compact.RenderedCompactJsonFormatter())
    .WriteTo.File(formatter: new Serilog.Formatting.Compact.CompactJsonFormatter(), "logs\\myapp.txt",            rollingInterval: RollingInterval.Day)
    .CreateLogger(); 
#endregion
```



注入日志实例，写日志：

```c#
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

        
    public WeatherForecastController(ILogger<WeatherForecastController> /*构造函数注入日志*/logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        //写日志
        _logger.LogInformation("Get 随机创建数据");
        var rng = new Random();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
```

打印结果：

```json
{
    "@t":"2020-07-16T09:18:31.9992123Z",
    "@mt":"Get 随机创建数据",
    "SourceContext":"SerilogDemo.Controllers.WeatherForecastController",
    "ActionId":"09eea1c3-b2a6-4d51-a24b-f87efc9d0813",
    "ActionName":"SerilogDemo.Controllers.WeatherForecastController.Get (SerilogDemo)",
    "RequestId":"0HM19D4PQR1VE:00000001",
    "RequestPath":"/weatherforecast",
    "SpanId":"|23975ccd-46f102381559a99f.",
    "TraceId":"23975ccd-46f102381559a99f",
    "ParentId":"",
    "ConnectionId":"0HM19D4PQR1VE"
}
```

