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


            //Configuration: �������󣬶�ȡappsettings.json�������ļ�
            //Log.Logger����ȡConfiguration�е���־������Ϣ��Ȼ����������ļ������ݡ�λ�õȡ�
            //UseSerilog(dispose: true)������Serilog��ܣ�dispose: true=>ϵͳ�˳�ʱ���ͷ���־����
            #region Serilog
            var logConfig = new ConfigurationBuilder()
                        ////���û���·��
                        .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                        ////��������ļ�
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                        ////��ӻ�������
                        .AddEnvironmentVariables()
                        .Build();
            /* ���json�ṹ����־
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(logConfig)
                            .MinimumLevel.Debug()
                            ////ʹ��Serilog.Context.LogContext�е����Էḻ��־�¼���
                            .Enrich.FromLogContext()
                            ////���������̨
                            .WriteTo.Console(new Serilog.Formatting.Compact.RenderedCompactJsonFormatter())
                            ////������ļ�
                            .WriteTo.File(formatter: new Serilog.Formatting.Compact.CompactJsonFormatter(), "logs\\myapp.txt", rollingInterval: RollingInterval.Day)
                            ////���������־���
                            .CreateLogger();
            */

            //�洢��־�ļ���·��
            string LogFilePath(string LogEvent) => $@"{AppContext.BaseDirectory}Logs\{LogEvent}\log.log";
            //�洢��־�ļ��ĸ�ʽ
            string SerilogOutputTemplate = "{NewLine}{NewLine}Date��{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel��{Level}{NewLine}Message��{Message}{NewLine}{Exception}" + new string('-', 50);

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(logConfig)
                         .MinimumLevel.Debug()
                         .Enrich.FromLogContext()//ʹ��Serilog.Context.LogContext�е����Էḻ��־�¼���
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
                //����Serilog��ܣ�dispose:true=>ϵͳ�˳�ʱ���ͷ���־����
                .UseSerilog(dispose: true);
    }
}
