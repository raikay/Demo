using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace SerilogDemo
{
public class Program
{
    //public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
    //                                                            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
    //                                                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //                                                            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    //                                                            .AddEnvironmentVariables()
    //                                                            .Build();
    public static void Main(string[] args)
    {
        #region Serilog
        var logConfig = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(logConfig)
                        .MinimumLevel.Debug()
                        .Enrich.FromLogContext()
                        .WriteTo.Console(new Serilog.Formatting.Compact.RenderedCompactJsonFormatter())
                        .WriteTo.File(formatter: new Serilog.Formatting.Compact.CompactJsonFormatter(), "logs\\myapp.txt", rollingInterval: RollingInterval.Day)
                        .CreateLogger(); 
        #endregion
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).UseSerilog(dispose: true);
}
}
