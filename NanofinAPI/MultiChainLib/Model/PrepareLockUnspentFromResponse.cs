using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib.Model
{
    public class PrepareLockUnspentFromResponse
    {
        [JsonProperty("txid")]
        public string txid { get; set; }

        [JsonProperty("vout")]
        public int vout { get; set; }
    }
}
