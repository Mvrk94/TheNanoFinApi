using MultiChainLib;
using MultiChainLib.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TheNanoFinAPI.Models;

namespace TheNanoFinAPI.MultiChainLib.Controllers
{
    public class MUtilityClass
    {
        private static nanofinEntities db = new nanofinEntities();

        //returns users associated address. if user has no address -> give user address -> return new address.
        public static async Task<string> getAddress(MultiChainClient client, int userID)
        {
            if (db.users.Where(list => list.User_ID == userID).Any())
            {
                return db.users.Where(list => list.User_ID == userID).Select(list => list.blockchainAddress).FirstOrDefault();
            }
            else
            {
                //get new address, get user for which address does not exist, add address to user record.
                var newAddress = await client.GetNewAddressAsync();
                newAddress.AssertOk();
                var userToEdit = db.users.Single(o => o.User_ID == userID);
                userToEdit.blockchainAddress = newAddress.Result;
                db.Entry(userToEdit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return newAddress.Result;
            }
        }
        //exchange assset1 (a serialized json) belonging to user 1, for asset2 belonging to user 2
        public static async Task atomicExchange(MultiChainClient multiChainCli, int user1, string JsonStrAsset1, int user2, string JsonStrAsset2)
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


    }
}