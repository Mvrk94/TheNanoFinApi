using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using NanofinAPI.Models;
using NanofinAPI.Models.DTOEnvironment;
using TheNanoFinAPI.MultiChainLib.Controllers;

namespace NanoFinAPI.Controllers
{
    public class ConsumerWalletHandlerController : ApiController
    {
        private database_nanofinEntities db = new database_nanofinEntities();

        #region RedeemProduct Code
        public async Task<IHttpActionResult> redeemProduct(int userID, int productID, int numberUnits, DateTime startdate)
        {
            //check that the minimum number of units is applied according to what is in db:
            if (!isValidNumUnits(productID,numberUnits))
            {
                return BadRequest("The minimum number of units constraint has not been met");
            }


            //calculate price of products for the specified number of days/units
            //"ProductValue" field in activeProduct Items
            decimal prodTotalPrice = calcProductPrice(productID, numberUnits);

            //calculate totalVoucherValues for this user
            decimal totalVoucherValues = calcTotalVoucherValues(userID);

            //get the user's voucherList in ascending order
            List<voucher> vouchersList = new List<voucher>();
            vouchersList = getUserVouchersList(userID);

            //make a decimal to deduct
            decimal amountToDeduct = prodTotalPrice;

            //check that the user has sufficient vouchers: that prodTotalPrice <= totalVoucherValues
            if (prodTotalPrice <= totalVoucherValues)//can proceed with redeem process
            {
                //Add activeProductItem to db table
                var consumerID = (from c in db.consumers where c.User_ID == userID select c.Consumer_ID).FirstOrDefault();
                activeproductitem activeProdItem = createActiveProductItem(consumerID, productID, "", true, prodTotalPrice, numberUnits, startdate);
                db.activeproductitems.Add(activeProdItem);
                db.SaveChanges();

                //Update the 1 voucher table, 2 voucherTransaction table-not anymore and the 3 productRedemptionLog table
                for (int i = 0; (i < vouchersList.Count) && (amountToDeduct > 0); i++)
                {
                    decimal voucherVal = vouchersList.ElementAt(i).voucherValue;

                    if (amountToDeduct > vouchersList.ElementAt(i).voucherValue)
                    {
                        //will have to finish up this small voucher, it's value becomes 0
                        //this voucher is recorded individually in the productRedemptionLog

                        vouchersList.ElementAt(i).voucherValue = 0;
                        db.Entry(vouchersList.ElementAt(i)).State = EntityState.Modified;
                         db.SaveChanges();

                        //addVoucherTransactionLog()
                        //addVoucherTransactionLog(vouchersList.ElementAt(i).Voucher_ID, vouchersList.ElementAt(i).Voucher_ID, userID, 0, 31, voucherVal, "Voucher Redemption for Product Purchase", System.DateTime.Now);

                        //addProductRedemptionLog()
                        addProductRedemptionLog(activeProdItem.ActiveProductItems_ID, vouchersList.ElementAt(i).Voucher_ID, voucherVal);

                        //update amount to deduct: becomes less by voucher val
                        amountToDeduct -= voucherVal;
                    }

                    if (amountToDeduct <= vouchersList.ElementAt(i).voucherValue)
                    {
                        //use a part of this voucher
                        vouchersList.ElementAt(i).voucherValue = voucherVal - amountToDeduct;
                        db.Entry(vouchersList.ElementAt(i)).State = EntityState.Modified;
                         db.SaveChanges();

                        //add voucherTransactionLog()
                        //addVoucherTransactionLog(vouchersList.ElementAt(i).Voucher_ID, vouchersList.ElementAt(i).Voucher_ID, userID, 0, 31, amountToDeduct, "Voucher Redemption for Product Purchase", DateTime.Now);

                        //add productRedemptionLog
                        addProductRedemptionLog(activeProdItem.ActiveProductItems_ID, vouchersList.ElementAt(i).Voucher_ID, amountToDeduct);

                        //update amount to deduct
                        amountToDeduct = 0;
                    }

                }//for loop

                string productName = await prodIDToProdName(productID);
                MConsumerController consumerCtrl = new MConsumerController(userID);
                consumerCtrl = await consumerCtrl.init();
                await consumerCtrl.redeemVoucher(productName, Decimal.ToInt32(prodTotalPrice));

                return StatusCode(HttpStatusCode.OK);
            }
            return BadRequest("Insufficient voucher total");
        }//RedeemProduct method

