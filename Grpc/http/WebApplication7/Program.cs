using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    #region 设置没有TLS的HTTP/2终结点。
                    //如果内网映射直接tcp类型 不是http类型

                    webBuilder.ConfigureKestrel(options =>
                    {
                                // Setup a HTTP/2 endpoint without TLS.
                                options.ListenLocalhost(81, o => o.Protocols =
                            HttpProtocols.Http2);
                    }); 
                    #endregion

                    webBuilder.UseStartup<Startup>();
                });
    }
}
