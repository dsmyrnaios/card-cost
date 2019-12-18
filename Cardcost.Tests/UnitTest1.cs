using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cardcost.Domain;
using Newtonsoft.Json;
using NUnit.Framework;
using Xunit;

namespace Cardcost.Tests
{
    public class Tests
    {
        private HttpClient _client;

        public Tests(HttpClient client)
        {
            _client = client;
        }

        [SetUp]
        public void Setup()
        {
        }

        [Xunit.Theory]
        [InlineData("51683800")]
        [InlineData("4571 7360")]
        public async void TestWhereWithoutParenthesis(string cardNum)
        {
            
            var result = await HttpPost($"http://localhost:5000/CardCost/payment_cards_cost", cardNum);
            //var archives = await _mediator.Send(new GenericCrud<Archive>.Query() {
            //    UserClaim = CommonMethodsForTests.GetAdminUserClaim(),
            //    StatusTimeConvertion = Domain.ModelEnums.EStatusTimeConverter.Output});
            ////
            //archives.Data.Should().NotBeNull();
            ////
            //var archive = archives.Data.First();

            //should succeed
            result.
        }





        private async Task<IActionResult> HttpPost(string url, string entity)
        {
            var postData = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            //
            HttpResponseMessage response;
            response = await _client.PostAsync(url, postData);
            
            //
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<CardInfo>(responseString);

            return jsonResponse;
        }
    }
}