using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cardcost.Core.Services.interfaces;
using Cardcost.Domain;
using Newtonsoft.Json;

namespace Cardcost.Core.Services
{
    public class CardService: ICardService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl = "https://lookup.binlist.net/";

        public CardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetCardInfo(string cardNum)
        {
            //trim the string and remove all space characters
            cardNum = cardNum.Replace(" ", "").Trim();

            var response = await _httpClient.GetAsync($"{_remoteServiceBaseUrl}{cardNum}");

            try
            {
                //Check if the api returns accepted status code 
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw  new ApiException(ex, (int)response.StatusCode);
            }

            //
            var responseBody = await response.Content.ReadAsStringAsync();
            var cardInfo = JsonConvert.DeserializeObject<CardInfo>(responseBody);

            int cost = 0;

            switch (cardInfo.Country.CountryCode)
            {
                case "GR":
                    cost = 15;
                    break;
                case "US":
                    cost = 5;
                    break;
                default:
                    cost = 10;
                    break;
            }

            return cost;
        }
    }
}
