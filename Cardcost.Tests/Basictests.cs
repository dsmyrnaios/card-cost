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
        [InlineData("5168")]
        public async Task Test1(string cardnum)
        {
            _cardService.Setup(service => service.GetCardInfo(It.IsAny<string>()))
                .Returns<string>(parameters => Task.FromResult(new Tuple<HttpStatusCode, int>(HttpStatusCode.Accepted, 10)));
            _validateCardNumber.Setup(service => service.Validate(It.IsAny<string>()))
                .Returns<string>(parameters => Task.FromResult(false));
            var card = new Card() { CardNumber = cardnum };
            //var postData = new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json");
            //
            var response = await _cardCostController.Post(card);
            //
            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, viewResult.StatusCode);
            //
            //var responseString = await response.Content.ReadAsStringAsync();
            //var jsonResponse = JsonConvert.DeserializeObject<int>(responseString);

            //Assert.Equal(jsonResponse,actual: 10);

        }

        
    }
}
