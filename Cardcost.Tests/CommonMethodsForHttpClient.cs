using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cardcost.Domain;
using Newtonsoft.Json;

namespace Cardcost.Tests
{
    public class CommonMethodsForHttpClient
    {
        private HttpClient _client;

        public CommonMethodsForHttpClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<object> HttpPostPut(string url, Card entity)
        {
            var postData = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            //
            HttpResponseMessage response;
            response = await _client.PostAsync(url, postData);
            //
            if (response.IsSuccessStatusCode)
                return response.StatusCode;
            //
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<object>(responseString);

            return jsonResponse;
        }
    }
}
