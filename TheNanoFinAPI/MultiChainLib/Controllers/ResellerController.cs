using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib.Controllers
{
    public class ResellerController
    {
        

        public ResellerController()
        {
           
        }

        async Task initJSONRpcClient()
        {
           
        }

        public async Task<List<PeerResponse>> test()
        {
            var client = new MultiChainClient("188.166.170.248", 6492, false, "multichainrpc", "AYBR44NDe7VdSWXHJCrR2i2xhCcnByorzHy6f6vaczTd", "NanoFinBlockChain");
            var peers = await client.GetPeerInfoAsync();
            peers.AssertOk();
            List<PeerResponse> peerList = new List<PeerResponse>();
            foreach (var peer in peers.Result)
            {
                peerList.Add(peer);
            }
            return peerList;
        }



        private bool resellerHasAddress()
        {
            return false;
        }

        private void getNewAddress(int resellerID)
        {

        }

        private void givePermissions(int resellerID)
        {

        }


    }
}
