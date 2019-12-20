using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Cardcost.Core.Services.interfaces;
using Cardcost.Core.ValidationRules.Interfaces;
using Cardcost.Domain;
using Cardcost.ErrorHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cardcost.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(ApiExceptionFilter))]
    [Route("[controller]")]
    public class CardCostController : ControllerBase
    {
        private readonly ILogger<CardCostController> _logger;
        private readonly ICardService _cardService;
        private readonly IValidateCardNumber _validateCardNumber;

        public CardCostController(ILogger<CardCostController> logger, ICardService cardService, IValidateCardNumber validateCardNumber)
        {
            _logger = logger;
            _cardService = cardService;
            _validateCardNumber = validateCardNumber;
        }

        [HttpPost, Route("payment_cards_cost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody]Card Card)
        {
            //Validation
            var validation = await _validateCardNumber.Validate(Card.CardNumber);

            var a = await _cardService.GetCardInfo(Card.CardNumber);

            if (a.Item1 == HttpStatusCode.NotFound)
                return NotFound();

            if (a.Item1 == HttpStatusCode.BadRequest)
                return BadRequest();

            return Ok(a.Item2);
        }
    }
}
