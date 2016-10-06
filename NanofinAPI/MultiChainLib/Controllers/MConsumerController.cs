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
            client = new MultiChainClient("188.166.170.248", 2748, false, "multichainrpc", "7yPU3yrroGZp4WAgrL2cD9JDe7WbwwiUmLps3PPmPPde", "NanoFinBlockchain");
            user = new MUserController(consumerUserID, client);
        }

        public async Task<MConsumerController> init()
        {
            user = await user.init();
            nanoFinAddr = await MUtilityClass.getAddress(client, 1);
            burnAddress = await MUtilityClass.getBurnAddress(client);
            return this;
        }

        //consumer send amount voucher to consumer.
        public async Task<bool> sendVoucherToConsumer(int recipientUserID, int amount)
        {
            //check if current user has enough to send
            if(await MUtilityClass.hasAssetBalance(client, user.propertyUserID(), "Voucher", amount) == true)
            {

                        //await user.grantPermissions(BlockchainPermissions.Connect, BlockchainPermissions.Send);
                //check recipient user has the correct permissions, if not grant permissions
                //MUserController recipientUser = new MUserController(recipientUserID);
                //recipientUser = await recipientUser.init();
                //await recipientUser.grantPermissions(BlockchainPermissions.Receive);

                // string recipientAddr = await MUtilityClass.getAddress(client, recipientUserID, BlockchainPermissions.Receive);

                string recipientAddr = await MUtilityClass.getAddress(client, recipientUserID);

                string metadata = "Consumer \'" + user.propertyUserID() + "\' sent " + amount.ToString() + " Voucher " + " to consumer \'" + recipientUserID.ToString() + "\'";
                var sendWithMetaDataFrom = await client.SendWithMetadataFromAsync(user.propertyUserAddress(), recipientAddr, "Voucher", amount, MUtilityClass.strToHex(metadata));  //metadata has to be converted to hex. convert back to string online or with MUtilityClasss
                return true;
            }
            return false;
        }
        //spend consumer voucher by sending it to nanofin voucher pool. issue consumer insurance product
        public async Task<bool> redeemVoucher(string insuranceProductName, int amount)
        {
            //if consumer has enough money - explicitly checked in consumer wallet handler
            string insuranceProductNameNoSpace = MUtilityClass.removeSpaces(insuranceProductName);

            string recipientAddr = user.propertyUserAddress();
            //await user.grantPermissions(BlockchainPermissions.Connect, BlockchainPermissions.Receive, BlockchainPermissions.Send);
            //SPEND CONSUMER VOUCHER. Voucher goes to NanoFin pool - Voucher dealt with further once product expires (spent - burn addr) or is refunded (nack to consumer)
            string metadata = "Consumer \'" + user.propertyUserID() + "\' spent " + amount.ToString() + " Voucher. Voucher to be redeemed for " + amount.ToString() + " " + insuranceProductNameNoSpace;
            var sendWithMetaDataFrom = await client.SendWithMetadataFromAsync(user.propertyUserAddress(), nanoFinAddr, "Voucher", amount, MUtilityClass.strToHex(metadata));  //metadata has to be converted to hex. convert back to string online or with MUtilityClasss
            sendWithMetaDataFrom.AssertOk();

            

            if (await isProductOnBlockchain(insuranceProductNameNoSpace) == true)
            {
                //issue of insurance product to consumer
                var issueMore = await client.IssueMoreFromWithMetadataAsync(nanoFinAddr, recipientAddr, insuranceProductNameNoSpace, amount, "Issue consumer \'" + user.propertyUserID().ToString() + "\' " + amount.ToString() + " " + insuranceProductNameNoSpace);
                issueMore.AssertOk();
            }
            else
            {
                //issue new asset to user
                string issuanceMetadata = "Create insurance product " + insuranceProductNameNoSpace + " asset on the NanoFin blockchain. This asset represents a product belonging to: " + await MUtilityClass.getProductProviderName(insuranceProductNameNoSpace);
                var issue = await client.IssueOpenWithMetadataFromAsync(nanoFinAddr, recipientAddr, insuranceProductNameNoSpace, amount, issuanceMetadata); 
                issue.AssertOk();
            }

            return true;
        }

        //send voucher from nanofin voucher pool back to consumer. burn insurance product asset customer currently owns
        public async Task<bool> refundConsumer(string insuranceProductName, int amount)
        {
            string insuranceProductNameNoSpace = MUtilityClass.removeSpaces(insuranceProductName);

            //await user.grantPermissions(BlockchainPermissions.Connect, BlockchainPermissions.Receive, BlockchainPermissions.Send);

            //refund consumer voucher balance - send asset from nanoFin pool
            string consMetadata = "Consumer \'" + user.propertyUserID() + "\' received " + amount.ToString() + " Voucher. *Voucher refund -  " + insuranceProductNameNoSpace + " was rejected by the product provider.* ";
            var sendWithMetaDataFrom = await client.SendWithMetadataFromAsync(nanoFinAddr, user.propertyUserAddress(), "Voucher", amount, MUtilityClass.strToHex(consMetadata));  //metadata has to be converted to hex. convert back to string online or with MUtilityClasss
            sendWithMetaDataFrom.AssertOk();

            //spend (insurance) asset belonging to consumer. Insurance asset burned
            string InsMetadata = "Refund consumer\'" + user.propertyUserID() + "\' - consumer rejected to redeem" + insuranceProductNameNoSpace + ". Burn " + amount.ToString() + " " + insuranceProductNameNoSpace + ".";
            var insSendWithMetaDataFrom = await client.SendWithMetadataFromAsync(user.propertyUserAddress(), burnAddress, insuranceProductNameNoSpace, amount, MUtilityClass.strToHex(InsMetadata));  //metadata has to be converted to hex. convert back to string online or with MUtilityClasss
            sendWithMetaDataFrom.AssertOk();
            return true;
        }

        //product expiration: burn consumer insurance product asset. STILL TODO check if IM has been paid - if not send voucher from nanofin voucher pool to IM.
        public async Task<bool> invalidateProduct(string insuranceProductName, int amount)
        {
            string insuranceProductNameNoSpace = MUtilityClass.removeSpaces(insuranceProductName);

            //await user.grantPermissions(BlockchainPermissions.Connect, BlockchainPermissions.Receive, BlockchainPermissions.Send);

            ////NOTE CHECK IF IM HAS BEEN PAID
            ////spend product worth of voucher from NanoFin pool
            //string consMetadata = "Product " + insuranceProductNameNoSpace + " expired. Burn " + amount.ToString() + " Voucher - from general NanoFin Voucher Asset pool. " + amount.ToString() ;
            //var sendWithMetaDataFrom = await client.SendWithMetadataFromAsync(nanoFinAddr, burnAddress, "Voucher", amount, MUtilityClass.strToHex(consMetadata));  //metadata has to be converted to hex. convert back to string online or with MUtilityClasss
            //sendWithMetaDataFrom.AssertOk();

            //spend (insurance) asset belonging to consumer. Insurance asset burned
            string InsMetadata = "Product " + insuranceProductNameNoSpace + " expired. Burn " + amount.ToString() + " " + insuranceProductNameNoSpace + " - from consumer \'" + user.propertyUserID() + "\'. ";
            var insSendWithMetaDataFrom = await client.SendWithMetadataFromAsync(user.propertyUserAddress(), burnAddress, insuranceProductNameNoSpace, amount, MUtilityClass.strToHex(InsMetadata));  //metadata has to be converted to hex. convert back to string online or with MUtilityClasss
            insSendWithMetaDataFrom.AssertOk();
            return true;
        }
        //redeem accepted by insurance manager: Burn  voucher from NanoFin Voucher pool. issue product provider (amount) ie represent IM payment on Blockchain
        public async Task<bool> acceptRedeem(string insuranceProductName, int amount)
        {
            string insuranceProductNameNoSpace = MUtilityClass.removeSpaces(insuranceProductName);
            string productProvider =  MUtilityClass.removeSpaces(await MUtilityClass.getProductProviderName(insuranceProductName)); // product provider name is the name of the asset on the blockchain.
            string productProviderAddress = await MUtilityClass.getProductProviderAddress(client, productProvider);

           // await user.grantPermissions(BlockchainPermissions.Connect, BlockchainPermissions.Receive, BlockchainPermissions.Send);

            //burn amount worth of voucher from NanoFin pool
            string consMetadata = "Product " + insuranceProductNameNoSpace + " (belonging to consumer\'" + user.propertyUserID() + "\' - for amount "+amount.ToString() +") accepted by " + productProvider + ". Burn " + amount.ToString() + " Voucher - from general NanoFin Voucher Asset pool. " + amount.ToString()+ " to be paid to " + productProvider;
            var sendWithMetaDataFrom = await client.SendWithMetadataFromAsync(nanoFinAddr, burnAddress, "Voucher", amount, MUtilityClass.strToHex(consMetadata));  //metadata has to be converted to hex. convert back to string online or with MUtilityClasss
            sendWithMetaDataFrom.AssertOk();

            //issue more asset belonging to product provier - represents how much Rand (Zar) NanoFin has paid to the product provider.
            if (await isProductOnBlockchain(productProvider) == true)
            {
                string metadata = "Issue product provider " + productProvider + " " + amount + " of asset: " + productProvider + ". This asset represents the amount of money (South African - Zar) NanoFin has paid the company " + productProvider;
                var issueMore = await client.IssueMoreFromWithMetadataAsync(nanoFinAddr, productProviderAddress, productProvider, amount, metadata );
                issueMore.AssertOk();
            }
            else
            {
                string metadata = "Issue product provider " + productProvider + " asset: " + productProvider + ". This asset represents the amount of money (South African - Zar) NanoFin has paid the company " + productProvider;
                var issue = await client.IssueOpenWithMetadataFromAsync(nanoFinAddr, productProviderAddress, productProvider, amount, metadata);
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