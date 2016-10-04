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

namespace NanofinAPI.Controllers
{
    public class ProcessInsuranceApplicationsController : ApiController
    {
        database_nanofinEntities db = new database_nanofinEntities();

        [HttpGet]
        public List<DTOconsumerriskvalue> getUnprocessedApplications()
        {
            var toreturn = new List<DTOconsumerriskvalue>();
            var list = (from c in db.consumerriskvalues where c.numUnprocessed > 0 select c);

            foreach(var p  in list)
            {
                toreturn.Add(new DTOconsumerriskvalue(p));
            }

            return toreturn;
        }

        [HttpPost]
        public Boolean ProcessBatchApplication(int [] consumerList)
        {
          
            foreach( var  i  in consumerList)
            {
                db.processApplications1(i);
            }
            db.UpdateConsumerRiskValues();

            return true;
        }

        [HttpPost]
        public Boolean ProcessApplication(int  consumerID)
        {
            db.processApplications1(consumerID);
            db.UpdateConsumerRiskValues();
            return true;
        }

        [HttpGet]
        public List<unprocessedapplication> getConsummerUnProccessedPurchases(int ConsumerID)
        {
            return (from c in db.unprocessedapplications where c.Consumer_ID == ConsumerID select c).ToList();
        }

        [HttpPost]
        public Boolean ProcessSingleApplication(int activeProductID)
        {
            db.processSingleApplication(activeProductID);
            return true;
        }

        [HttpGet]
        public  List<consumerinfosummary> getUserInformation(int consumerID)
        {
            return db.consumerinfosummaries.Where(c => c.Consumer_ID == consumerID).ToList();
        }

        [HttpGet]
        public void RejectedApplication(int ActiveProductID)
        {
            var rejectProd = db.activeproductitems.Find(ActiveProductID);
            var prodValue = rejectProd.productValue;
            var tempCons = db.consumers.Find(rejectProd.Consumer_ID);
            var tempUser = db.users.Find(tempCons.User_ID);

            refundConsumer(tempCons.User_ID, rejectProd.productValue);

            //deactive product from active
            rejectProd.isActive = false;
            rejectProd.Accepted = false;
            //notify user push Notification            
            var NC = new NotificationController();

            var message = "NanoFin: Your purchase for product ";
            message += prodIDToProdName(rejectProd.Product_ID);
            message += " has been rejected. Your Account has been refunded with R";
            message += rejectProd.productValue.ToString() + ".";
            NC.SendSMS(tempUser.userContactNumber, message);
        }

        public async Task<string> prodIDToProdName(int productID)
        {
            product tmp = await db.products.SingleAsync(l => l.Product_ID == productID);
            return tmp.productName;
        }

        public void refundConsumer(int userID, decimal BulkVoucherAmount)
        {
            voucher newVoucher = new voucher();
            newVoucher.User_ID = userID;
            newVoucher.voucherValue = BulkVoucherAmount;
            newVoucher.VoucherType_ID = 2;
            newVoucher.voucherCreationDate = DateTime.Now;
            db.vouchers.Add(newVoucher);
            db.SaveChanges();

            addVoucherTransaction(newVoucher.Voucher_ID, newVoucher.Voucher_ID, userID, 1, BulkVoucherAmount, 41);
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
