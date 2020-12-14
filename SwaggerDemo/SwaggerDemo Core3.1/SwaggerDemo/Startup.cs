using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace SwaggerDemo
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "1.0",
                    Title = "SwaggerDemo API",
                    Description = "SwaggerDemo文档",
                    Contact = new OpenApiContact { Email = "raikay@163.com", Name = "Swagger Name", Url = new Uri("http://www.raikay.com/") }
                });
                //添加生成的xml文件
                var basePath = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = System.IO.Path.Combine(basePath, "SwaggerDemo.xml");//这个就是刚刚配置的xml文件名
                options.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释

                var xmlModelPath = System.IO.Path.Combine(basePath, "SwaggerDemo.Model.xml");//这个就是Model层的xml文件名
                options.IncludeXmlComments(xmlModelPath);

                //添加Jwt验证设置
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new List<string>()
                        }
    });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Value: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //c.RoutePrefix = "";//在域名根目录直接访问swagger，
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerDemo API V1");
            });

            #endregion
            app.UseMvc();
        }
    }
}
