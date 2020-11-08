using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApolloDemo
{
    public class Configs //or CustomConfig
    {
        //Configuration.GetSection("AllowedHosts").Get<List<string>>();
        //string connectionString = Configuration.GetSection("Quartz")["connectionString"];
        private static IConfiguration _configuration;
        public static void Init(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// 阿波罗配置中心
        /// </summary>
        public static string ApolloAbc => _configuration["abc"];
        public static string Aaa => _configuration["aaa"];
        public static string Abc =>  _configuration.GetSection("abc:abcd").Get<string>();
        /// <summary>
        /// 类对象
        /// </summary>
        public static Abc2 Abc2 => _configuration.GetSection("abc2").Get<Abc2>();
        //public static Abc2 Abc2z => _configuration.GetSection(nameof(Abc2)).Get<Abc2>();
        /*
        var configs = new List<OAuthConfiguration>();
        configuration.GetSection(nameof(OAuthConfiguration)).Bind(configs);
        */
    }

    public class Abc2
    {
        public  string Abcd2{ set; get; }
    }


}
