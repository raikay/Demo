﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ApolloDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get([FromServices] IConfiguration configuration)
        {
            return configuration["abc"];
        }

        [HttpGet("GetDis")]
        public IActionResult GetDis([FromServices] IDistributedCache cache, /*[FromServices] IMemoryCache memoryCache, [FromServices] IEasyCachingProvider easyCaching,*/ [FromQuery] string query)
        {

            #region IDistributedCache
            var key = $"GetDis-{query ?? ""}";
            var time = cache.GetString(key);
            if (string.IsNullOrEmpty(time)) //此处需要考虑并发情形
            {
                var option = new DistributedCacheEntryOptions();
                time = "time-val-apollo";
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