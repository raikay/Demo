using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace SerilogDemo
{
    public class Program
    {
        //public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        //                                                            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
        //                                                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //                                                            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
        //                                                            .AddEnvironmentVariables()
        //                                                            .Build();



        public static void Main(string[] args)
        {


            //Configuration: 构建对象，读取appsettings.json的配置文件
            //Log.Logger：读取Configuration中的日志配置信息，然后设置输出的级别、内容、位置等。
            //UseSerilog(dispose: true)：引入Serilog框架，dispose: true=>系统退出时，释放日志对象
            #region Serilog
            var logConfig = new ConfigurationBuilder()
                        ////设置基础路径
                        .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                        ////添加配置文件
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                        ////添加环境变量
                        .AddEnvironmentVariables()
                        .Build();
            /* 输出json结构化日志
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(logConfig)
                            .MinimumLevel.Debug()
                            ////使用Serilog.Context.LogContext中的属性丰富日志事件。
                            .Enrich.FromLogContext()
                            ////输出到控制台
                            .WriteTo.Console(new Serilog.Formatting.Compact.RenderedCompactJsonFormatter())
                            ////输出到文件
                            .WriteTo.File(formatter: new Serilog.Formatting.Compact.CompactJsonFormatter(), "logs\\myapp.txt", rollingInterval: RollingInterval.Day)
                            ////清除内置日志框架
                            .CreateLogger();
            */

            //存储日志文件的路径
            string LogFilePath(string LogEvent) => $@"{AppContext.BaseDirectory}Logs\{LogEvent}\log.log";
            //存储日志文件的格式
            string SerilogOutputTemplate = "{NewLine}{NewLine}Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel：{Level}{NewLine}Message：{Message}{NewLine}{Exception}" + new string('-', 50);

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(logConfig)
                         .MinimumLevel.Debug()
                         .Enrich.FromLogContext()//使用Serilog.Context.LogContext中的属性丰富日志事件。
                         .WriteTo.Console(new Serilog.Formatting.Compact.RenderedCompactJsonFormatter())
                         .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Debug).WriteTo.File(LogFilePath("Debug"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                         .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information).WriteTo.File(LogFilePath("Information"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                         .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Warning).WriteTo.File(LogFilePath("Warning"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                         .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error).WriteTo.File(LogFilePath("Error"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                         .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Fatal).WriteTo.File(LogFilePath("Fatal"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                         .CreateLogger();
            #endregion
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                //引入Serilog框架，dispose:true=>系统退出时，释放日志对象
                .UseSerilog(dispose: true);
    }
}
