using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cardcost.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cardcost.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilter))]
    [ApiController]
    [Route("[controller]")]
    public class CardCostController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CardCostController> _logger;

        public CardCostController(ILogger<CardCostController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public virtual async Task<IActionResult> PostAsync([FromBody]string cardNumber)
        {
            //Check if
            if (String.IsNullOrWhiteSpace(cardNumber))
                throw new Exception("No data was given");

            //if(String.IsNullOrWhiteSpace(cardNum))

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
