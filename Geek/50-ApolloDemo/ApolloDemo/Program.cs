using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Ctrip.Framework.Apollo;
using Com.Ctrip.Framework.Apollo.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ApolloDemo
{
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }



    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
            {
                //阿波罗日志输出级别
                LogManager.UseConsoleLogging(Com.Ctrip.Framework.Apollo.Logging.LogLevel.Trace);
                //var c = configurationBuilder.Build().GetSection("Apollo").Get<ApolloOptions>();
                //注入阿波罗配置
                configurationBuilder.AddApollo(configurationBuilder.Build().GetSection("Apollo"))
                //.AddDefault(Com.Ctrip.Framework.Apollo.Enums.ConfigFileFormat.Json)
                .AddDefault()
                .AddNamespace("app-aidemo4438", Com.Ctrip.Framework.Apollo.Enums.ConfigFileFormat.Json)
                .AddNamespace("application", Com.Ctrip.Framework.Apollo.Enums.ConfigFileFormat.Properties);

                //本地如果有值覆盖apollo
                //reloadOnChange 如果文件改变 从新加载配置
                configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                //最下面的一个文件 覆盖前一个文件
                configurationBuilder.AddJsonFile("test.json", optional: false, reloadOnChange: true);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
}
