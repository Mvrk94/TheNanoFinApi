using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheNanoFinAPI.MultiChainLib.Model
{
    public class IssueAssetParamsJSON
    {
        public Dictionary<string, object> Values { get; private set; }

        public IssueAssetParamsJSON()
        {
            this.Values = new Dictionary<string, object>();
        }

        public string name
        {
            get
            {
                return this.GetValue<string>("name");
            }
            set
            {
                this.SetValue("name", value);
            }
        }

        public bool open
        {
            get
            {
                return this.GetValue<bool>("open");
            }
            set
            {
                this.SetValue("open", value);
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