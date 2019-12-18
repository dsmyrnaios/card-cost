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
        private readonly string _remoteServiceBaseUrl;

        public CardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Tuple<HttpStatusCode, int>> GetCardInfo(string cardNum)
        {
            //
            cardNum = cardNum.Replace(" ", "").Trim();

            var responseStatusCode = await _httpClient.GetAsync($"https://lookup.binlist.net/{cardNum}");
            
            if (responseStatusCode.StatusCode != HttpStatusCode.OK)
                return new Tuple<HttpStatusCode, int>(responseStatusCode.StatusCode, 0);

            var responseString = await _httpClient.GetStringAsync($"https://lookup.binlist.net/{cardNum}");

            var cardInfo = JsonConvert.DeserializeObject<CardInfo>(responseString);

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

            return new Tuple<HttpStatusCode, int>(responseStatusCode.StatusCode, cost);
        }
    }
}
