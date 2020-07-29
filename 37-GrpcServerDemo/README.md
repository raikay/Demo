# 37丨gRPC：内部服务间通讯利器
 - 一个远程过程调用框架
 - 由Google 公司发起并开源

**gRPC的特点**
- 提供几乎所有主流语言的实现，打破语言隔阂
- 基于HTTP/2 ，开放协议，受到广泛的支持，易于实现和集成
- 默认使用Protocol Buffers 序列化，性能相较于RESTful Json 好很多
- 工具链成熟，代码生成便捷，开箱即用
- 支持双向流式的请求和响应，对批量处理、低延时场景友好


**.NET生态对gRPC的支持情况**
- 提供基于HttpClient 的原生框架实现
- 提供原生的ASP .NET Core 集成库
- 提供完整的代码生成工具
- Visual Studio 和Visual StuidoCode 提供proto 文件的智能提示

**核心包**
```
//服务端
Grpc.AspNetCore
//客户端
Google.Protobuf
Grpc.Net.Client
Grpc.Net.ClientFactory
Grpc.Tools
```
order.proto
```protobuf
//表示使用proto3协议
syntax = "proto3";
// 命名空间 GrpcServices
option csharp_namespace = "GrpcServices";

package GrpcServices;

//定义服务名为 OrderGrpc
service OrderGrpc {
    //该服务方法CreateOrder，入参 CreateOrderCommand
    //响应 CreateOrderResult
	rpc CreateOrder(CreateOrderCommand) returns (CreateOrderResult);
}

//定义消息（参数）类型，数字1-5，表示顺序，要定义顺序，
message CreateOrderCommand {
	string buyerId = 1;
    int32 productId = 2;
    double unitPrice = 3;
    double discount = 4;
    int32 units = 5;
}

message CreateOrderResult {
    int32 orderId = 1;
}
```

生成时会根据order.proto自动生成`Order.cs`和`OrderGrpc.cs`,存放在`GrpcServerDemo\obj\Debug\netcoreapp3.1`.  
1、提前引用Grpc.AspNetCore  
2、注意项目文件：
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp3.1</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Grpc.AspNetCore" Version="2.30.0" />
  </ItemGroup>

  <ItemGroup>
    <Folder Include="GrpcSrvices\" />
  </ItemGroup>
  <ItemGroup>
    <Protobuf Include="Proto\order.proto" GrpcServices="Server" />
  </ItemGroup>

</Project>

```

客户端引用服务端的proto文件即可，一样自动生成两个类文件  

# 38丨gRPC：用代码生成工具提高生产效率

**命令**
```c#
//指定目录下proto工程文件添加至工程
dotnet grpc  add-file
//dotnet grpc  add-file ..\GrpcServerDemo\Proto\order.proto
//指定url地址的文件添加至工程
dotnet grpc add-url
//移除文件引用
dotnet grpc remove
//更新文件
dotnet grpc refresh
```

**最佳实践**

- 使用单独的Git 仓库管理proto 文件  
- 使用submodule 将proto 文件集成到工程目录中  
- 使用dotnet-grpc命令行添加proto 文件及相关依赖包引用  

备注：
由proto 生成的代码文件会存放在obj 目录中，不会被签入到Git 仓库

安装工具：
```shell
dotnet tool install dotnet-grpc -g
```