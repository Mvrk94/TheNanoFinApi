using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheNanoFinAPI.MultiChainLib.Model
{
    public class MetadataJSON
    {

        public Dictionary<string, object> Values { get; private set; }

        public MetadataJSON()
        {
            this.Values = new Dictionary<string, object>();
        }

        public string metadata
        {
            get
            {
                return this.GetValue<string>("metadata");
            }
            set
            {
                this.SetValue("metadata", value);
            }
        }

        private void SetValue(string name, object value)
        {
            this.Values[name] = value;
        }

        public T GetValue<T>(string name)
        {
            if (this.Values.ContainsKey(name))
                return (T)this.Values[name];
            else
                return default(T);
        }

    }
}