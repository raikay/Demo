using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExceptionDemo.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExceptionDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}



            //app.UseExceptionHandler(errApp =>
            //{
            //    errApp.Run(async context =>
            //    {
            //        //在  Features 获取异常
            //        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            //        IKnownException knownException = exceptionHandlerPathFeature.Error as IKnownException;
            //        if (knownException == null)
            //        {
            //            //不是一直错误 非业务逻辑错误
            //            var logger = context.RequestServices.GetService<ILogger<MyExceptionFilterAttribute>>();
            //            logger.LogError(exceptionHandlerPathFeature.Error, exceptionHandlerPathFeature.Error.Message);
            //            knownException = KnownException.Unknown;
            //            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //        }
            //        else
            //        {
            //            //业务逻辑的错误
            //            knownException = KnownException.FromKnownException(knownException);
            //            context.Response.StatusCode = StatusCodes.Status200OK;
            //        }
            //        var jsonOptions = context.RequestServices.GetService<IOptions<JsonOptions>>();
            //        context.Response.ContentType = "application/json; charset=utf-8";
            //        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(knownException, jsonOptions.Value.JsonSerializerOptions));
            //    });
            //});
            //app.UseExceptionHandler("/error");
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
