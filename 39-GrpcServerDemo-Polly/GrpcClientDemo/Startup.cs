using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GrpcServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static GrpcServices.OrderGrpc;
using Polly;

namespace GrpcClientDemo
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
            services.AddControllers();

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true); //允许使用不加密的HTTP/2协议
            services.AddGrpcClient<OrderGrpc.OrderGrpcClient>(options =>
            {
                //不配置SetSwitch 直接访问 5002 服务端的http2会报错
                //options.Address = new Uri("https://localhost:5001");
                options.Address = new Uri("http://localhost:5002");
            })
                //HttpRequestException、500、408会执行该策略 
                //.AddTransientHttpErrorPolicy(p=>p.RetryAsync(5)) //重试5次
                //循环重试，每次时间递增
                //.AddTransientHttpErrorPolicy(p => p.WaitAndRetryForeverAsync(i => TimeSpan.FromSeconds(i * 3)))
                //重试20次，每次间隔2秒
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(20,i => TimeSpan.FromSeconds(2)))


            ////验证证书用远返回true 允许无效、自签名证书
            //.ConfigurePrimaryHttpMessageHandler(provider =>
            //{
            //    var handler = new SocketsHttpHandler();
            //    handler.SslOptions.RemoteCertificateValidationCallback = (a, b, c, d) => true; //允许无效、或自签名证书
            //    return handler;
            //})
            ;
            #region 自定义策略
            /*
            var reg = services.AddPolicyRegistry();
            //retryforever策略
            reg.Add("retryforever", Policy.HandleResult<HttpResponseMessage>(message =>
            {
                //当响应码是201时，执行这个策略
                return message.StatusCode == System.Net.HttpStatusCode.Created;
            })
                //.Fallback(HttpResponseMessage)//可以返回我们定义好的response
                
                .RetryForeverAsync()//重试
                
            );
            //为orderclient添加retryforever策略
            services.AddHttpClient("orderclient").AddPolicyHandlerFromRegistry("retryforever"); 
            */
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    OrderGrpcClient service = context.RequestServices.GetService<OrderGrpcClient>();

                    try
                    {
                        var r = service.CreateOrder(new CreateOrderCommand { BuyerId = "abc" });
                    }
                    catch (Exception ex)
                    {
                    }

                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
