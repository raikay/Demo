# 23丨静态文件中间件：前后端分离开发合并部署骚操作

### 1、静态文件中间件的能力

- 支持指定想对路径
- 支持目录浏览
- 支持设置默认文档
- 支持多目录映射



### 添加中间件注册

对，就这一句话就可以开启静态文件支持

```
app.UseStaticFiles();
```

根目录创建wwwroot（默认指定的是wwwroot）文件夹

放入静态文件即可。如`wwwroot/index.html`

访问地址

```
http://localhost:5000/index.html
```



设置默认文件

```c#
app.UseDefaultFiles();
```

指定File文件夹下为静态文件文件夹

```c#
app.UseStaticFiles();

#region 指定File文件夹下为静态文件文件夹，和wwwroot共存
//访问地址：http://localhost:5000/page.html
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "File"))
}); 
#endregion

```

映射自定义路径：

```c#
app.UseStaticFiles();

#region 指定File文件夹下为静态文件文件夹，和wwwroot共存
app.UseStaticFiles(new StaticFileOptions
{
    //访问地址：http://localhost:5000/files/page.html
    RequestPath = "/files",
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "File"))
}); 
#endregion
```

开启目录浏览：

```
services.AddDirectoryBrowser();
app.UseDirectoryBrowser();
```



URL重写：

```c#
//非api开头的地址，都从写到index.html(目前主流spa)
app.MapWhen(context =>
{
    return !context.Request.Path.Value.StartsWith("/api");
}, appBuilder =>
{
    var option = new RewriteOptions();
    option.AddRewrite(".*", "/index.html", true);
    appBuilder.UseRewriter(option);

    appBuilder.UseStaticFiles();
    //推荐上面静态文件中间件方式，不是下面这种自己输出文件的方式，下面这种是缓存用到的请求头
    //appBuilder.Run(async c =>
    //{
    //    var file = env.WebRootFileProvider.GetFileInfo("index.html");

    //    c.Response.ContentType = "text/html";
    //    using (var fileStream = new FileStream(file.PhysicalPath, FileMode.Open, FileAccess.Read))
    //    {
    //        await StreamCopyOperation.CopyToAsync(fileStream, c.Response.Body, null, BufferSize, c.RequestAborted);
    //    }
    //});
});
```