        public async Task<string> prodIDToProdName(int productID)
        {
            product tmp = await db.products.SingleAsync(l => l.Product_ID == productID);
            return tmp.productName;
        }

        [HttpGet]
        public bool isValidNumUnits(int productId, int numUnits)
        {
            var dbMinUnits = db.insuranceproducts.Where(c => c.Product_ID == productId).Select(c => c.ipMinimunNoOfUnits);
            return (numUnits >= int.Parse(dbMinUnits.FirstOrDefault().ToString()));
        }

        //send in product id, numUnits return: totalPrice for the numUnits
        private decimal calcProductPrice(int prodID, int numUnits)
        {
            var prodUnitCost = db.insuranceproducts.Where(b => b.Product_ID == prodID).Select(p => p.ipUnitCost);
            return Decimal.Parse((numUnits * prodUnitCost.FirstOrDefault()).ToString());
        }

        //get the vouchers that this user currently has where the value is>0 in ascending order
        private List<voucher> getUserVouchersList(int userId)
        {
            List<voucher> list = new List<voucher>();
            list = (from v in db.vouchers where v.User_ID == userId && v.voucherValue > 0 orderby v.voucherValue ascending select v).ToList();

            return list;
        }

        [HttpGet]
        public decimal calcTotalVoucherValues(int userID)
        {
            var vouchTotValues = (decimal)0;
            List<voucher> usersVouchers = getUserVouchersList(userID);
            foreach (voucher v in usersVouchers)
            {
                vouchTotValues = vouchTotValues + v.voucherValue;
            }
            return vouchTotValues;
        }

        private activeproductitem createActiveProductItem(int ConsumerID, int ProductID, string policyNum, bool isActive, decimal productValue, int duration, DateTime startDate)
        {
            activeproductitem activeProdItem = new activeproductitem();
            activeProdItem.Consumer_ID = ConsumerID;
            activeProdItem.Product_ID = ProductID;
            activeProdItem.activeProductItemPolicyNum = policyNum;
            activeProdItem.isActive = isActive;
            activeProdItem.productValue = productValue;
            activeProdItem.duration = duration;
            activeProdItem.activeProductItemStartDate = startDate;

            return activeProdItem;
        }

        private void addProductRedemptionLog(int activeProductItemID, int voucherID, decimal transactionAmount)
        {
            productredemptionlog prodRedLog = new productredemptionlog();
            prodRedLog.ActiveProductItems_ID = activeProductItemID;
            prodRedLog.Voucher_ID = voucherID;
            prodRedLog.transactionAmount = transactionAmount;

            db.productredemptionlogs.Add(prodRedLog);
            db.SaveChanges();
        }

        private void addVoucherTransactionLog(int voucherID, int voucherSentTo, int SenderID, int ReceiverID, int transType, decimal amount, string description, DateTime date)
        {
            vouchertransaction vouchTrans = new vouchertransaction();
            vouchTrans.Voucher_ID = voucherID;
            vouchTrans.VoucherSentTo = voucherSentTo;
            vouchTrans.Sender_ID = SenderID;
            vouchTrans.Receiver_ID = ReceiverID;
            vouchTrans.TransactionType_ID = transType;
            vouchTrans.transactionAmount = amount;
            vouchTrans.transactionDescription = description;
            vouchTrans.transactionDate = date;

            db.vouchertransactions.Add(vouchTrans);
            db.SaveChanges();

        }
        
        
        #endregion


        #region get Consumer Catalog Code

        // GET: api/getAllCatalogInsuranceProducts
        public List<DTOcatalogInsuranceProduct> getAllCatalogInsuranceProducts()
        {
            List<DTOcatalogInsuranceProduct> toReturn = new List<DTOcatalogInsuranceProduct>();

            List<insuranceproduct> list = (from c in db.insuranceproducts select c).ToList();

            foreach (insuranceproduct p in list)
            {
                toReturn.Add(new DTOcatalogInsuranceProduct(p));
            }

            return toReturn;
        }

