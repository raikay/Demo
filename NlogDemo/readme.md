

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
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" autoReload="true">
  　　<targets>
    　　　　<target name="defaultlog" xsi:type="File" keepFileOpen="false" encoding="utf-8" fileName="${basedir}/logs/${level}${shortdate}.log" maxArchiveFiles="100" layout="${longdate}|${level:uppercase=true}|${logger}|${message}" />
    　　　　<!--fileName值——表示在程序运行目录，分日志级别按天写入日志文件-->
    　　　　<!--maxArchiveFiles值——日志文件最大数量，超出则删除最早的文件-->
    　　　　<!--layout值——日志内容格式：时间+日志级别+LoggerName+日志内容-->　　
  </targets>
  　　<rules>
    　　　　<!--支持将任意级别、任意LoggerName的日志写入target：defaultlog-->
    　　　　<!--其中*就表示任意，可以改为"项目命名空间.*"，则只输出对应命名空间下的日志。在Info级别尤为明显-->
    　　　　<logger name="*" minlevel="trace" writeTo="defaultlog" />
  </rules>
</nlog>
```

### 使用log
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoCore.Controllers
{
    [Route("api/[controller]/[action]")]  //Api控制器
    [ApiController]
    public class HomeController : Controller
    {
        private ILogger _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation("测试一下，不要紧张!");
            return new string[] { "value1", "value2" };
        }   
    }
}
```
鸣谢：
```
https://www.cnblogs.com/fger/p/12118437.html
```