

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
                {
                    //注意这里 
                    services.AddHostedService<Worker1>();
                    services.AddHostedService<Worker2>();
                });
        /// <summary>
        /// 注意需要继承IHostedService
        /// </summary>
        public class Worker1 : IHostedService, IDisposable
        {
            private Timer _timer;
            public static int a = 0;
            public Task StartAsync(CancellationToken cancellationToken)
            {
                _timer = new Timer(dowork, null, TimeSpan.Zero,
                  TimeSpan.FromSeconds(2));//频率两秒一次
                return Task.CompletedTask;
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                _timer?.Change(Timeout.Infinite, 0);
                return Task.CompletedTask;
            }
            public void dowork(object state)
            {
                exec();
            }
            /// <summary>
            /// 执行代码块
            /// </summary>
            public void exec()
            {
                a++;
                Console.WriteLine("Worker1第{0}次执行", a);
            }
            public void Dispose()
            {
                _timer?.Dispose();
            }


        }



        /// <summary>
        /// 注意需要继承BackgroundService
        /// 实际 BackgroundService 继承自 IHostedService
        /// </summary>
        public class Worker2 : BackgroundService
        {

            /// <summary>
            /// 执行方法
            /// </summary>
            /// <param name="stoppingToken"></param>
            /// <returns></returns>
            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                int i = 1;
                //入参委托

                while (true)
                {
                    Console.WriteLine($"Worker2第{i++}次执行");
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }

            }

        }

    }
}
