using MultiChainLib.Model;
using Newtonsoft.Json;
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
        string nanoFinAddr;
        string burnAddress;
        public MResellerController(int resellerUserID)
        {
            client = new MultiChainClient("188.166.170.248", 6492, false, "multichainrpc", "AYBR44NDe7VdSWXHJCrR2i2xhCcnByorzHy6f6vaczTd", "NanoFinBlockChain");
            user = new MUserController(resellerUserID, client);
        }

        public async Task<MResellerController> init()
        {
            user = await user.init();
            nanoFinAddr = await MUtilityClass.getAddress(client, 1);
            burnAddress = await MUtilityClass.getBurnAddress(client);
            return this;
        }
        
        public async void buyBulk(int amount)
        {
            //check if current user has the correct permissions, if not grant permissions
            user.grantPermissions(BlockchainPermissions.Connect, BlockchainPermissions.Send, BlockchainPermissions.Receive);
            //issue reseller bulk voucher of amount specified, transaction contains metadata
            var issueMore = await client.IssueMoreFromWithMetadataAsync(nanoFinAddr, user.propertyUserAddress(), "BulkVoucher", amount, "Issue reseller \'" + user.propertyUserID().ToString() + "\' " + amount.ToString() + " BulkVoucher.");
            issueMore.AssertOk();
        }

        public async Task<int> sendBulk(int recipientUserID, int amount)
        {
            string recipientAddr = await MUtilityClass.getAddress(client, recipientUserID);
            //spend reseller BulkVoucher inputs
            string metadata = "Reseller \'" + user.propertyUserID() + "\' spent " + amount.ToString() + " BulkVoucher. " + amount.ToString() + " Voucher of same amount to be issued to user \'" + recipientUserID.ToString() + "\'.";
            var sendWithMetaDataFrom = await client.SendWithMetadataFromAsync(user.propertyUserAddress(), burnAddress, "BulkVoucher", amount, MUtilityClass.strToHex(metadata));  //metadata has to be converted to hex. convert back to string online or with MUtilityClasss
            sendWithMetaDataFrom.AssertOk();

            //check if recipient has the correct permissions, if not grant permissions
            MUserController recipientUser = new MUserController(recipientUserID);
            recipientUser = await recipientUser.init();
            recipientUser.grantPermissions(BlockchainPermissions.Receive);
            //issue recipient voucher
            var issueMore = await client.IssueMoreFromWithMetadataAsync(nanoFinAddr, recipientAddr, "Voucher", amount, "Issue user \'" + recipientUserID.ToString() + "\' " + amount.ToString() + " Voucher");
            issueMore.AssertOk();

            return 0;
        }


        public async Task atomicVoucherExchange(int resellerUserID, int bulkVoucherAmount, int UserID, int voucherAmount )
        {

            var jVoucher = new voucherJSON()
            {
                Voucher = voucherAmount
            };

            var jBulkVoucher = new bulkVoucherJSON()
            {
                BulkVoucher = bulkVoucherAmount
            };

            var voucherJsonStr = JsonConvert.SerializeObject(jVoucher.Values);
            var bulkVouckerJsonStr = JsonConvert.SerializeObject(jBulkVoucher.Values);
            await MUtilityClass.atomicExchange(client, resellerUserID, bulkVouckerJsonStr, UserID, voucherJsonStr);
        }


    }
}
