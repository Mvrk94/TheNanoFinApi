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

        //invalidate insurance products products

        public MConsumerController(int consumerUserID)
        {
            client = new MultiChainClient("188.166.170.248", 4396, false, "multichainrpc", "HFaiZ1WtPLAnuSNoA7VyTDL4C97D6dgWpcEdjNw1Jhjv", "NanoFinBlockchain");
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
                await user.grantPermissions(BlockchainPermissions.Connect, BlockchainPermissions.Send);
                //check recipient user has the correct permissions, if not grant permissions
                MUserController recipientUser = new MUserController(recipientUserID);
                recipientUser = await recipientUser.init();
                await recipientUser.grantPermissions(BlockchainPermissions.Receive);

                string recipientAddr = await MUtilityClass.getAddress(client, recipientUserID, BlockchainPermissions.Receive);


                string metadata = "Consumer \'" + user.propertyUserID() + "\' sent " + amount.ToString() + " Voucher " + " to consumer \'" + recipientUserID.ToString() + "\'";
                var sendWithMetaDataFrom = await client.SendWithMetadataFromAsync(user.propertyUserAddress(), recipientAddr, "Voucher", amount, MUtilityClass.strToHex(metadata));  //metadata has to be converted to hex. convert back to string online or with MUtilityClasss
                return true;
            }
            return false;
        }

        public async Task<bool> redeemVoucher(string insuranceProductName, int amount)
        {
            //if consumer has enough money - explicitly checked in consumer wallet handler
            string insuranceProductNameNoSpace = MUtilityClass.removeSpaces(insuranceProductName);

            string recipientAddr = user.propertyUserAddress();
            await user.grantPermissions(BlockchainPermissions.Connect, BlockchainPermissions.Receive, BlockchainPermissions.Send);
            //spend consumer voucher
            string metadata = "Consumer \'" + user.propertyUserID() + "\' spent " + amount.ToString() + " Voucher. Voucher to be redeemed for " + amount.ToString() + " " +insuranceProductName;
            var sendWithMetaDataFrom = await client.SendWithMetadataFromAsync(user.propertyUserAddress(), burnAddress, "Voucher", amount, MUtilityClass.strToHex(metadata));  //metadata has to be converted to hex. convert back to string online or with MUtilityClasss
            sendWithMetaDataFrom.AssertOk();

            

            if (await isProductOnBlockchain(insuranceProductName) == true)
            {
                //issue of insurance product to consumer
                var issueMore = await client.IssueMoreFromWithMetadataAsync(nanoFinAddr, recipientAddr, insuranceProductNameNoSpace, amount, "Issue consumer \'" + user.propertyUserID().ToString() + "\' " + amount.ToString() + " " + insuranceProductNameNoSpace);
                issueMore.AssertOk();
            }
            else
            {
                //issue new asset to user
                var issue = await client.IssueOpenWithMetadataFromAsync(nanoFinAddr, recipientAddr, insuranceProductNameNoSpace, amount, "Create insurance product asset " + insuranceProductNameNoSpace + ". This represents a product belonging to: 2Help1"); //get product proider name and maybe some 
                issue.AssertOk();
            }

            return true;
        }


        public async Task<bool> isProductOnBlockchain(string insuranceProductName)
        {
            insuranceProductName = MUtilityClass.removeSpaces(insuranceProductName);
            var assets = await client.ListAssetsAsync();
            assets.AssertOk();
            AssetResponse singleAssetResponse = null;
            foreach (var asset in assets.Result)
            {
                singleAssetResponse = asset;
                if (singleAssetResponse.Name.Equals(insuranceProductName))
                {
                    return true;
                }
            }
            return false;
        }



    }
}