using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cardcost.Controllers;
using Cardcost.Core.Services.interfaces;
using Cardcost.Core.ValidationRules.Interfaces;
using Cardcost.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Cardcost.Tests
{
    public class BasicTests
    {
        private CardCostController _cardCostController;
        private Mock<ICardService> _cardService;
        private Mock<IValidateCardNumber> _validateCardNumber;

        public BasicTests()
        {
            _cardService = new Mock<ICardService>();
            _validateCardNumber = new Mock<IValidateCardNumber>();
            var logger = new Mock<ILogger<CardCostController>>();
            _cardCostController = new CardCostController(logger.Object, _cardService.Object, _validateCardNumber.Object);
        }

        
        [Theory]
        [InlineData("51683800")]
        [InlineData("516838")]
        [InlineData("11111111")]
        public async Task StatusCodeAccepted_Greek_Card_Test(string cardNum)
        {
            _validateCardNumber.Setup(service => service.Validate(It.IsAny<string>()))
                .Returns<string>(parameters => Task.FromResult(true));
            _cardService.Setup(service => service.GetCardInfo(It.IsAny<string>()))
                .Returns<string>(parameters => Task.FromResult(It.IsAny<int>()));
            
            var card = new Card() { CardNumber = cardNum };
            //
            var response = await _cardCostController.Post(card);
            //
            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, viewResult.StatusCode);
            //
        }

        
    }
}
