



# 22丨异常处理中间件：区分真异常与逻辑异常



### 第一步 : 定义异常类

```c#
public interface IKnownException
{
    public string Message { get; }

    public int Code { get; }

    public object[] Data { get; }
}
```

```c#
public class KnownException : IKnownException
{
    public string Message { get; private set; }

    public int Code { get; private set; }

    public object[] Data { get; private set; }

    public readonly static IKnownException Unknown = new KnownException { Message = "未知错误", Code = 9999 };

    public static IKnownException FromKnownException(IKnownException exception)
    {
    	return new KnownException { Message = exception.Message, Code = exception.Code, Data = exception.Data };
    }
}
```



### 第二步 : 定义异常过滤器

```c#
public class MyExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        IKnownException knownException = context.Exception as IKnownException;
        if (knownException == null)
        {
            var logger = context.HttpContext.RequestServices.GetService<ILogger<MyExceptionFilterAttribute>>();
            logger.LogError(context.Exception, context.Exception.Message);
            knownException = KnownException.Unknown;
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
        else
        {
            knownException = KnownException.FromKnownException(knownException);
            context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
        }
        context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(knownException)
        {
            ContentType = "application/json; charset=utf-8"
        };
    }
}
```

### 第三步 :定义错误类型：

```c#
public class MyServerException : Exception, IKnownException
{
    public MyServerException(string message, int errorCode, params object[] errorData) : base(message)
    {
        this.Code = errorCode;
        this.Data = errorData;
    }

    public int Code { get; private set; }
    public object[] Data { get; private set; }
}
```

### 第四步 :注册全局拦截：

```c#
public void ConfigureServices(IServiceCollection services)
{
    //这种方法只是作用在MVC里面，不像中间件会更早一些
    services.AddMvc(mvcOptions =>
    {
        mvcOptions.Filters.Add<Exceptions.MyExceptionFilter>();
        // MyExceptionFilterAttribute 可以不在这里全局注册，
        //可以更细力度的 只在某个函数或 controller标注属性  //[MyExceptionFilter]
        //mvcOptions.Filters.Add<Exceptions.MyExceptionFilterAttribute>();
    }).AddJsonOptions(jsonoptions =>
    {
        jsonoptions.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    });
    services.AddControllers();
}
```



**抛出错误：**

```c#
public IEnumerable<WeatherForecast> Get()
{
    //throw new Exceptions.MyServerException("服务出错了", 65);
    throw new Exception("出错啦");
}
```



