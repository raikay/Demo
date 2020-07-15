using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ConfigDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddMyConfiguration();

            var configRoot = builder.Build();

            Microsoft.Extensions.Primitives.ChangeToken.OnChange(() => configRoot.GetReloadToken(), () =>
            {
                Console.WriteLine($"lastTime:{configRoot["lastTime"]}");
            });

            Console.WriteLine("开始了");
            Console.ReadKey();
        }
    }
}
