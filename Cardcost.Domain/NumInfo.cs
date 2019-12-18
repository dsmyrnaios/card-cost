using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cardcost.Domain
{
    public class NumInfo
    {
        [JsonProperty("length")]
        public int CardLength { get; set; }

        [JsonProperty("luhn")]
        public bool Luhn { get; set; }
    }
}