        // GET: api/getSpecificCategoryCatalogInsuranceProduct/insuranceTypeID
        public List<DTOcatalogInsuranceProduct> getSpecificCategoryCatalogInsuranceProducts(int insuranceTypeID)
        {
            List<DTOcatalogInsuranceProduct> toReturn = new List<DTOcatalogInsuranceProduct>();

            List<insuranceproduct> list = (from c in db.insuranceproducts where c.InsuranceType_ID == insuranceTypeID select c).ToList();

            foreach (insuranceproduct p in list)
            {
                toReturn.Add(new DTOcatalogInsuranceProduct(p));
            }

            return toReturn;
        }

        //GET: api/getSpecificSingleProductDetails/ProductID
        public DTOcatalogInsuranceProduct getSpecificSingleProductDetails(int ProductID)
        {
            DTOcatalogInsuranceProduct toReturn = new DTOcatalogInsuranceProduct(db.insuranceproducts.Single(c => c.Product_ID == ProductID));
            if (toReturn == null)
            {
                return null;
            }

            return toReturn;
        }


        #endregion

        //Consumer send voucher code in wallet handler

        #region Consumer View Wallet code

        //GET nanobucks
        public Decimal getConsumerVoucherTotal(int userID)
        {
            Decimal vouchTotal = (from c in db.vouchers where c.User_ID == userID && c.voucherValue > 0 select c.voucherValue).Sum();
            return vouchTotal;
        }

        //GET consumer's active product items: 
        public List<DTOactiveproductitem> GetConsumerActiveProductitems(int userID)
        {
            List<DTOactiveproductitem> toReturn = new List<DTOactiveproductitem>();
            List<activeproductitem> list = (from c in db.activeproductitems where c.consumer.User_ID==userID && c.isActive==true select c).ToList();

            foreach (activeproductitem p in list)
            {
                toReturn.Add(new DTOactiveproductitem(p));
            }

            return toReturn;
        }

        //GET consumer's active product items with product details: 
        public List<DTOactiveProductItemWithDetail> GetConsumerActiveProductitemsWithDetail(int userID)
        {
            List<DTOactiveProductItemWithDetail> toReturn = new List<DTOactiveProductItemWithDetail>();

            List<activeproductitem> list = (from c in db.activeproductitems where c.consumer.User_ID == userID && c.isActive==true select c).ToList();


            foreach (activeproductitem p in list)
            {
                //get an instance of an insurance product for each and every active product item of the consumer
                insuranceproduct insuranceProd = (from i in db.insuranceproducts where i.Product_ID == p.Product_ID select i).SingleOrDefault();

                toReturn.Add(new DTOactiveProductItemWithDetail(p,insuranceProd));
            }

            return toReturn;
        }



        //GET specific Product/productID
        [ResponseType(typeof(DTOproduct))]
        public IHttpActionResult Getproduct(int id)
        {
            DTOproduct toReturn = new DTOproduct( db.products.Find(id));
            if (toReturn == null)
            {
                return NotFound();
            }
            return CreatedAtRoute("DefaultApi", new { id = toReturn.Product_ID }, toReturn); ;
        }

        //GET specific Insuranceproduct/productID
        [ResponseType(typeof(DTOinsuranceproduct))]
        public DTOinsuranceproduct GetInsuranceProduct(int productId)
        {
            insuranceproduct p = db.insuranceproducts.Where(c => c.Product_ID == productId).SingleOrDefault();

            DTOinsuranceproduct toReturn = new DTOinsuranceproduct(p);
            if (toReturn == null)
            {
                return null;
            }
            return toReturn; 
        }



        #endregion

        #region Consumer view Histories
        //GET transaction history:  1. voucher received from reseller   2.vouchers received from friend   3. vouchers sent to friend   4. voucher redemption for products


