using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutofacDemo.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AutofacDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            #region AutoFac
            //core 原生 DI
            //services.AddTransient<IServices, Services>();
            //实例化 AutoFac  容器   
            var builder = new ContainerBuilder();

            //注册要创建的组件
            builder.RegisterType<Services>();//.As<IServices>();

            //var assemblysServices = Assembly.Load("AutofacDemo.Service");//要记得!!!这个注入的是实现类层，不是接口层！不是 IServices
            //builder.RegisterAssemblyTypes(assemblysServices);//.AsImplementedInterfaces();//指定已扫描程序集中的类型注册为提供所有其实现的接口。


            //将services填充到Autofac容器生成器中
            builder.Populate(services);

            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();

            //第三方IOC接管 core内置DI容器
            return new AutofacServiceProvider(ApplicationContainer);
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
