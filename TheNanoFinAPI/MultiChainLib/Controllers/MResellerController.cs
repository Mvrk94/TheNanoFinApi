using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNanoFinAPI.MultiChainLib.Controllers;

namespace MultiChainLib.Controllers
{
    public class MResellerController
    {
        MultiChainClient client;
        MUserController user;
        public MResellerController(int resellerUserID)
        {
            client = new MultiChainClient("188.166.170.248", 6492, false, "multichainrpc", "AYBR44NDe7VdSWXHJCrR2i2xhCcnByorzHy6f6vaczTd", "NanoFinBlockChain");
            user = new MUserController(resellerUserID, client);
        }

        public async void init()
        {
            user = await user.init();
        }

        public async void buyBulk(int amount)
        {
            user.grantPermissions(BlockchainPermissions.Connect, BlockchainPermissions.Send, BlockchainPermissions.Receive);
            String nanoFinAddr = await MUtilityClass.getAddress(1,client);
            var transaction = await client.IssueMoreFromAsync(nanoFinAddr, user.propertyUserAddress(), "BulkVoucher", amount);
            transaction.AssertOk();
        }


    }
}
