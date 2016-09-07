using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib.Model
{
    public class AppendRawExchangeResponse
    {
        [JsonProperty("hex")]
        public string hex { get; set; }

        [JsonProperty("complete")]
        public bool complete { get; set; }
    }
}
