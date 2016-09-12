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

namespace NanoFinAPI.Controllers
{
    public class WalletHandlerController : ApiController
    {

        //POST...add a voucher: use cases involved:
        //-reseller buys voucher online= reseller gets a brand new bulk voucher added
        //-= consumer gets a brand new normal voucher added

        //PUT...update a voucher: use cases involved:
        //-reseller sends voucher= update reseller's voucher amount
        private nanofinEntities db = new nanofinEntities();
        //POST...add a voucher: use cases involved:
        //-reseller buys voucher online= reseller gets a brand new bulk voucher added
        //-= consumer gets a brand new normal voucher added

        //PUT...update a voucher: use cases involved:
        //-reseller sends voucher= update reseller's voucher amount
        public IHttpActionResult SendVoucher(int senderID, int receiverID, decimal amountToSend, int transactionType_ID, int voucherTypeID)
        {
            if (getVoucherAccountBalance(senderID) < amountToSend)
            {
                return BadRequest("invalid funds"); // or custom responce for invalid funds
            }

            Decimal toDeduct = amountToSend;
            List<voucher> senderVouchers = (from c in db.vouchers where c.User_ID == senderID && c.voucherValue > 0 orderby c.voucherValue ascending select c).ToList();

            voucher newVoucher = new voucher();
            newVoucher.User_ID = receiverID;
            newVoucher.voucherValue = amountToSend;
            newVoucher.VoucherType_ID = voucherTypeID;
            DateTime now = DateTime.Now;
            now.ToString("yyyy-MM-dd H:mm:ss");
            newVoucher.voucherCreationDate = now;
            db.vouchers.Add(newVoucher);
            db.SaveChanges();


            for (int i = 0; i < senderVouchers.Count && toDeduct > 0; i++)
            {
                voucher temp = senderVouchers.ElementAt(i);

                if (toDeduct >= temp.voucherValue)
                {
                    toDeduct -= temp.voucherValue;
                    addVoucherTransaction(newVoucher.Voucher_ID, temp.Voucher_ID, receiverID, senderID, temp.voucherValue, transactionType_ID);
                    temp.voucherValue = 0;
                }
                else
                {
                    temp.voucherValue -= toDeduct;
                    addVoucherTransaction(newVoucher.Voucher_ID, temp.Voucher_ID, receiverID, senderID, toDeduct, transactionType_ID);
                    toDeduct = 0;
                }

            }

            return Ok();
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


        public async Task<IHttpActionResult> buyBulkVoucher(int userID, decimal BulkVoucherAmount, DateTime date)
        {
            if (isUserReseller(userID))
            {
                //need to check if reseller has the available funds in bank account - else return bad request or err page
                //transfer money from reseller bank account to nanoFin account

                voucher newVoucher = new voucher();
                newVoucher.User_ID = userID;
                newVoucher.voucherValue = BulkVoucherAmount;
                newVoucher.VoucherType_ID = 1;
                newVoucher.voucherCreationDate = date;
                db.vouchers.Add(newVoucher);
                db.SaveChanges();

                addVoucherTransaction(newVoucher.Voucher_ID, newVoucher.Voucher_ID, userID, 1, BulkVoucherAmount, 1);
                //buy bulk transaction on blockchain
                MResellerController resellerCtrl = new MResellerController(userID);
                resellerCtrl = await resellerCtrl.init();
                await resellerCtrl.buyBulk(Decimal.ToInt32(BulkVoucherAmount));

                return Ok();
            }
            else
            {
                return BadRequest("User not a valid reseller");
            }
        }




        //Send bulk voucher recipient details unknown
        public async Task<IHttpActionResult> sendBulkVoucher_RecipientUnknown(int resellerUserID, string recipientDetails, decimal transferAmount)
        {
            if (isUserReseller(resellerUserID))
            {
                int recipientUserID = getUserDetailsToUserID(recipientDetails);
                if (recipientUserID > 0)
                {
                    if (getVoucherAccountBalance(resellerUserID) < transferAmount)
                    {
                        return BadRequest("invalid funds"); // or custom responce for invalid funds
                    }
                    else
                    {
                         SendVoucher(resellerUserID, recipientUserID, transferAmount, 2, 2);

                        MResellerController resellerCtrl = new MResellerController(resellerUserID);
                        resellerCtrl = await resellerCtrl.init();
                        await resellerCtrl.sendBulk(recipientUserID, Decimal.ToInt32(transferAmount));
                    }
                }
                else
                {
                    return BadRequest("Recipient details not valid");
                }
                return Ok();

            }
            else
            {
                return BadRequest("User not a valid reseller");
            }
        }

        //Send bulk voucher recipient details known
        public async Task<IHttpActionResult> sendBulkVoucher(int resellerUserID, int recipientID, decimal transferAmount)
        {
            if (isUserReseller(resellerUserID))
            {
                if (recipientID > 0)
                {
                    if (getVoucherAccountBalance(resellerUserID) < transferAmount)
                    {
                        return BadRequest("invalid funds"); // or custom responce for invalid funds
                    }
                    else
                    {
                        SendVoucher(resellerUserID, recipientID, transferAmount, 2, 2);

                        MResellerController resellerCtrl = new MResellerController(resellerUserID);
                        resellerCtrl = await resellerCtrl.init();
                        await resellerCtrl.sendBulk(recipientID, Decimal.ToInt32(transferAmount));

                    }
                }
                else
                {
                    return BadRequest("Recipient details not valid");
                }
                return Ok();

            }
            else
            {
                return BadRequest("User not a valid reseller");
            }
        }

        public async Task<IHttpActionResult> consumerSendVoucher(int consumerUserID, int recipientID, decimal transferAmount)
        {
            if (isUserConsumer(consumerUserID) && isUserConsumer(recipientID))
            {
                if (recipientID > 0)
                {
                    if (getVoucherAccountBalance(consumerUserID) < transferAmount)
                    {
                        return BadRequest("invalid funds");
                    }
                    else
                    {
                         SendVoucher(consumerUserID, recipientID, transferAmount, 21, 2);

                        MConsumerController consumerCtrl = new MConsumerController(consumerUserID);
                        consumerCtrl = await consumerCtrl.init();
                        await consumerCtrl.sendVoucherToConsumer(recipientID, Decimal.ToInt32(transferAmount));

                    }


                }
                else
                {
                    return BadRequest("Recipient details invalid");
                }
                
                return Ok();
            }
            else
            {
                return BadRequest("Sender and Receiver must both be of type Consumer");
            }
        }

        public async Task<IHttpActionResult> consumerSendVoucher_RecipientUnknown(int consumerUserID, string recipientDetails, decimal transferAmount)
        {
            if (isUserConsumer(consumerUserID))
            {
                int recipientUserID = getUserDetailsToUserID(recipientDetails);
                if (isUserConsumer(recipientUserID))
                {
                     SendVoucher(consumerUserID, recipientUserID, transferAmount, 21, 2);


                    MConsumerController consumerCtrl = new MConsumerController(consumerUserID);
                    consumerCtrl = await consumerCtrl.init();
                    await consumerCtrl.sendVoucherToConsumer(recipientUserID, Decimal.ToInt32(transferAmount));

                    return Ok();
                }
                else
                {
                    return BadRequest("Receiver is not a consumer");
                }

            }
            else
            {
                return BadRequest("Sender is not a consumer");
            }
        }




        //GET: reseller active vouchers -  JSON object with a list of DTOVoucher objects value > 0
        [ResponseType(typeof(DTOvoucher))]
        public IHttpActionResult getResellerActiveBulkVouchers(int resellerUserID)
        {
            if (isUserReseller(resellerUserID))
            {
                List<voucher> rawVoucherList = (from c in db.vouchers where c.User_ID == resellerUserID && c.voucherValue > 0 && c.VoucherType_ID == 1 select c).ToList();

                if (rawVoucherList == null)
                {
                    return NotFound();
                }
                else
                {
                    List<DTOvoucher> voucherList = new List<DTOvoucher>();
                    foreach (voucher v in rawVoucherList)
                    {
                        voucherList.Add(new DTOvoucher(v));
                    }
                    return Ok(voucherList);
                }

            }
            else
            {
                return BadRequest("User is not a valid reseller");
            }

        }

        //GET: all vouchers a reseller has purchased
        [ResponseType(typeof(DTOvoucher))]
        public IHttpActionResult getResellerPurchasedBulkVouchers(int resellerUserID)
        {
            if (isUserReseller(resellerUserID))
            {
                List<vouchertransaction> rawVoucherTransactionList = (from c in db.vouchertransactions where c.Receiver_ID == resellerUserID && c.Sender_ID == 1 && c.TransactionType_ID == 1 select c).ToList();

                if (rawVoucherTransactionList == null)
                {
                    return NotFound();
                }
                else
                {
                    List<DTOvouchertransaction> voucherTransactionList = new List<DTOvouchertransaction>();
                    foreach (vouchertransaction vt in rawVoucherTransactionList)
                    {
                        voucherTransactionList.Add(new DTOvouchertransaction(vt));
                    }
                    return Ok(voucherTransactionList);
                }

            }
            else
            {
                return BadRequest("User is not a valid reseller");
            }

        }

        //GET: all vouchers a reseller has sold
        [ResponseType(typeof(DTOvouchertransaction))]
        public IHttpActionResult getResellerSentBulkVouchers(int resellerUserID)
        {
            if (isUserReseller(resellerUserID))
            {
                List<vouchertransaction> rawVoucherTransactionList = (from c in db.vouchertransactions where c.Sender_ID == resellerUserID && c.TransactionType_ID == 2 orderby c.VoucherSentTo select c).ToList();


                if (rawVoucherTransactionList == null)
                {
                    return NotFound();
                }
                else
                {
                    IEnumerable<ResellerSendHistory> toreturn = new List<ResellerSendHistory>();
                    toreturn = rawVoucherTransactionList.GroupBy(d => d.VoucherSentTo).Select(
                        g => new ResellerSendHistory
                        {
                            amountSent = g.Sum(d => d.transactionAmount),
                            dateSend = g.ElementAt(0).transactionDate,
                            phoneNo = g.ElementAt(0).user.userContactNumber,
                        });
                    
                    return Ok(toreturn);
                }

            }
            else
            {
                return BadRequest("User is not a valid reseller");
            }

        }
        //takes resellerUserID returns list of DTOresellerSentVoucher_withUserDetails
        [ResponseType(typeof(List<DTOresellerSentVoucher_withUserDetails>))]
        public IHttpActionResult getSentBulkVoucherUserDetails(int resellerUserID)
        {
            if (isUserReseller(resellerUserID))
            {
                List<vouchertransaction> rawVoucherTransactionList = (from c in db.vouchertransactions where c.Sender_ID == resellerUserID && c.TransactionType_ID == 2 orderby c.VoucherSentTo select c).ToList();


                if (rawVoucherTransactionList == null)
                {
                    return NotFound();
                }
                else
                {
                    List<DTOvouchertransaction> voucherTransactionList = new List<DTOvouchertransaction>();
                    int voucherSentToID = rawVoucherTransactionList.First().VoucherSentTo;
                    decimal tmpTransactionValue = 0;
                    int index = 0;
                    foreach (vouchertransaction vt in rawVoucherTransactionList)
                    {
                        if (index + 1 == rawVoucherTransactionList.Count)
                        {
                            tmpTransactionValue += Math.Abs(vt.transactionAmount);
                            DTOvouchertransaction tmpVoucherTransaction = new DTOvouchertransaction();
                            tmpVoucherTransaction.Sender_ID = rawVoucherTransactionList.ElementAt(index - 1).Sender_ID;
                            tmpVoucherTransaction.Receiver_ID = rawVoucherTransactionList.ElementAt(index - 1).Receiver_ID;
                            tmpVoucherTransaction.TransactionType_ID = 2;
                            tmpVoucherTransaction.transactionAmount = tmpTransactionValue;
                            tmpVoucherTransaction.transactionDate = rawVoucherTransactionList.ElementAt(index - 1).transactionDate;
                            tmpVoucherTransaction.VoucherSentTo = rawVoucherTransactionList.ElementAt(index - 1).VoucherSentTo;
                            voucherTransactionList.Add(tmpVoucherTransaction);
                        }
                        else if (voucherSentToID == vt.VoucherSentTo)
                        {
                            tmpTransactionValue += Math.Abs(vt.transactionAmount);
                        }
                        else
                        {
                            DTOvouchertransaction tmpVoucherTransaction = new DTOvouchertransaction();
                            tmpVoucherTransaction.Sender_ID = rawVoucherTransactionList.ElementAt(index - 1).Sender_ID;
                            tmpVoucherTransaction.Receiver_ID = rawVoucherTransactionList.ElementAt(index - 1).Receiver_ID;
                            tmpVoucherTransaction.TransactionType_ID = 2;
                            tmpVoucherTransaction.transactionAmount = tmpTransactionValue;
                            tmpVoucherTransaction.transactionDate = rawVoucherTransactionList.ElementAt(index - 1).transactionDate;
                            tmpVoucherTransaction.VoucherSentTo = rawVoucherTransactionList.ElementAt(index - 1).VoucherSentTo;
                            voucherTransactionList.Add(tmpVoucherTransaction);
                            voucherSentToID = vt.VoucherSentTo;
                            tmpTransactionValue = Math.Abs(vt.transactionAmount);
                        }
                        index++;
                    }

                    List<DTOresellerSentVoucher_withUserDetails> list = new List<DTOresellerSentVoucher_withUserDetails>();
                    foreach (DTOvouchertransaction v in voucherTransactionList)
                    {
                        DTOuser tmp = new DTOuser(db.users.Single(x => v.Receiver_ID == x.User_ID));
                        list.Add(new DTOresellerSentVoucher_withUserDetails(v, tmp));
                    }
                    return Ok(list);
                }
            }
            else
            {
                return BadRequest("User is not a valid reseller");
            }



        }

        [ResponseType(typeof(decimal))]
        public IHttpActionResult getResellerTotalCashEarned(int resellerUserID)
        {
            decimal totalAmountSent = 0;
            decimal amountEarned = 0;
            if (isUserReseller(resellerUserID))
            {
                List<vouchertransaction> rawVoucherTransactionList = (from c in db.vouchertransactions where c.Sender_ID == resellerUserID && c.TransactionType_ID == 2 orderby c.VoucherSentTo select c).ToList();
                if (rawVoucherTransactionList == null)
                {
                    return NotFound();
                }
                else //has sent vouchers. add total then get 10% -> for now.
                {

                    foreach (vouchertransaction vt in rawVoucherTransactionList)
                    {
                        totalAmountSent += Math.Abs(vt.transactionAmount);
                    }
                    amountEarned = totalAmountSent * Convert.ToDecimal(0.1);
                }
            }
            else
            {
                return BadRequest("User is not a valid reseller");
            }
            return Ok(amountEarned);
        }

        public Decimal getVoucherAccountBalance(int userID)
        {
            Decimal result = (from c in db.vouchers where c.User_ID == userID && c.voucherValue > 0 select c.voucherValue).Sum();
            return result;
        }
        //input (user: email/cellphone number/ user name) return user ID; return -1 if user details passed are not in User table
        public int getUserDetailsToUserID(String input)
        {
            if (db.users.Any(list => list.userName == input || list.userEmail == input || list.userContactNumber == input))
            {
                return db.users.Where(list => list.userName == input || list.userEmail == input || list.userContactNumber == input).Select(list => list.User_ID).FirstOrDefault();
            }
            else
                return -1;
        }
        //Check if user is a reseller
        public bool isUserReseller(int userID)
        {
            return db.resellers.Where(list => list.User_ID == userID).Any();
        }
        //GET: check if user has (amount) worth of vouchers
        public bool getHasSufficientAmount(int userID, decimal amount)
        {
            decimal balance = getVoucherAccountBalance(userID);
            return balance >= amount;
        }

        //GET: user first name
        public string getUserFirstName(int userID)
        {
            if (db.users.Any(list => list.User_ID == userID))
            {
                return db.users.Where(list => list.User_ID == userID).Select(list => list.userFirstName).FirstOrDefault();
            }
            else
                return "";
        }

        public bool isUserConsumer(int UserID)
        {
            return db.consumers.Where(c => c.User_ID == UserID).Any();
        }

    }
}