
# 14ح�Զ�����������Դ���ͳɱ�ʵ�ֶ��ƻ����÷���

��ȡԶ�̰��������õ�

Nuget:
```
Microsoft.Extensions.Configuration  
Microsoft.Extensions.Configuration.Abstractions
```


# ����

## 08ح���ÿ�ܣ��÷����޷���Ӧ���ֻ���

Nuget:
```
Microsoft.Extensions.Configuration  
Microsoft.Extensions.Configuration.Abstractions
```

����������ڴ棺
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

����������
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


            //#region �ֲ��
            //var section = configurationRoot.GetSection("SECTION1");
            //Console.WriteLine($"KEY3:{section["KEY3"]}");

            //var section2 = configurationRoot.GetSection("SECTION1:SECTION2"); 
            //Console.WriteLine($"KEY4:{section2["KEY4"]}");
            //#endregion

            #region ǰ׺����
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

��ȡ�ļ����ã�
```csharp
var builder = new ConfigurationBuilder();

//optional:false �ļ������ڱ���
//reloadOnChange:true  �ļ�������¶�ȡ�µ��ļ�
builder.AddJsonFile("appsettings.json",optional:false,reloadOnChange:true);

var configurationRoot = builder.Build();
//Console.WriteLine($"Key1:{configurationRoot["Key1"]}");
//Console.WriteLine($"Key2:{configurationRoot["Key2"]}");
//Console.WriteLine($"Key3:{configurationRoot["Key3"]}");
Console.ReadKey();
```
> ����ӵ��ļ��Ḳ��ǰ�ߵ�ֵ

�����ļ��ı䴥���¼���
```csharp
builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var configurationRoot = builder.Build();

ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), () =>
{
    //�����ļ��ı䴥���¼�
    Console.WriteLine($"Key1:{configurationRoot["Key1"]}");
    Console.WriteLine($"Key2:{configurationRoot["Key2"]}");
    Console.WriteLine($"Key3:{configurationRoot["Key3"]}");
});
Console.WriteLine("��ʼ��");
```

֧��ǿ���ͣ�
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
//�����ȡ����Ĳ���new����ʱ����ֵ���������ļ���ֵ
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
//֧��ǿ����
Microsoft.Extensions.Configuration.Binder

//֧���ļ�����
Microsoft.Extensions.Configuration.Ini
Microsoft.Extensions.Configuration.Json
```

```
//configurationRoot.GetSection("OrderService").Bind(config);

// binderOptions.BindNonPublicProperties = true ˽�б�����Ч
configurationRoot.GetSection("OrderService").Bind(config, 
    binderOptions => { binderOptions.BindNonPublicProperties = true; });
```