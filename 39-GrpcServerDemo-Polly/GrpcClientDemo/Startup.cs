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

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true); //����ʹ�ò����ܵ�HTTP/2Э��
            services.AddGrpcClient<OrderGrpc.OrderGrpcClient>(options =>
            {
                //������SetSwitch ֱ�ӷ��� 5002 ����˵�http2�ᱨ��
                //options.Address = new Uri("https://localhost:5001");
                options.Address = new Uri("http://localhost:5002");
            })
                //HttpRequestException��500��408��ִ�иò��� 
                //.AddTransientHttpErrorPolicy(p=>p.RetryAsync(5)) //����5��
                //ѭ�����ԣ�ÿ��ʱ�����
                //.AddTransientHttpErrorPolicy(p => p.WaitAndRetryForeverAsync(i => TimeSpan.FromSeconds(i * 3)))
                //����20�Σ�ÿ�μ��2��
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(20,i => TimeSpan.FromSeconds(2)))


            ////��֤֤����Զ����true ������Ч����ǩ��֤��
            //.ConfigurePrimaryHttpMessageHandler(provider =>
            //{
            //    var handler = new SocketsHttpHandler();
            //    handler.SslOptions.RemoteCertificateValidationCallback = (a, b, c, d) => true; //������Ч������ǩ��֤��
            //    return handler;
            //})
            ;
            #region �Զ������
            /*
            var reg = services.AddPolicyRegistry();
            //retryforever����
            reg.Add("retryforever", Policy.HandleResult<HttpResponseMessage>(message =>
            {
                //����Ӧ����201ʱ��ִ���������
                return message.StatusCode == System.Net.HttpStatusCode.Created;
            })
                //.Fallback(HttpResponseMessage)//���Է������Ƕ���õ�response
                
                .RetryForeverAsync()//����
                
            );
            //Ϊorderclient���retryforever����
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
