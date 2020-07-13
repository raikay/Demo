

添加Nuget:

```c#
Autofac.Extras.DynamicProxy
Autofac.Extensions.DependencyInjection
```

在注册第三方容器的入口，注册autofac

```c#
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
             //注册第三方容器的入口
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
```



注册service

```c#

public void ConfigureContainer(ContainerBuilder builder)
{
    builder.RegisterType<MyService>().As<IMyService>();
    #region 命名注册

    builder.RegisterType<MyServiceV2>().Named<IMyService>("service2");
    #endregion

    #region 属性注册
    //builder.RegisterType<MyNameService>();
    //builder.RegisterType<MyServiceV2>().As<IMyService>().PropertiesAutowired();
    #endregion

    #region AOP
    //builder.RegisterType<MyInterceptor>();
    //builder.RegisterType<MyNameService>();
    //builder.RegisterType<MyServiceV2>().As<IMyService>().PropertiesAutowired().InterceptedBy(typeof(MyInterceptor)).EnableInterfaceInterceptors();
    #endregion

    #region 子容器
    //builder.RegisterType<Services.MyNameService>().InstancePerMatchingLifetimeScope("myscope");
    #endregion

}
```



获取service

```c#
this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
//获取不带名字的
var servicenamed = this.AutofacContainer.Resolve<IMyService>();
servicenamed.ShowCode();

//获取带名字的
var service = this.AutofacContainer.ResolveNamed<IMyService>("service2");
service.ShowCode();
```



属性注册

```c#
#region 属性注册
//把要注册的属性类先注册一下，否则管理器找不到
builder.RegisterType<MyNameService>();
//把MyServiceV2 注册为IMyService，  PropertiesAutowired 允许属性注册
builder.RegisterType<MyServiceV2>().As<IMyService>().PropertiesAutowired();
#endregion
    

```

```c#
    
//使用正常
var servicenamed = this.AutofacContainer.Resolve<IMyService>();
  servicenamed.ShowCode();
```



面向切面：

不改变方法，把要执行的内容放在方法前、后



实现接口：

```c#
public class MyInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        Console.WriteLine($"Intercept before,Method:{invocation.Method.Name}");
        invocation.Proceed();//要执行方法的占位
        Console.WriteLine($"Intercept after,Method:{invocation.Method.Name}");
    }
}
```



ConfigureContainer:

```c#
builder.RegisterType<MyServiceV2>().As<IMyService>()
    .PropertiesAutowired()//允许属性注册
    .InterceptedBy(typeof(MyInterceptor))//注册切面类
    //.EnableClassInterceptors();//在类中开启切面注入，需要把方法设计为虚方法
    .EnableInterfaceInterceptors();//开启切面注册
```



实现自动注入：

```c#
public void ConfigureContainer(ContainerBuilder builder)
{
builder.RegisterAssemblyTypes(typeof(Program).Assembly)
.Where(x => x.Name.EndsWith("service", StringComparison.OrdinalIgnoreCase)).AsImplementedInterfaces();
}
```

