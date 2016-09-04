using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib.Model
{
    public class voucherJSON
    {
        public Dictionary<string, object> Values { get; private set; }

        public voucherJSON()
        {
            this.Values = new Dictionary<string, object>();
        }

        public int Voucher
        {
            get
            {
                return this.GetValue<int>("Voucher");
            }
            set
            {
                this.SetValue("Voucher", value);
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
