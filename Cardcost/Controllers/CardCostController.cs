using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Cardcost.Core.Services;
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
        //private readonly RedisService _redisService;

        public CardCostController(ILogger<CardCostController> logger, ICardService cardService, IValidateCardNumber validateCardNumber/*, RedisService redisService*/)
        {
            _logger = logger;
            _cardService = cardService;
            _validateCardNumber = validateCardNumber;
            //_redisService = redisService;
        }

        [HttpPost, Route("payment_cards_cost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody]Card card)
        {
            //Validate the card number string
            var validation = await _validateCardNumber.Validate(card.CardNumber);
            //
            int cost = 0;
            var cardNumber = card.CardNumber.Replace(" ", "").Trim();
            //var existingCardCost = await _redisService.Get(cardNumber.Substring(0,6));
            //if (String.IsNullOrEmpty(existingCardCost))
            //{
            //    //Get the cost using public api and card number
            //    cost = await _cardService.GetCardInfo(cardNumber);
            //    //Save the firts 6 digits to redis in order to get in the future
            //    await _redisService.Set(cardNumber.Substring(0, 6), cost.ToString());
            //}
            //else
            //{
            //    cost = Convert.ToInt32(existingCardCost);
            //}

            //Get the cost using public api and card number
            cost = await _cardService.GetCardInfo(cardNumber);
            
            return Ok(cost);
        }
    }
}
