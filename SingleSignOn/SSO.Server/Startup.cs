using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SSO.Server
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
            services.AddControllersWithViews();

            var builder = services.AddIdentityServer(options =>// 注册IdentityServer4
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })//Ids4服务
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Ids4Config.GetIdentityResources())// // 添加身份资源
                .AddInMemoryClients(Ids4Config.GetClients())//;//把配置文件的Client配置资源放到内存
                                .AddTestUsers(new List<IdentityServer4.Test.TestUser>() // 添加测试用户
                                {
                                     new IdentityServer4.Test.TestUser ()
                                     {
                                         Username = "admin",
                                         Password = "1234",
                                         SubjectId = "999"
                                     }
                                })
                                ;


            builder.AddInMemoryApiResources(Ids4Config.GetApis());  // 添加Api资源
            /*
            //// configures IIS out-of-proc settings (see https://github.com/aspnet/AspNetCore/issues/14882)
            //services.Configure<IISOptions>(iis =>
            //{
            //    iis.AuthenticationDisplayName = "Windows";
            //    iis.AutomaticAuthentication = false;
            //});

            //// configures IIS in-proc settings
            //services.Configure<IISServerOptions>(iis =>
            //{
            //    iis.AuthenticationDisplayName = "Windows";
            //    iis.AutomaticAuthentication = false;
            //});
            
            */
            // or in-memory, json config
            //builder.AddInMemoryIdentityResources(Configuration.GetSection("IdentityResources"));
            //builder.AddInMemoryApiResources(Configuration.GetSection("ApiResources"));
            //builder.AddInMemoryClients(Configuration.GetSection("clients"));

            //启用开发者签名，仅在开发时使用，不建议用于生产-你需要将密钥材料存储在安全的地方
            builder.AddDeveloperSigningCredential();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //使用IdentityServer中间件，必须放到 UseRouting 与 UseEndpoints 之间。
            app.UseIdentityServer();
            app.UseStaticFiles();

            app.UseRouting();

            //使用授权中间件
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
