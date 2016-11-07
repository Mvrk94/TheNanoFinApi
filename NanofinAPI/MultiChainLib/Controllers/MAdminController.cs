using MultiChainLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TheNanoFinAPI.MultiChainLib.Controllers;

namespace NanofinAPI.MultiChainLib.Controllers
{
    public class MAdminController
    {
        MultiChainClient client;
        MUserController user;
        string nanoFinAddr;
        string burnAddress;

        private decimal totalBulkVoucher = 0;
        private decimal totalVoucher = 0;
        private decimal totalAssets = 0;

        public MAdminController(int consumerUserID)
        {
            client = new MultiChainClient("188.166.170.248", 2748, false, "multichainrpc", "7yPU3yrroGZp4WAgrL2cD9JDe7WbwwiUmLps3PPmPPde", "NanoFinBlockchain");
            user = new MUserController(consumerUserID, client);
        }

        public async Task<MAdminController> init()
        {
            user = await user.init();
            nanoFinAddr = await MUtilityClass.getAddress(client, 1);
            burnAddress = await MUtilityClass.getBurnAddress(client);
            return this;
        }

        public async Task<List<AssetResponse>>  getListedAssets()
        {
            List<AssetResponse> assetList = new List<AssetResponse>();
            var assets = await client.ListAssetsAsync();
            assets.AssertOk();
            foreach (var walk in assets.Result)
            {
                if(walk.Name == "Voucher")
                {
                    totalBulkVoucher = walk.IssueQty;
                }
                else if (walk.Name == "BulkVoucher")
                {
                    totalBulkVoucher = walk.IssueQty;
                }
                else
                {
                    
                }
                    assetList.Add(walk);
            }

            return assetList;
        }




    }
}