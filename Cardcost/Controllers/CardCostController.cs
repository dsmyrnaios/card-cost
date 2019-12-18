using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cardcost.Domain;
using Cardcost.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cardcost.Controllers
{
    //[ServiceFilter(typeof(ApiExceptionFilter))]
    [ApiController]
    [Route("[controller]")]
    public class CardCostController : ControllerBase
    {
        private readonly ILogger<CardCostController> _logger;

        public CardCostController(ILogger<CardCostController> logger)
        {
            _logger = logger;
        }

        [HttpPost, Route("payment_cards_cost")]
        public virtual async Task<IActionResult> Post([FromBody]Card Card)
        {
            //Check if
            if (String.IsNullOrWhiteSpace(Card.CardNumber))
                throw new Exception("No data was given");

            //if(String.IsNullOrWhiteSpace(cardNum))

            return Ok();
        }
    }
}
