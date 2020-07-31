using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace RedisDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }




        //app.UseResponseCaching();
        //[ResponseCache(Duration = 6000, VaryByQueryKeys = new string[] { "query" })]
        public dynamic GetOrder([FromQuery] string query)
        {
            return new  { Id = 100, Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        }

        public IActionResult GetDis([FromServices] IDistributedCache cache, /*[FromServices] IMemoryCache memoryCache, [FromServices] IEasyCachingProvider easyCaching,*/ [FromQuery] string query)
        {
            //redis的问题一直没连接上，感觉代码应该没问题了
            #region IDistributedCache
            var key = $"GetDis-{query ?? ""}";
            var time = cache.GetString(key);
            if (string.IsNullOrEmpty(time)) //此处需要考虑并发情形
            {
                var option = new DistributedCacheEntryOptions();
                time = DateTime.Now.ToString();
                cache.SetString(key, time, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600) });
            }
            #endregion

            #region IEasyCachingProvider
            //var key = $"GetDis-{query ?? ""}";
            //var time = easyCaching.Get(key, () => DateTime.Now.ToString(), TimeSpan.FromSeconds(600));


            #endregion

            return Content("abc" + time);
        }
    }
}
