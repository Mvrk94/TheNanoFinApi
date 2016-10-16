using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Http.Description;
using NanofinAPI.Models;
using NanofinAPI.Models.DTOEnvironment;
using MultiChainLib.Controllers;
using TheNanoFinAPI.MultiChainLib.Controllers;
using System.Data.Entity;

namespace NanofinAPI.Controllers
{
    public class BCTestController : ApiController
    {
        database_nanofinEntities db = new database_nanofinEntities();
        //redeem  -> accept / reject(refund)



       


         [HttpPost]
        [ResponseType(typeof(bool))]
        public async Task<bool> resellerBuyBulk(int userID, int amount)
        {
            MResellerController resellerCtrl = new MResellerController(userID);
            resellerCtrl = await resellerCtrl.init();
            await resellerCtrl.buyBulk(Decimal.ToInt32(amount));
            return true;
        }


        [HttpPost]
        [ResponseType(typeof(bool))]
        public async Task<bool> consumerRedeem(int productID, int userID, int amount)
        {
            string productName = await prodIDToProdName(productID);

            MConsumerController consumerCtrl = new MConsumerController(userID);
            consumerCtrl = await consumerCtrl.init();
            await consumerCtrl.redeemVoucher(productName, Decimal.ToInt32(amount));
            return true;
        }


        public async Task<string> prodIDToProdName(int productID)
        {
            product tmp = await db.products.SingleAsync(l => l.Product_ID == productID);
            return tmp.productName;
        }


        [HttpPost]
        [ResponseType(typeof(bool))]
        public async Task<bool> consumerRefund(int productID, int userID, int amount)
        {
            string productName = await prodIDToProdName(productID);

            MConsumerController consumerCtrl = new MConsumerController(userID);
            consumerCtrl = await consumerCtrl.init();
            await consumerCtrl.refundConsumer(productName, Decimal.ToInt32(amount));


            return true;
        }


        [HttpPost]
        [ResponseType(typeof(bool))]
        public async Task<bool> acceptRedeem(int productID, int userID, int amount)
        {
            string productName = MUtilityClass.removeSpaces(await prodIDToProdName(productID));

            MConsumerController consumerCtrl = new MConsumerController(userID);
            consumerCtrl = await consumerCtrl.init();
            await consumerCtrl.acceptRedeem(productName, amount);
            return true;
        }


        [HttpPost]
        [ResponseType(typeof(bool))]
        public async Task<bool> invalidateProduct(int productID, int userID, int amount)
        {
            string productName = MUtilityClass.removeSpaces(await prodIDToProdName(productID));

            MConsumerController consumerCtrl = new MConsumerController(userID);
            consumerCtrl = await consumerCtrl.init();
            await consumerCtrl.invalidateProduct(productName, amount);
            return true;
        }

        


        



    }
}