        public List<DTOvoucher> getVouchersBelongingToUser(int userID)
        {
            List<DTOvoucher> toReturn = new List<DTOvoucher>();
            List<voucher> list = (from c in db.vouchers where c.User_ID == userID select c).ToList();

            foreach (voucher v in list)
            {
                toReturn.Add(new DTOvoucher(v));
            }

            return toReturn;

        }


        //Get all the voucher transaction history for this user
        public List<DTOvouchertransaction> getAllHistories(int userID)
        {

            // List<DTOvouchertransaction> toReturn = new List<DTOvouchertransaction>();
            List<DTOvouchertransaction> toReturn = db.vouchertransactions.Where(v => (v.Sender_ID == userID || v.Receiver_ID == userID) && (v.TransactionType_ID == 2 || v.TransactionType_ID == 21 || v.TransactionType_ID == 31))
                .GroupBy(s => s.VoucherSentTo).Select(x => new DTOvouchertransaction
                {
                    Voucher_ID = x.FirstOrDefault().Voucher_ID,
                     VoucherSentTo = x.FirstOrDefault().VoucherSentTo,
                     Sender_ID = x.FirstOrDefault().Sender_ID,
                    Receiver_ID = x.FirstOrDefault().Receiver_ID,
                    TransactionType_ID = x.FirstOrDefault().TransactionType_ID,
                    transactionDescription = x.FirstOrDefault().transactionDescription,
                    transactionAmount = x.Sum(s => s.transactionAmount),
                    transactionDate = x.FirstOrDefault().transactionDate

                }).OrderBy(d=>d.transactionDate)
               .ToList();

            //Reset the descriptions
            foreach (DTOvouchertransaction v in toReturn)
            {
                v.transactionDescription = setTransactionHistoryDescription(userID,v.Sender_ID,v.Receiver_ID,v.TransactionType_ID);
            }
                      
            return toReturn;
        }

        //method used in "getVouchersBelongingToUser()" to write a clearer description for the user. //transaction types: 1=ResellerBuyBulk 2=ResellerSend 21=ConsumerSend 31=ConsumerRedeem
        private string setTransactionHistoryDescription(int userID, int SenderID, int ReceiverID, int transactionTypeID)
        {
            string description = "";

            //If the user is the receiver:  1.from reseller  2.from friend
            if (userID == ReceiverID)
            {
                if (transactionTypeID == 2)//ResellerSend
                {
                    description = "Voucher purchased from Reseller";
                }

                if (transactionTypeID == 21)//ConsumerSend
                {
                    description = "Voucher received from NanoFin friend";
                }
            }

            //If the user is the sender: 1.to friend   2.Redeem
            if (userID == SenderID)
            {
                if (transactionTypeID == 21) //ConsumerSend
                {
                    description = "Voucher sent to NanoFin friend";
                }

                if (transactionTypeID == 31)
                {
                    description = "Redeem voucher for product";
                }
            }

            return description;
        }


        #endregion


        #region Consumer View, Update Profile
        //GET profile information
        public DTOconsumerUserProfileInfo getConsumerProfileInfo(int userID)
        {
            DTOconsumerUserProfileInfo toReturn = new DTOconsumerUserProfileInfo(db.consumers.Single(c=>c.User_ID == userID));

            if (toReturn == null)
            {
                return null;
            }

            return toReturn;
        }



        //Update profile information /userID/updatedProfile- will update both consumer and user tables based on the DTOconsumerUserProfile being passed
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult updateConsumerProfile(int UserId, DTOconsumerUserProfileInfo dtoConsumerProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (UserId != dtoConsumerProfile.userID)
            {
                return BadRequest();
            }

            var putConsumer = db.consumers.Single(e => e.User_ID == UserId);
            //warning the methode being called was auto generated into the Entity Mapper class 
            //please define your own methode some where besides this location
           // db.Entry(EntityMapper.updateEntity(putConsumer, dtoConsumerProfile)).State = EntityState.Modified;

            var putUser = db.users.Single(d => d.User_ID == UserId);

            //db.Entry(EntityMapper.updateEntity(putUser, dtoConsumerProfile)).State = EntityState.Modified;

            try
            {
                 db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();

            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        #endregion



    }

}
