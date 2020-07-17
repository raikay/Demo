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

            var builder = services.AddIdentityServer(options =>// ע��IdentityServer4
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })//Ids4����
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Ids4Config.GetIdentityResources())// // ��������Դ
                .AddInMemoryClients(Ids4Config.GetClients())//;//�������ļ���Client������Դ�ŵ��ڴ�
                                .AddTestUsers(new List<IdentityServer4.Test.TestUser>() // ��Ӳ����û�
                                {
                                     new IdentityServer4.Test.TestUser ()
                                     {
                                         Username = "admin",
                                         Password = "1234",
                                         SubjectId = "999"
                                     }
                                })
                                ;


            builder.AddInMemoryApiResources(Ids4Config.GetApis());  // ���Api��Դ
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

            //���ÿ�����ǩ�������ڿ���ʱʹ�ã���������������-����Ҫ����Կ���ϴ洢�ڰ�ȫ�ĵط�
            builder.AddDeveloperSigningCredential();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //ʹ��IdentityServer�м��������ŵ� UseRouting �� UseEndpoints ֮�䡣
            app.UseIdentityServer();
            app.UseStaticFiles();

            app.UseRouting();

            //ʹ����Ȩ�м��
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
