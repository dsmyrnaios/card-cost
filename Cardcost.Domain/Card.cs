using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cardcost.Domain
{
    public class Card
    {
        [JsonProperty("card_number")]
        public string CardNumber { get; set; }
    }
}
