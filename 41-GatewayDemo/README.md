# 41丨网关与BFF：区分场景与职责
什么是BFF
- 全称为Backend For Frontend
- 负责认证/授权
- 负责服务聚合
- 目标是为前端的提供服务
- 网关职责的一种进化，区别不大、职责统一

![](https://ftp.bmp.ovh/imgs/2020/07/a803746de07beca1.png)
![](https://ftp.bmp.ovh/imgs/2020/07/dafba05d8000510d.png)
![](https://ftp.bmp.ovh/imgs/2020/07/5d5e3667d71c0236.png)
![](https://ftp.bmp.ovh/imgs/2020/07/c4e2359f707eceea.png)

打造网关
- 添加包 Ocelot 14.0.3
- 添加配置文件 ocelot.json
- 添加配置读取代码
- 注册 Ocelot 服务
- 注册 Ocelot 中间件

不建议微服务之间不应直接相互调用，同时不建议微服务之间共享数据存储。  
微服务之间是通过eventbus来传递集成事件，来传递数据，他们之间不应有直接的调用  

当我们需要微服务之间数据聚合来满足前端的需求是，就可以搭建聚合服务，或者在网关上直接实现聚合服务。  

建议为不同的客户端设计不同的网关。不同的客户端验证方式是不同的，比如移动端toeken,web短session  

专用网关的另外一个好处是故障隔离

**注：** 15.x及以前的配置文件根节点是ReRoutes，16.x以后是Routes

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ReRoutes//": "15.x及以前的配置文件根节点是ReRoutes，16.x以后是Routes",
  "Routes": [
    {
      "DownstreamPathTemplate": "/api/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 5001
        }
      ],
      "UpstreamPathTemplate": "/user/api/{everything}",
      "UpstreamHttpMethod": []
    },
    {
      "DownstreamPathTemplate": "/api/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 5002
        }
      ],
      "UpstreamPathTemplate": "/order/api/{everything}",
      "UpstreamHttpMethod": []
    }

  ]
}

```