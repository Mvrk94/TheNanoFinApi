using NanofinAPI.Models;
using NanofinAPI.Models.DTOEnvironment;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Extreme.Statistics.TimeSeriesAnalysis;

namespace NanofinAPI.Controllers
{
    public class predictions
    {
        public string  values { get; set;}
    }

    public  class ClientMessage
    {
        public string message { get; set; }
        public string IDs { get; set; }
    }


    public class ClientVouchers
    {
        public double  amount { get; set; }
        public string IDs { get; set; }
    }

    public class ConsumerProfilesController : ApiController
    {
        database_nanofinEntities db = new database_nanofinEntities();

        public List<consumerprofiledata> getConsumerProfileData()
        {
            return db.consumerprofiledatas.ToList();
        }

        public List<consumerpreferencesreport> getPreferencesReports()
        {
            return db.consumerpreferencesreports.ToList();
        }

        public List<DTOconsumergroup> getConsumerGroups()
        {
            var toreturn = new List<DTOconsumergroup>();
            var list = db.consumergroups;

            foreach(var temp in  list)
            {
                toreturn.Add(new DTOconsumergroup(temp));
            }

            return toreturn;
        }


        public DTOconsumergroup getSingleConsumerGroup( int consumerGroupID)
        {
            var temp = db.consumergroups.Find(consumerGroupID);
            return new DTOconsumergroup(temp);
        }

        public Boolean AddConsumerGroups(DTOconsumergroup newGroup)
        {
            var newGroupEntity = EntityMapper.updateEntity(null, newGroup);

            db.consumergroups.Add(newGroupEntity);
            db.SaveChanges();
            return true;
        }

        public Boolean updateConsumerGroup(DTOconsumergroup updateGroup)
        {

            var updateded = db.consumergroups.Find(updateGroup.idconsumerGroups);

            EntityMapper.updateEntity(updateded, updateGroup);

            db.Entry(updateded).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }

        public Boolean sendMessageToConsumer(ClientMessage advt)
        {
            var notificationH = new NotificationController();
            var consumerReferences = advt.IDs.Split(',').Select(Int32.Parse).ToList();

            foreach ( var  id  in consumerReferences)
            {
                var cons = db.consumers.Find(id);
                notificationH.SendSMS(cons.user.userContactNumber, advt.message);
            }

            return true;
        }


        public Boolean sendVoucherToClients(ClientVouchers advt)
        {
            var notificationH = new NotificationController();
            var consumerReferences = advt.IDs.Split(',').Select(Int32.Parse).ToList();

            foreach (var id in consumerReferences)
            {
                var cons = db.consumers.Find(id);
               
            }

            return true;
        }


    
        private  async Task sendConsmerVoucher(int User_ID, double amount)
        {
            refundConsumer(User_ID, (Decimal)amount);
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






        public List<double> getPredictions(predictions prevValueStr, int numPredictions, int value1 = 1, int value2 = 1)
        {
            List<double> toreturn = new List<double>();

            var prevValues = prevValueStr.values.Split(',').Select(Int32.Parse).ToList();

            toreturn.AddRange(Array.ConvertAll(prevValues.ToArray(), c => (double)c));
            ArimaModel model = new ArimaModel(toreturn.ToArray(), value1, value2);
            model.Compute();

            toreturn.AddRange(Array.ConvertAll(model.Forecast(numPredictions).ToArray(), x => (double)x));
            return toreturn;
        }



    }
}
