using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib.Model
{


    public class Offer
    {

        [JsonProperty("amount")]
        public double amount { get; set; }

        [JsonProperty("assets")]
        public IList<object> assets { get; set; }
    }

    public class Asset
    {

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("assetref")]
        public string assetref { get; set; }

        [JsonProperty("qty")]
        public double qty { get; set; }
    }

    public class Ask
    {

        [JsonProperty("amount")]
        public double amount { get; set; }

        [JsonProperty("assets")]
        public IList<Asset> assets { get; set; }
    }

    public class Result
    {

        [JsonProperty("offer")]
        public Offer offer { get; set; }

        [JsonProperty("ask")]
        public Ask ask { get; set; }

        [JsonProperty("requiredfee")]
        public double requiredfee { get; set; }

        [JsonProperty("candisable")]
        public bool candisable { get; set; }

        [JsonProperty("cancomplete")]
        public bool cancomplete { get; set; }

        [JsonProperty("complete")]
        public bool complete { get; set; }
    }

    public class DecodeRawExchangeResponse
    {

        [JsonProperty("result")]
        public Result result { get; set; }

        [JsonProperty("error")]
        public object error { get; set; }

        [JsonProperty("id")]
        public object id { get; set; }
    }


    //public class DecodeRawExchangeResponse
    //{
    //    [JsonProperty("offer")]
    //    public Offer offer { get; set; }
    //    [JsonProperty("ask")]
    //    public Ask ask { get; set; }
    //    [JsonProperty("completedfee")]
    //    public double completedfee { get; set; }
    //    [JsonProperty("requiredfee")]
    //    public double requiredfee { get; set; }
    //    [JsonProperty("candisable")]
    //    public bool candisable { get; set; }
    //    [JsonProperty("cancomplete")]
    //    public bool cancomplete { get; set; }
    //    [JsonProperty("complete")]
    //    public bool complete { get; set; }
    //} // end class DecodeRawExchangeResponse

    //public class Offer
    //{
    //    [JsonProperty("amount")]
    //    public double amount { get; set; }
    //    [JsonProperty("assets")]
    //    public List<Asset> assets { get; set; }
    //}
    //public class Ask
    //{
    //    [JsonProperty("amount")]
    //    public double amount { get; set; }
    //    [JsonProperty("assets")]
    //    public List<Asset> assets { get; set; }
    //}

    //public class Asset
    //{
    //    [JsonProperty("name")]
    //    public double name { get; set; }
    //    [JsonProperty("assetref")]
    //    public double asseterf { get; set; }
    //    [JsonProperty("qty")]
    //    public double qty { get; set; }

    //}

}
