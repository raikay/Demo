### 描述
依赖注入，使代码降低耦合。  
以往的代码是，自己管理/创建（new）依赖项，如果依赖更换，要修改所有创建依赖项代码。  
依赖注入，只需要在注入的地方修改就可以  

### 如何使用

#### NuGet添加引用
```
Autofac.Extras.DynamicProxy
Autofac.Extensions.DependencyInjection
```

#### 修改Starup中的ConfigureServices函数
```
//记得修改返回值
public IServiceProvider ConfigureServices(IServiceCollection services)
	{
		services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

		#region AutoFac
		//实例化 AutoFac  容器   
		var builder = new ContainerBuilder();

		//注册要创建的组件
		builder.RegisterType<Services>().As<IServices>();

		//将services填充到Autofac容器生成器中
		builder.Populate(services);

		//使用已进行的组件登记创建新容器
		var ApplicationContainer = builder.Build();

		//第三方IOC接管 core内置DI容器
		return new AutofacServiceProvider(ApplicationContainer);
		#endregion

	}
```

#### 构造函数注入以及调用
```
using System.Collections.Generic;
using AutofacDemo.Service;
using Microsoft.AspNetCore.Mvc;

namespace AutofacDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IServices _services;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="services"></param>
        public ValuesController(IServices services)
        {
            _services = services;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return _services.GetDataList().ToArray();
        }
    }
}

```
目前为止已经调用成功，可以正常调用。
### 批量注册到容器
上面的方法是单个的，当接口多时，采用如下方法批量注册  

```

#region AutoFac

//实例化 AutoFac  容器   
var builder = new ContainerBuilder();

//注册要创建的组件
//builder.RegisterType<Services>().As<IServices>();
//只需要将上面这一行，修改为下面两行就可以
var assemblysServices = Assembly.Load("AutofacDemo.Service");//要记得!!!这个注入的是实现类层，不是接口层！不是 IServices
builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();//指定已扫描程序集中的类型注册为提供所有其实现的接口。


//将services填充到Autofac容器生成器中
builder.Populate(services);

//使用已进行的组件登记创建新容器
var ApplicationContainer = builder.Build();

//第三方IOC接管 core内置DI容器
return new AutofacServiceProvider(ApplicationContainer);
#endregion
```

### NetCore自带DI注入，以及批量注入
```
//单个注入
//services.AddTransient<IServices, Services>();
批量注入
AddAssembly(services, "AutofacDemo.Service");
```
#### AddAssembly 函数
```
/// <summary>  
/// 自动注册服务——获取程序集中的实现类对应的多个接口
/// </summary>
/// <param name="services">服务集合</param>  
/// <param name="assemblyName">程序集名称</param>
public void AddAssembly(IServiceCollection services, string assemblyName)
{
	if (!String.IsNullOrEmpty(assemblyName))
	{
		Assembly assembly = Assembly.Load(assemblyName);
		List<Type> ts = assembly.GetTypes().Where(u => u.IsClass && !u.IsAbstract && !u.IsGenericType).ToList();
		foreach (var item in ts.Where(s => !s.IsInterface))
		{
			var interfaceType = item.GetInterfaces();
			if (interfaceType.Length == 1)
			{
				services.AddTransient(interfaceType[0], item);
			}
			if (interfaceType.Length > 1)
			{
				services.AddTransient(interfaceType[1], item);
			}
		}
	}
}
```
### 没有接口的注入
```
//单个
builder.RegisterType<Services>();//.As<IServices>();
//批量
//var assemblysServices = Assembly.Load("AutofacDemo.Service");//要记得!!!这个注入的是实现类层，不是接口层！不是 IServices
//builder.RegisterAssemblyTypes(assemblysServices);//.AsImplementedInterfaces();//指定已扫描程序集中的类型注册为提供所有其实现的接口。

```
