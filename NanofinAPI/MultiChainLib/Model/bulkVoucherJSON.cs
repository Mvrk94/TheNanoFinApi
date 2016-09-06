using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib.Model
{
    public class bulkVoucherJSON
    {

        public Dictionary<string, object> Values { get; private set; }

        public bulkVoucherJSON()
        {
            this.Values = new Dictionary<string, object>();
        }

        public int BulkVoucher
        {
            get
            {
                return this.GetValue<int>("BulkVoucher");
            }
            set
            {
                this.SetValue("BulkVoucher", value);
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
