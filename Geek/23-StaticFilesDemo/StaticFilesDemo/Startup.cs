using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace StaticFilesDemo
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
            services.AddDirectoryBrowser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //sapp.UseDefaultFiles();//����Ĭ���ļ����������index.html
            app.UseDirectoryBrowser();
            app.UseStaticFiles();

            #region ָ��File�ļ�����Ϊ��̬�ļ��ļ��У���wwwroot����
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/files",
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "File"))
            });
            #endregion

            //��api��ͷ�ĵ�ַ������д��index.html(Ŀǰ����spa)
            app.MapWhen(context =>
            {
                return !context.Request.Path.Value.StartsWith("/api");
            }, appBuilder =>
            {
                var option = new RewriteOptions();
                option.AddRewrite(".*", "/index.html", true);
                appBuilder.UseRewriter(option);

                appBuilder.UseStaticFiles();
                //�Ƽ����澲̬�ļ��м����ʽ���������������Լ�����ļ��ķ�ʽ�����������ǻ����õ�������ͷ
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



            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
