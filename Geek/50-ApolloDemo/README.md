# 50丨配置：使用分布式配置中心方案版本化管理配置

Nuget:
```
Com.Ctrip.Framework.Apollo.Configuration
```

```csharp
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
                //configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

```

appsetting
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "abc": "loc-abc",
    "RedisCache//": {
      "Configuration": "192.168.198.128:6379",
      "InstanceName": "GoodSite"
    },
  "Apollo": {
    "AppId//": "配置中心唯一标识",
    "AppId": "apidemo4438",
    "Env//": "环境",
    "Env": "DEV",
    "MetaServer": "http://106.54.227.205:8080/",
    "ConfigServer//": "这个是协程的示例地址",
    "ConfigServer": [ "http://106.54.227.205:8080/" ]
  },
  "AllowedHosts": "*"
}

```

apollo配置图：

![img](http://cdn.mc.huluxia.net/g4/M02/EE/16/rBAAdl8rp6KAWmxYAAFMwA70S_8397.png)

地址：http://106.54.227.205

apollo/admin




