using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace AutofacDemo
{
    public class ConfigHelper
    {
        public static IConfiguration Configuration { set; get; }
        static ConfigHelper()
        {
            Configuration = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
                .Build();
            //Configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json").Build();

        }

        public static string Get(string key)
        {
            //var val = ConfigHelper.Configuration[key];
            var val = ConfigHelper.Configuration.GetSection(key).Value;
            return val;
        }


    }
}
