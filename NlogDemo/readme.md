
# 添加日志

### NuGet
```
NLog.Web.AspNetCore
```

### Program配置nlog
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace NlogDemo1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //配置Nlog
            NLogBuilder.ConfigureNLog("nlog.config");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            //配置Nlog
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Information);
                logging.AddConsole();
            }).UseNLog();//其中，UseNLog是拓展方法，需要引入NLog.Web.AspNetCore
    }
}

```
### 配置文件
```
<?xml version="1.0" encoding="utf-8"?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" autoReload="true" internalLogLevel="Info">
  <!-- 启用.net core的核心布局渲染器 -->
  <extensions>
    <add assembly="NLog.Web.AspNetCore" />
  </extensions>
  <!-- 写入日志的目标配置 archiveAboveSize="102400" maxArchiveDays="60" -->
  <targets>
    <!-- 调试  -->
    <target xsi:type="File" name="debug" fileName="../logs/debug-${shortdate}.log" layout="${longdate}|${event-properties:item=EventId_Id}|${uppercase:${level}}|${logger}|${message} ${exception:format=tostring}|url: ${aspnet-request-url}|action: ${aspnet-mvc-action}" />
    <!-- 警告  -->
    <target xsi:type="File" name="warn" fileName="../logs/warn-${shortdate}.log" layout="${longdate}|${event-properties:item=EventId_Id}|${uppercase:${level}}|${logger}|${message} ${exception:format=tostring}|url: ${aspnet-request-url}|action: ${aspnet-mvc-action}" />
    <!-- 错误  -->
    <target xsi:type="File" name="error" fileName="../logs/error-${shortdate}.log" layout="${longdate}|${event-properties:item=EventId_Id}|${uppercase:${level}}|${logger}|${message} ${exception:format=tostring}|url: ${aspnet-request-url}|action: ${aspnet-mvc-action}" />
    <!-- 控制台  -->
    <target xsi:type="Console" name="console" layout="${message}" />
  </targets>
  <!-- 映射规则 -->
  <rules>
    <!-- 调试  -->
    <logger name="*" minlevel="Trace" maxlevel="Debug" writeTo="debug" />
    <!--<logger name="*" minlevel="Trace" writeTo="console" />-->
    <!-- 警告  -->
    <logger name="*" minlevel="Info" maxlevel="Warn" writeTo="warn" />
    <!-- 错误  -->
    <logger name="*" minlevel="Error" maxlevel="Fatal" writeTo="error" />
    <!--跳过不重要的微软日志-->
    <logger name="Microsoft.*" maxlevel="Info" final="true" />
  </rules>
</nlog>
```

### 使用log
```
[HttpGet]
public IEnumerable<WeatherForecast> Get()
{
    _logger.LogError("错误日志");
    _logger.LogDebug("调试日志");
    var s = 0;
    var s1 = 5 / s;
    var rng = new Random();
    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = rng.Next(-20, 55),
        Summary = Summaries[rng.Next(Summaries.Length)]
    })
    .ToArray();
}
```

# 全局异常

新建类并实现接口 `IExceptionFilter` / `IAsyncExceptionFilter` 

```
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace NlogDemo.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(IWebHostEnvironment env, ILogger<GlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            string message;
            
            if (_env.IsProduction())
            {
                message =$"IsProduction:{context.Exception.Message}";//Enums.StatusCodes.Status500InternalServerError.ToDescription();
            }
            else
            {
                message = context.Exception.Message;
            }

            _logger.LogError(context.Exception, message);
            //var data = ResponseOutput.NotOk(message);
            context.Result = new InternalServerErrorResult(new { msg= message });
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }

    }
    public class InternalServerErrorResult : ObjectResult
    {
        public InternalServerErrorResult(object value) : base(value)
        {
            StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
        }
    }
}
```
注入全局异常处理：
```
 options.Filters.Add<GlobalExceptionFilter>();
```
如下：
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers(options =>
    {
        options.Filters.Add<GlobalExceptionFilter>();
        //禁止去除ActionAsync后缀
        options.SuppressAsyncSuffixInActionNames = false;
    });
}
```



鸣谢：
```
https://www.cnblogs.com/fger/p/12118437.html
https://www.cnblogs.com/dotnet261010/p/13286218.html?utm_source=tuicool
```