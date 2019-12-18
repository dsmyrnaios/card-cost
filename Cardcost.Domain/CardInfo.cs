using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Cardcost.Domain
{
    [JsonObject]
    public class CardInfo
    {
        [JsonProperty("scheme")]
        public string Scheme { get; set; }

        [JsonProperty("type")]
        public string CardType { get; set; }

        [JsonProperty("brand")]
        public string CardBrand { get; set; }

        [JsonProperty("prepaid")]
        public bool Prepaid { get; set; }

        [JsonProperty("number")]
        public NumInfo NumInfo { get; set; }

        [JsonProperty("bank")]
        public Bank Bank { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }
    }
}
