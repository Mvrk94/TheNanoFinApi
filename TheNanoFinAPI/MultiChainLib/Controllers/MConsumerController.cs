using MultiChainLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TheNanoFinAPI.MultiChainLib.Controllers
{
    public class MConsumerController
    {

        MultiChainClient client;
        MUserController user;
        string nanoFinAddr;
        string burnAddress;

        public MConsumerController(int consumerUserID)
        {
            client = new MultiChainClient("188.166.170.248", 6492, false, "multichainrpc", "AYBR44NDe7VdSWXHJCrR2i2xhCcnByorzHy6f6vaczTd", "NanoFinBlockChain");
            user = new MUserController(consumerUserID, client);
        }

        public async Task<MConsumerController> init()
        {
            user = await user.init();
            nanoFinAddr = await MUtilityClass.getAddress(client, 1);
            burnAddress = await MUtilityClass.getBurnAddress(client);
            return this;
        }


        public async Task<bool> sendVoucherToConsumer(int recipientUserID, int amount)
        {
            //check if current user has enough to send
            if(await MUtilityClass.hasAssetBalance(client, user.propertyUserID(), "Voucher", amount) == true)
            {
                //check if current user has the correct permissions, if not grant permissions
                user.grantPermissions(BlockchainPermissions.Connect, BlockchainPermissions.Send);
                //check recipient user has the correct permissions, if not grant permissions
                MUserController recipientUser = new MUserController(recipientUserID);
                recipientUser = await recipientUser.init();
                recipientUser.grantPermissions(BlockchainPermissions.Receive);

                string recipientAddr = await MUtilityClass.getAddress(client, recipientUserID);


                string metadata = "Consumer \'" + user.propertyUserID() + "\' sent " + amount.ToString() + " Voucher " + " to consumer \'" + recipientUserID.ToString() + "\'";
                var sendWithMetaDataFrom = await client.SendWithMetadataFromAsync(user.propertyUserAddress(), recipientAddr, "Voucher", amount, MUtilityClass.strToHex(metadata));  //metadata has to be converted to hex. convert back to string online or with MUtilityClasss
                return true;
            }
            return false;
        }

       



    }
}