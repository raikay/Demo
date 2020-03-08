using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Raikay.Demo.MSJwt
{
    public class Startup
    {
        /*
         ���demo������һ���������ʾ������������ڻ��棬Ȼ��ȥУ�黺�棬����ͨsessionһ����
         ���ֲܷ�ʽ��ƽ̨��֤�������֤���ڴ���û�оͲ�����֤
         JWT ��������һ����״̬�ĵ�¼��Ȩ��֤
         JWT�ǻ���json�ļ�Ȩ���ƣ���������״̬�ģ�����������û���紫ͳ��������ͻ��˵ĵ�¼��Ϣ�ģ����Ϊ�ֲ�ʽ�����ṩ�˱���
         https://www.cnblogs.com/RayWang/p/9255093.html
             */


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //��֤
            services.AddAuthorization(options =>
            {
                options.AddPolicy("System", policy => policy.RequireClaim("SystemType").Build());
                options.AddPolicy("Client", policy => policy.RequireClaim("ClientType").Build());
                options.AddPolicy("Admin", policy => policy.RequireClaim("AdminType").Build());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            #region Token
            app.UseMiddleware<TokenAuth>(); 
            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
