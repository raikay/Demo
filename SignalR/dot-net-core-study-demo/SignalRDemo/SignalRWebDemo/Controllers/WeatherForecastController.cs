using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignalRWebDemo.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRWebDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IHubContext<MyHub> _hubContext;
        private readonly ILogger<WeatherForecastController> _logger;
        private static int count = 0;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHubContext<MyHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }
        [HttpGet("PushMsg")]
        public async Task<IActionResult> PushMsg([FromQuery]string content)
        {
            await _hubContext.Clients.All.SendAsync("ShowMsg",new MsgInfo { Title = "TestTitle", MsgContent = content + count++ });
            return Ok();
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
    }
}
