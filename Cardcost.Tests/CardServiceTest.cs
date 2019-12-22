using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cardcost.Core.Services;
using Cardcost.Core.Services.interfaces;
using Moq;
using Xunit;

namespace Cardcost.Tests
{
    public class CardServiceTest
    {
        private ICardService _cardService;
        private Mock<HttpClient> _httpclient;

        public CardServiceTest()
        {
            _httpclient = new Mock<HttpClient>();
            _cardService = new CardService(_httpclient.Object);
        }

        [Theory]
        [InlineData("516838")]
        [InlineData("51683800")]
        public async Task SendCardNumberRequestForGreekCard(string cardNum)
        {
            var response = await _cardService.GetCardInfo(cardNum);
            // Assert
            var viewResult = Assert.IsType<Tuple<HttpStatusCode, int>>(response);
            Assert.Equal(new Tuple<HttpStatusCode, int>(HttpStatusCode.OK, 15), viewResult);
        }

        [Theory]
        [InlineData("4571 7360")]
        public async Task SendCardNumberRequestForForeignCard(string cardNum)
        {
            var response = await _cardService.GetCardInfo(cardNum);
            // Assert
            var viewResult = Assert.IsType<Tuple<HttpStatusCode, int>>(response);
            Assert.Equal(new Tuple<HttpStatusCode, int>(HttpStatusCode.OK, 10), viewResult);
        }

        [Theory]
        [InlineData("45717360")]
        public async Task SendCardNumberRequestForUsaCard(string cardNum)
        {
            var response = await _cardService.GetCardInfo(cardNum);
            // Assert
            var viewResult = Assert.IsType<Tuple<HttpStatusCode, int>>(response);
            Assert.Equal(new Tuple<HttpStatusCode, int>(HttpStatusCode.OK, 5), viewResult);
        }


    }
}
