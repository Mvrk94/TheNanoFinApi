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
        public MResellerController(int resellerUserID)
        {
            client = new MultiChainClient("188.166.170.248", 6492, false, "multichainrpc", "AYBR44NDe7VdSWXHJCrR2i2xhCcnByorzHy6f6vaczTd", "NanoFinBlockChain");
            user = new MUserController(resellerUserID, client);
        }

        public async Task<MResellerController> init()
        {
            user = await user.init();
            return this;
        }

        public async void buyBulk(int amount)
        {
            //user.grantPermissions(BlockchainPermissions.Connect, BlockchainPermissions.Send, BlockchainPermissions.Receive);
            //String nanoFinAddr = await MUtilityClass.getAddress(1,client);
            //var transaction = await client.IssueMoreFromAsync(nanoFinAddr, user.propertyUserAddress(), "BulkVoucher", amount);
            //transaction.AssertOk();
            await atomicVoucherExchange(client);
        }

        public static async Task atomicVoucherExchange(MultiChainClient multiChainCli)
        {
            var client = multiChainCli;

            var jVoucher = new voucherJSON()
            {
                Voucher = 0
            };

            var voucherJsonStr = JsonConvert.SerializeObject(jVoucher.Values);


            var prepLockUnspentAsset2From = await client.PrepareLockUnspentFromsync("1PmZXxtR8SGBZ7YnrJpWkXYY5GTUzj3vc6pLkf", voucherJsonStr);
            prepLockUnspentAsset2From.AssertOk();
            PrepareLockUnspentFromResponse lockedAsset2 = prepLockUnspentAsset2From.Result;
            //Console.WriteLine("Prepare Lock unspent asset 2");
            //Console.WriteLine(lockedAsset2.txid);
            //Console.WriteLine(lockedAsset2.vout);
            //create raw exchange taking in locked inputs for asset 2. ask for asset 1
            var jBulkVoucher = new bulkVoucherJSON()
            {
                BulkVoucher = 50
            };
            //Console.WriteLine("Create raw exchange");
            var bulkVouckerJsonStr = JsonConvert.SerializeObject(jBulkVoucher.Values);
            var newRawExch = await client.CreateRawExchangeAsync(lockedAsset2.txid, lockedAsset2.vout, bulkVouckerJsonStr);
            newRawExch.AssertOk();
            string hexBlob = newRawExch.Result;
            //Console.WriteLine("HexBlob: " + hexBlob);
            //decode raw exchange transaction - output: offer, ask & cancompete
            //Console.WriteLine("#Decode");
            //var decodeExch = await client.DecodeRawExchangeAsync(hexBlob);
            //decodeExch.AssertOk();
            //Console.WriteLine("decode hex blob: " + decodeExch.Result);

            //prepare lock unspent inputs of asset 1
            var prepLockUnspentAsset1From = await client.PrepareLockUnspentFromsync("17hUWPNtst5zjbnJb2VJHYnUbXQFxrjfTmCH9y", bulkVouckerJsonStr);
            prepLockUnspentAsset1From.AssertOk();
            PrepareLockUnspentFromResponse lockedAsset1 = prepLockUnspentAsset1From.Result;
            //Console.WriteLine("Prepare Lock unspent asset 1");
            //Console.WriteLine(lockedAsset1.txid);
            //Console.WriteLine(lockedAsset1.vout);

            //append asset 1 to raw exchange
            var appendRawExch = await client.AppendRawExchangeAsync(hexBlob, lockedAsset1.txid, lockedAsset1.vout, voucherJsonStr);
            appendRawExch.AssertOk();
            AppendRawExchangeResponse appendRawExchResponse = appendRawExch.Result;
            //Console.WriteLine("Append Raw Exchange Response - hex then complete");
            //Console.WriteLine(appendRawExchResponse.hex);
            //Console.WriteLine(appendRawExchResponse.complete);

            //broadcast raw exchange
            var sendRawTransaction = await client.SendRawTransactionAsync(appendRawExchResponse.hex);
            sendRawTransaction.AssertOk();
            //Console.WriteLine("Send Raw Transaction");
            //Console.WriteLine("txid: " + sendRawTransaction.Result);


        }


    }
}
