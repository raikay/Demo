
# 14丨自定义配置数据源：低成本实现定制化配置方案

获取远程阿波罗配置等

Nuget:
```
Microsoft.Extensions.Configuration  
Microsoft.Extensions.Configuration.Abstractions
```


# 其他

## 08丨配置框架：让服务无缝适应各种环境

Nuget:
```
Microsoft.Extensions.Configuration  
Microsoft.Extensions.Configuration.Abstractions
```

配置添加在内存：
```csharp
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
namespace ConfigurationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new Dictionary<string, string>()
            {
                { "key1","value1" },
                { "key2","value2" },
                { "section1:key4","value4" },
                { "section2:key5","value5" },
                { "section2:key6","value6" },
                { "section2:section3:key7","value7" }
            });

            IConfigurationRoot configurationRoot = builder.Build();

            ///IConfiguration config = configurationRoot;

            Console.WriteLine(configurationRoot["key1"]);
            Console.WriteLine(configurationRoot["key2"]);

            IConfigurationSection section = configurationRoot.GetSection("section1");
            Console.WriteLine($"key4:{section["key4"]}");
            Console.WriteLine($"key5:{section["key5"]}");

            IConfigurationSection section2 = configurationRoot.GetSection("section2");
            Console.WriteLine($"key5_v2:{section2["key5"]}");
            var section3 = section2.GetSection("section3");
            Console.WriteLine($"key7:{section3["key7"]}");



        }
    }
}
```

环境变量：
```
using Microsoft.Extensions.Configuration;
using System;

namespace ConfigurationEnvironmentVariablesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            //builder.AddEnvironmentVariables();

            //var configurationRoot = builder.Build();
            //Console.WriteLine($"key1:{configurationRoot["key1"]}");


            //#region 分层键
            //var section = configurationRoot.GetSection("SECTION1");
            //Console.WriteLine($"KEY3:{section["KEY3"]}");

            //var section2 = configurationRoot.GetSection("SECTION1:SECTION2"); 
            //Console.WriteLine($"KEY4:{section2["KEY4"]}");
            //#endregion

            #region 前缀过滤
            builder.AddEnvironmentVariables("XIAO_");
            var configurationRoot = builder.Build();
            Console.WriteLine($"KEY1:{configurationRoot["KEY1"]}");
            Console.WriteLine($"KEY2:{configurationRoot["KEY2"]}");
            Console.ReadKey();
            #endregion
        }
    }
}
```

![](https://imgkr.cn-bj.ufileos.com/b9125581-9600-4331-ba41-2aadef7d2d4f.png)

读取文件配置：
```csharp
var builder = new ConfigurationBuilder();

//optional:false 文件不存在报错
//reloadOnChange:true  文件变更重新读取新的文件
builder.AddJsonFile("appsettings.json",optional:false,reloadOnChange:true);

var configurationRoot = builder.Build();
//Console.WriteLine($"Key1:{configurationRoot["Key1"]}");
//Console.WriteLine($"Key2:{configurationRoot["Key2"]}");
//Console.WriteLine($"Key3:{configurationRoot["Key3"]}");
Console.ReadKey();
```
> 后添加的文件会覆盖前边的值

配置文件改变触发事件：
```csharp
builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var configurationRoot = builder.Build();

ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), () =>
{
    //配置文件改变触发事件
    Console.WriteLine($"Key1:{configurationRoot["Key1"]}");
    Console.WriteLine($"Key2:{configurationRoot["Key2"]}");
    Console.WriteLine($"Key3:{configurationRoot["Key3"]}");
});
Console.WriteLine("开始了");
```

支持强类型：
```
var builder = new ConfigurationBuilder();
builder.AddJsonFile("appsettings.json");

var configurationRoot = builder.Build();

var config = new Config()
{
    Key1 = "config key1",
    Key5 = false
};

configurationRoot.Bind(config);
//这里读取输出的不是new对象时赋的值，是配置文件的值
Console.WriteLine($"Key1:{config.Key1}");
Console.WriteLine($"Key5:{config.Key5}");
Console.WriteLine($"Key6:{config.Key6}");

```
appsetting:
```
{
  "Key2": "Value2",
  "Key6": 0,
  "OrderService": {
    "Key1": "order key1",
    "Key5": true,
    "Key6": 200
  }
}

```


Nuget:
```
//支持强类型
Microsoft.Extensions.Configuration.Binder

//支持文件类型
Microsoft.Extensions.Configuration.Ini
Microsoft.Extensions.Configuration.Json
```

```
//configurationRoot.GetSection("OrderService").Bind(config);

// binderOptions.BindNonPublicProperties = true 私有变量生效
configurationRoot.GetSection("OrderService").Bind(config, 
    binderOptions => { binderOptions.BindNonPublicProperties = true; });
```