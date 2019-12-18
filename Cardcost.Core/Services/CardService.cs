using System;
using System.Collections.Generic;
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

        public async Task<CardInfo> GetCardInfo(string cardNum)
        {
            //var uri = API.Catalog.GetAllCatalogItems(_remoteServiceBaseUrl,
            //    page, take, brand, type);

            var responseString = await _httpClient.GetStringAsync($"https://lookup.binlist.net/{cardNum}");

            var catalog = JsonConvert.DeserializeObject<CardInfo>(responseString);
            return catalog;
        }
    }
}
