using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using AutoFacDemo2.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoFacDemo2
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
            services.AddControllers();//.AddControllersAsServices();
        }



        public void ConfigureContainer(ContainerBuilder builder)
        {
            //builder.RegisterType<MyService>().As<IMyService>();
            #region ����ע��

            //builder.RegisterType<MyServiceV2>().Named<IMyService>("service2");
            #endregion

            #region ����ע��
            //��Ҫע�����������ע��һ�£�����������Ҳ���
           // builder.RegisterType<MyNameService>();
            //��MyServiceV2 ע��ΪIMyService��  PropertiesAutowired ��������ע��
           // builder.RegisterType<MyServiceV2>().As<IMyService>().PropertiesAutowired();
            #endregion

            #region AOP
            builder.RegisterType<MyInterceptor>();
            builder.RegisterType<MyNameService>();

            builder.RegisterType<MyServiceV2>().As<IMyService>()
                .PropertiesAutowired()//��������ע��
                .InterceptedBy(typeof(MyInterceptor))//ע��������
                //.EnableClassInterceptors();//�����п�������ע�룬��Ҫ�ѷ������Ϊ�鷽��
                .EnableInterfaceInterceptors();//��������ע��
            #endregion

            #region ������
            //builder.RegisterType<Services.MyNameService>().InstancePerMatchingLifetimeScope("myscope");
            #endregion

        }
        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Autofac


            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            var servicenamed = this.AutofacContainer.Resolve<IMyService>();
            servicenamed.ShowCode();


            //var service = this.AutofacContainer.ResolveNamed<IMyService>("service2");
            //service.ShowCode();

            #region ������

            //using (var myscope = AutofacContainer.BeginLifetimeScope("myscope"))
            //{
            //    var service0 = myscope.Resolve<MyNameService>();
            //    using (var scope = myscope.BeginLifetimeScope())
            //    {
            //        var service1 = scope.Resolve<MyNameService>();
            //        var service2 = scope.Resolve<MyNameService>();
            //        Console.WriteLine($"service1=service2:{service1 == service2}");
            //        Console.WriteLine($"service1=service0:{service1 == service0}");
            //    }
            //}
            #endregion
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }



    }
}
