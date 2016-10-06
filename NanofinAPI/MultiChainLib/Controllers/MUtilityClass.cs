using MultiChainLib;
using MultiChainLib.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NanofinAPI.Models;

namespace TheNanoFinAPI.MultiChainLib.Controllers
{
    public class MUtilityClass
    {
        private static nanofinEntities db = new nanofinEntities();

        //returns users associated address. if user has no address -> give user address -> return new address. permission params - assign permissions if new address is created.
       // public static async Task<string> getAddress(MultiChainClient client, int userID, params BlockchainPermissions[] paramPermissions)
       public static async Task<string> getAddress(MultiChainClient client, int userID)
        {
            user tmp = new user();
            tmp.User_ID = -1;
            tmp = await db.users.SingleAsync(l => l.User_ID == userID);
            if (tmp.User_ID != -1)
            {
                if (tmp.blockchainAddress == null)
                {
                    //get new address, get user for which address does not exist, add address to user record.
                    var newAddress = await client.GetNewAddressAsync();
                    newAddress.AssertOk();
                    string newAddr = newAddress.Result;
                    //give permissions
                   // var grant1 = client.GrantAsync(new List<string>() { newAddr }, BlockchainPermissions.Send);
                    //var grant2 = client.GrantAsync(new List<string>() { newAddr }, BlockchainPermissions.Receive);

                    tmp.blockchainAddress = newAddress.Result;
                    db.Entry(tmp).State = EntityState.Modified;
                    db.SaveChanges();

                    //if(paramPermissions.Length > 0)
                    //{
                    //    MUserController tmpUser = new MUserController(userID);
                    //    tmpUser = await tmpUser.init();
                    //    await tmpUser.grantPermissions(paramPermissions);
                    //}

                    return newAddress.Result;
                }else
                {
                    return tmp.blockchainAddress;
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
        //exchange assset1 (a serialized json) belonging to user 1, for asset2 belonging to user 2
        public static async Task<int> atomicExchange(MultiChainClient multiChainCli, int user1, string JsonStrAsset1, int user2, string JsonStrAsset2)
        {
            var client = multiChainCli;
            string user1Addr = await getAddress(client, user1);
            string user2Addr = await getAddress(client, user2);

            //
            var prepLockUnspentAsset2From = await client.PrepareLockUnspentFromsync(user1Addr, JsonStrAsset1);
            prepLockUnspentAsset2From.AssertOk();
            PrepareLockUnspentFromResponse lockedAsset2 = prepLockUnspentAsset2From.Result;

            //create raw exchange taking in locked inputs for asset 2. ask for asset 1
            var newRawExch = await client.CreateRawExchangeAsync(lockedAsset2.txid, lockedAsset2.vout, JsonStrAsset2);
            newRawExch.AssertOk();
            string hexBlob = newRawExch.Result;

            //decode raw exchange transaction - output: offer, ask & cancompete
            //var decodeExch = await client.DecodeRawExchangeAsync(hexBlob);
            //decodeExch.AssertOk();

            //prepare lock unspent inputs of asset 1
            var prepLockUnspentAsset1From = await client.PrepareLockUnspentFromsync(user2Addr, JsonStrAsset2);
            prepLockUnspentAsset1From.AssertOk();
            PrepareLockUnspentFromResponse lockedAsset1 = prepLockUnspentAsset1From.Result;

            //append asset 1 to raw exchange
            var appendRawExch = await client.AppendRawExchangeAsync(hexBlob, lockedAsset1.txid, lockedAsset1.vout, JsonStrAsset1);
            appendRawExch.AssertOk();
            AppendRawExchangeResponse appendRawExchResponse = appendRawExch.Result;


            //broadcast raw exchange
            var sendRawTransaction = await client.SendRawTransactionAsync(appendRawExchResponse.hex);
            sendRawTransaction.AssertOk();

            return 0;

        }// atomic exchange

        public static async Task<string> getBurnAddress(MultiChainClient multiChainCli)
        {
            var client = multiChainCli;
            var info = await client.GetInfoAsync();
            info.AssertOk();
            return info.Result.burnaddress;
        }


        public static string strToHex(string input)
        {
            char[] values = input.ToCharArray();
            string hex = "";
            foreach (char letter in values)
            {
                int value = Convert.ToInt32(letter);
                hex += String.Format("{0:X}", value); ;
            }
            return hex;
        }

        public static string hexToStr(string hexBlob)
        {
            string str = "";
            char[] hex = hexBlob.ToCharArray();
            for(int i = 0; i < hexBlob.Length; i = i + 2)
            {
                string tmpHex = hex[i].ToString() + hex[i + 1].ToString();
                str += System.Convert.ToChar(System.Convert.ToUInt32(tmpHex, 16)).ToString();
            }
            return str;
        }

        //check if user has asset or if user has amount (assetBalanceToCheck) of type asset on the blockchain
        public static async Task<bool> hasAssetBalance(MultiChainClient client, int userID, string asset, int assetBalanceToCheck)
        {
            string userAddress = await getAddress(client, userID);
            var getAddressBalances = await client.GetAddressBalancesAsync(userAddress);
            getAddressBalances.AssertOk();
            foreach (var balance in getAddressBalances.Result)
            {
                if(balance.Name.Equals(asset) && Convert.ToInt32(balance.Qty) >= assetBalanceToCheck)
                {
                    return true;
                }
            }
            return false;
        }

        //remove all spaces from string.
        public static string removeSpaces(string input)
        {
            string noSpaces = input;
            noSpaces = noSpaces.Replace(" ", string.Empty); // kill spaces
            noSpaces = noSpaces.Replace("	", string.Empty); //kill tabs
            return noSpaces;
        }

        public static async Task<string> getProductProviderName(string insuranceProductName)
        {
            insuranceProductName = removeSpaces(insuranceProductName);
            List<product> productList = (from l in db.products select l).ToList();

            foreach (product prod in productList)
            {
                string tmpName = removeSpaces(prod.productName);
                if (insuranceProductName.Equals(tmpName))
                {
                    productprovider prodProvider = await db.productproviders.SingleAsync(l => l.ProductProvider_ID == prod.ProductProvider_ID);
                    return removeSpaces(prodProvider.ppCompanyName);
                }
            }

            return "";
        }

        public static async Task<string> getProductProviderAddress(MultiChainClient client, string productProviderName)
        {
            productProviderName = removeSpaces(productProviderName);
            List<productprovider> providerList = (from l in db.productproviders select l).ToList();

            foreach (productprovider p in providerList)
            {
                string tmpName = removeSpaces(p.ppCompanyName);
                if (productProviderName.Equals(tmpName))
                {
                    return await getAddress(client, p.User_ID);
                }
            }

            return "";
        }


    }
}