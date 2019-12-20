using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cardcost.Controllers;
using Cardcost.Core.Services.interfaces;
using Cardcost.Domain;
using DotNetLiberty.AspNet.Testing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Xunit;

namespace Cardcost.Tests
{
    public class BasicTests : IClassFixture<WebApplicationTestFixture<Startup>>
    {
        private HttpClient _client;

        public BasicTests(WebApplicationTestFixture<Startup> fixture)
        {
            _client = fixture..;
            _client.BaseAddress = new Uri("https://localhost:5000");
        }
    

        [Theory]
        [InlineData("51683800")]
        public async Task Test1(string cardnum)
        {
            var card = new Card() { CardNumber = cardnum };
            var postData = new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json");
            //
            HttpResponseMessage response = await _client.PostAsync("/CardCost/payment_cards_cost", postData);
            //
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<int>(responseString);

            Assert.Equal(jsonResponse,actual: 10);

        }

        
    }
}
