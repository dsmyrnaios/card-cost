using System;
using Newtonsoft.Json;

namespace Cardcost.Domain
{
    [JsonObject]
    public class Country
    {
        [JsonProperty("numeric")]
        public string Numeric { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alpha2")]
        public string CountryCode { get; set; }
        
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("emoji")]
        public string Emoji { get; set; }

        [JsonProperty("latitude")]
        public long Latitude { get; set; }

        [JsonProperty("longitude")]
        public long Longitude { get; set; }
    }
}
