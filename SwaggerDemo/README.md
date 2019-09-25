
### 1、NuGet引用
```
Swashbuckle.AspNetCore
```
### 2、Startup类中ConfigureServices函数添加代码
```
#region Swagger
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

    #region Swagger
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
        {
            Version = "1.0",
            Title = "SwaggerDemo API",
            Description = "SwaggerDemo文档",
            TermsOfService = "None",
            Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "SwaggerDemo", Email = "raikay@163.com", Url = "http://www.raikay.com/" }
        });
    });
    #endregion
}
#endregion
```
### 3、编辑Configure类
```
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    #region Swagger
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerDemo API V1");
    });
    #endregion
    app.UseMvc();
}
```
### 4、查看效果
访问网址 `http://localhost:5000/swagger/index.html`  
![](./SwaggerDemo/Doc/20190925150742.png)

### 5、设置在域名根目录直接访问swagger
```
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "";//在域名根目录直接访问swagger，
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerDemo API V1");
});

```

### 6、默认跳转页面修改为swagger  

编辑`Properties`文件夹下 `launchSettings.json` 文件，launchUrl值修改为swagger  
  
```
  "profiles": {
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "launchUrl": "swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "SwaggerDemo": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
```
### 7、添加参数/函数注释

##### 项目上右键属性，点击生成，选中下面 `XML 文档文件`  
  
![](./SwaggerDemo/Doc/20190925160612.png)
  
##### NuGet添加引用
```
Microsoft.Extensions.PlatformAbstractions
```
##### 修改代码  
```
#region Swagger
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
    {
        Version = "1.0",
        Title = "SwaggerDemo API",
        Description = "SwaggerDemo文档",
        TermsOfService = "None",
        Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "SwaggerDemo", Email = "raikay@163.com", Url = "http://www.raikay.com/" }
    }); 
    //就是这里
    var basePath = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;
    var xmlPath = System.IO.Path.Combine(basePath, "SwaggerDemo.xml");//这个就是刚刚配置的xml文件名
    c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释
});

#endregion
```
*忽略警告编码：`;1591`*
##### 查看效果
*记得给controller 和参数类都加上注释 才会有注释显示*
![](./SwaggerDemo/Doc/20190925161919.png)

### 8、多层项目，显示其他层注释
上面把Param类放在controller层，所以注释显示没问题，当独立处model层，model的注释就不会显示。  
这个时候我们按照上面的操作，model项目右键-->属性-->生成--选中 XML文档生成   
然后修改代码，添加一条model层xml文档的导入  

```
#region Swagger
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
    {
        Version = "1.0",
        Title = "SwaggerDemo API",
        Description = "SwaggerDemo文档",
        TermsOfService = "None",
        Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "SwaggerDemo", Email = "raikay@163.com", Url = "http://www.raikay.com/" }
    }); 
    //就是这里
    var basePath = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;
    var xmlPath = System.IO.Path.Combine(basePath, "SwaggerDemo.xml");//这个就是刚刚配置的xml文件名
    c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释

    var xmlModelPath = System.IO.Path.Combine(basePath, "SwaggerDemo.Model.xml");//这个就是Model层的xml文件名
    c.IncludeXmlComments(xmlModelPath);
});

#endregion
```


