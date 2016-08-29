using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib.Controllers
{
    public class ResellerController
    {
        MultiChainClient client;
        public ResellerController()
        {
            client = new MultiChainClient("188.166.170.248", 6492, false, "multichainrpc", "AYBR44NDe7VdSWXHJCrR2i2xhCcnByorzHy6f6vaczTd", "NanoFinBlockChain");
        }

        public async void buyBulk(int resellerID, decimal amount)
        {

        }

    }
}
