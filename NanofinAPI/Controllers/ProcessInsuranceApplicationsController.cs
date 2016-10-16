using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NanofinAPI.Models;
using NanofinAPI.Models.DTOEnvironment;
using System.Threading.Tasks;
using System.Data.Entity;
using TheNanoFinAPI.MultiChainLib.Controllers;

namespace NanofinAPI.Controllers
{
    public class ProcessInsuranceApplicationsController : ApiController
    {
        database_nanofinEntities db = new database_nanofinEntities();

        [HttpPost]
        public List<clientswithunprocessedapplication> getUnprocessedApplications()
        {
            return  db.clientswithunprocessedapplications.ToList();
        }



        //[HttpPost]
        //public Boolean ProcessBatchApplication(int [] consumerList)
        //{
          
        //    foreach( var  i  in consumerList)
        //    {
        //       db.processApplications1(i);
        //    }
        //   db.UpdateConsumerRiskProfiles();

        //    return true;
        //}

        [HttpPost]
        public Boolean ProcessAllConsumerApplication(int  consumerID)
        {
            db.processApplications1(consumerID);
            db.UpdateConsumerRiskProfiles();
            return true;
        }

        [HttpPost]
        public List<unprocessedapplication> getConsummerUnProccessedPurchases(int idConsumer)
        {
            var ConsumerID = db.consumerriskvalues.Find(idConsumer).Consumer_ID;

            return (from c in db.unprocessedapplications  select c).ToList();
        }

        [HttpPost]
        public Boolean ProcessSingleApplication(int activeProductID)
        {
            db.processSingleClientApplication(activeProductID);
            return true;
        }

        [HttpPost]
        public  getallconsumerinformation getUserInformation(int idConsumer)
        {
            return db.getallconsumerinformations.SingleOrDefault(c => c.idConsumer == idConsumer);
        }

        [HttpPost]
        public Boolean processApplicationNativeImp(int activeProductID)
        {
            var toDeactivate = db.activeproductitems.Find(activeProductID);
            toDeactivate.activeProductItemPolicyNum = "PP_IM_" + activeProductID.ToString();
            db.SaveChanges();
            return true;
        }


        [HttpPost]
        public async Task RejectedApplication(int ActiveProductID)
        {
            var rejectProd = db.activeproductitems.Find(ActiveProductID);
            var prodValue = rejectProd.productValue;
            var tempCons = db.consumers.Find(rejectProd.Consumer_ID);
            var tempUser = db.users.Find(tempCons.User_ID);

            refundConsumer(tempCons.User_ID, rejectProd.productValue);

            //deactive product from active
            rejectProd.isActive = false;
            rejectProd.Accepted = false;
            rejectProd.activeProductItemPolicyNum = "Rejected";
            //notify user push Notification            
            var NC = new NotificationController();

            var message = "NanoFin: Your purchase for '";
            message += prodIDToProdName(rejectProd.Product_ID);
            message += "; has been rejected. Your Account has been refunded with R";
            message += rejectProd.productValue.ToString() + ".";
            //db.UpdateConsumerRiskValues();
            NC.SendSMS(tempUser.userContactNumber, message);

            //reflect on blockchain
            string productName = prodIDToProdName(rejectProd.Product_ID);
            MConsumerController consumerCtrl = new MConsumerController(tempUser.User_ID);
            consumerCtrl = await consumerCtrl.init();
            await consumerCtrl.refundConsumer(productName, Decimal.ToInt32(prodValue));
        }

        private string prodIDToProdName(int productID)
        {
            product tmp =  db.products.Find(productID);
            return tmp.productName;
        }

        private void refundConsumer(int userID, decimal voucherAmout)
        {
            voucher newVoucher = new voucher();
            newVoucher.User_ID = userID;
            newVoucher.voucherValue = voucherAmout;
            newVoucher.VoucherType_ID = 2;
            newVoucher.voucherCreationDate = DateTime.Now;
            db.vouchers.Add(newVoucher);
            db.SaveChanges();

            addVoucherTransaction(newVoucher.Voucher_ID, newVoucher.Voucher_ID, userID, 1, voucherAmout, 41);
        }

        private void addVoucherTransaction(int newVoucherID, int voucherID, int receiverID, int senderID, decimal Amount, int transactionTypeID)
        {
            vouchertransaction newTransaction = new vouchertransaction();
            newTransaction.VoucherSentTo = newVoucherID;
            newTransaction.Voucher_ID = voucherID;
            newTransaction.Receiver_ID = receiverID;
            newTransaction.Sender_ID = senderID;
            newTransaction.transactionAmount = Amount;
            newTransaction.TransactionType_ID = transactionTypeID;
            DateTime now = DateTime.Now;
            now.ToString("yyyy-MM-dd H:mm:ss");
            newTransaction.transactionDate = now;
            //newTransaction.transactionDate = DateTime.Now;

            transactiontype transactionTypeString = (from list in db.transactiontypes where list.TransactionType_ID == transactionTypeID select list).Single();
            newTransaction.transactionDescription = transactionTypeString.transactionTypeDescription;

            db.vouchertransactions.Add(newTransaction);
            db.SaveChanges();
        }
    }
}
