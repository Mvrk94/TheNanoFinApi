using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Data.Entity;
using NanofinAPI.Models;

namespace NanofinAPI.Controllers
{

    public class RandomDataController : ApiController
    {
        public DateTime dd = new DateTime(1, 1, 1);
        nanofinEntities db = new nanofinEntities();
        Random r = new Random();

        [HttpGet]
        [ResponseType(typeof(Boolean))]
        public async Task<Boolean> CleanReselleDates()
        {
            Boolean toreturn = false;

            List<voucher> voucherlist = (from c in db.vouchers select c).ToList();

            foreach (voucher temp in voucherlist)
            {
                temp.voucherCreationDate = temp.voucherCreationDate.Value.AddMonths(-7);
                db.Entry(temp).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

            return toreturn;
        }

        [HttpGet]
        [ResponseType(typeof(Boolean))]
        public async Task<Boolean> ModifyHistory(int voucherID, int consumerVoucherID)
        {

            List<voucher> voucherlist = (from c in db.vouchers where c.VoucherType_ID == 1 && c.Voucher_ID > voucherID select c).ToList();
            //consumer [] consumerList = (from c in db.consumers select c).ToArray();
            voucher[] consumerVouchers = (from c in db.vouchers where c.VoucherType_ID == 2 && c.Voucher_ID > consumerVoucherID select c).ToArray();
            int counter = 1;


            foreach (voucher temp in voucherlist)
            {
                int value = (int)temp.voucherValue;
                decimal amount = (Decimal)(value * 0.2);
                voucher cons = consumerVouchers[counter];

                for (int i = 0; i < 5; i++)
                {
                    //counter++;
                    consumerVouchers[counter].voucherValue = amount;
                    addVoucherTransaction(consumerVouchers[counter].Voucher_ID, temp.Voucher_ID, cons.User_ID, temp.User_ID, amount, 2, (DateTime)consumerVouchers[counter].voucherCreationDate);

                    db.SaveChanges();
                }

                temp.voucherValue = 0;
                db.SaveChanges();
            }
            return true;
        }

        [HttpGet]
        [ResponseType(typeof(String))]
        public async Task<String> CorrectTransactions()
        {
            //List<vouchertransaction> voucherTrans = (from c  in  db.vouchertransactions where ;
            //List<>


            string result = (from c in db.vouchertransactions where c.VoucherSentTo >= 961 select c.transactionAmount).Sum().ToString();
            result += "  " + (from c in db.vouchers where c.Voucher_ID >= 961 select c.voucherValue).Sum().ToString();

            voucher[] consumerVoucherList = (from c in db.vouchers where c.Voucher_ID >= 961 && c.VoucherType_ID == 2 select c).ToArray();

            foreach (voucher temp in consumerVoucherList)
            {
                temp.voucherCreationDate = null;
                temp.voucherValue = 0;
                db.SaveChanges();
            }


            return result;
        }



        [HttpGet]
        [ResponseType(typeof(Boolean))]
        public async Task<Boolean> PurchaseProducts()
        {

            List<voucher> voucherlist = (from c in db.vouchers where c.voucherValue >= 10 select c).ToList();
            product[] productList = (from c in db.products select c).ToArray();
            consumer[] consumerList = (from c in db.consumers select c).ToArray();

            foreach (voucher temp in voucherlist)
            {
                for (int i = 0; i < 5; i++)
                {
                    voucher newVoucher = new voucher();
                    newVoucher.User_ID = consumerList[r.Next(41)].User_ID;
                    newVoucher.voucherValue = (Decimal)((int)temp.voucherValue * 0.2);
                    newVoucher.VoucherType_ID = 2;
                    DateTime now = temp.voucherCreationDate.Value.AddHours(r.Next(150));
                    now.ToString("yyyy-MM-dd H:mm:ss");

                    newVoucher.voucherCreationDate = now;
                    db.vouchers.Add(newVoucher);
                    db.SaveChanges();
                }

                temp.voucherValue = 0;
                db.SaveChanges();
            }
            return true;
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
                        //await SendVoucher(resellerUserID, recipientID, transferAmount, 2, 2,DateTime.now);
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



        public async Task<IHttpActionResult> SendVoucher(int senderID, int receiverID, decimal amountToSend, int transactionType_ID, int voucherTypeID, DateTime datum)
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
            await db.SaveChangesAsync();


            for (int i = 0; i < senderVouchers.Count && toDeduct > 0; i++)
            {
                voucher temp = senderVouchers.ElementAt(i);

                if (toDeduct >= temp.voucherValue)
                {
                    toDeduct -= temp.voucherValue;
                    addVoucherTransaction(newVoucher.Voucher_ID, temp.Voucher_ID, receiverID, senderID, temp.voucherValue, transactionType_ID, datum);
                    temp.voucherValue = 0;
                }
                else
                {
                    temp.voucherValue -= toDeduct;
                    addVoucherTransaction(newVoucher.Voucher_ID, temp.Voucher_ID, receiverID, senderID, toDeduct, transactionType_ID, datum);
                    toDeduct = 0;
                }

            }

            return Ok();
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
                        //await SendVoucher(resellerUserID, recipientUserID, transferAmount, 2, 2);
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



        public bool isUserConsumer(int UserID)
        {
            return db.consumers.Where(c => c.User_ID == UserID).Any();
        }

        private void addVoucherTransaction(int newVoucherID, int voucherID, int receiverID, int senderID, decimal Amount, int transactionTypeID, DateTime datum)
        {
            vouchertransaction newTransaction = new vouchertransaction();
            newTransaction.VoucherSentTo = newVoucherID;
            newTransaction.Voucher_ID = voucherID;
            newTransaction.Receiver_ID = receiverID;
            newTransaction.Sender_ID = senderID;
            newTransaction.transactionAmount = Amount;
            newTransaction.TransactionType_ID = transactionTypeID;
            DateTime now = datum;
            now.ToString("yyyy-MM-dd H:mm:ss");
            newTransaction.transactionDate = now;
            //newTransaction.transactionDate = DateTime.Now;
            newTransaction.transactionDescription = "Reseller purchase of bulk voucher";

            db.vouchertransactions.Add(newTransaction);
            db.SaveChanges();
        }


        //public class userCleaner
        //{
        //    public string contactno { get; set; }
        //    public string IDno { get; set; }
        //    public string email { get; set; }
        //}


        //public class RandomDataController : ApiController
        //{
        //    nanofinEntities db = new nanofinEntities();
        //    String[] phoneNumbers = { "0713324361","0616795032", "0832989716", "0721595000", "0764739570" };
        //    String[] emails = {"tshigabel@outlook",  "tshigabel@gmail.com", "mark.garber94@gmail.com","margauxFourie@gmail.com", "chrisvdsande@gmail.com"};
        //    int EmailCounter =  0;
        //    int PhoneCounter = 0;
        //    Random r = new Random();



        //    [HttpPost]
        //    public async  Task<Boolean> addUsers(List<DTOuser> list)
        //    {
        //        foreach( DTOuser temp in list)
        //        {
        //            db.users.Add(EntityMapper.updateEntity(null,  temp));
        //            db.SaveChanges();
        //        }

        //        return true;
        //    }

        //    [HttpPost]
        //    public async Task<Boolean> addResellers(List<DTOreseller> list)
        //    {

        //        List<user> resellerUsers =  (from  c  in db.users where c.User_ID >= 111 select c ).ToList() ;

        //        for (int i  = 0; i < resellerUsers.Count - 1; i++)
        //        {
        //            list[i].User_ID = resellerUsers[i].User_ID;
        //            db.resellers.Add(EntityMapper.updateEntity(null, list[i]));
        //            db.SaveChanges();
        //        }

        //        return true;
        //    }

        //    [HttpPost]
        //    public async Task<Boolean> addConsumer(List<DTOconsumer> list)
        //    {

        //        List<user> consumerUsers = (from c in db.users where c.User_ID >= 541 select c).ToList();

        //        for (int i = 0; i < consumerUsers.Count - 1; i++)
        //        {
        //            list[i].User_ID = consumerUsers[i].User_ID;
        //            db.consumers.Add(EntityMapper.updateEntity(null, list[i]));
        //            db.SaveChanges();
        //        }

        //        return true;
        //    }


        //    [HttpPost]
        //    public async Task<Boolean> CleanData(List<userCleaner> list)
        //    {
        //        r = new Random();
        //        List<user> consumerUsers = (from c in db.users select c).ToList();

        //        foreach (user  temp in consumerUsers)
        //        {
        //            temp.userPassword = "$2a$09$tEPis8M8QuomEMZUsp5wrO0pjWPDN7sCvkwX0Bntmt5GDDW4uYqWK";
        //            temp.userContactNumber = GeneratePhoneNumber();
        //            temp.IDnumber = generateIDNo();
        //            EmailCounter++;
        //            db.SaveChanges();
        //        }

        //        return true;
        //    }


        //    private string GeneratePhoneNumber()
        //    {
        //        return phoneNumbers[EmailCounter % 5];
        //    }
        //    public string GenerateEmail()
        //    {            
        //        return emails[EmailCounter % 5];
        //    }
        //    public string generateIDNo()
        //    {
        //        string result = "";
        //        string year, month, day;
        //        day = month = year = "";

        //        year = (1970 +  r.Next() % 26).ToString();
        //        month = "0"+(r.Next() % 10).ToString();
        //        day = (r.Next() % 28).ToString();
        //        //1990-07-31T00:00:00
        //        return year + month + day + (1000 + r.Next() % 9000).ToString() +  "085";
        //    }
        //    public string GenerateDateTime()
        //    {
        //        string result = "";
        //        string year, month, day;
        //        day = month = year = "";

        //        year = (1970 + r.Next() % 26).ToString();
        //        month = (r.Next() % 12).ToString();
        //        day = (r.Next() % 28).ToString();
        //        //1990-07-31T00:00:00
        //        return year + "-" + month + "-" + day + "T" + (r.Next() % 24).ToString() + ":" + (r.Next() % 59).ToString() + (r.Next() % 59).ToString();
        //    }




        //}


        /*
            public class RandomWalletHandler : ApiController
            {
                //POST...add a voucher: use cases involved:
                //-reseller buys voucher online= reseller gets a brand new bulk voucher added
                //-= consumer gets a brand new normal voucher added
                DateTime start = new DateTime(2015, 6, 1, 1, 1, 1);
                DateTime end = new DateTime(2015, 7, 1, 1, 1, 1);
                //PUT...update a voucher: use cases involved:
                //-reseller sends voucher= update reseller's voucher amount
                private nanofinEntities db = new nanofinEntities();
                //POST...add a voucher: use cases involved:
                //-reseller buys voucher online= reseller gets a brand new bulk voucher added
                //-= consumer gets a brand new normal voucher added
                Random r = new Random();
                //PUT...update a voucher: use cases involved:
                //-reseller sends voucher= update reseller's voucher amount
                [HttpGet]
                public async Task<Boolean> Random_ResellerBuyBulk()
                {
                    Boolean toreturn = false;

                    List<user> resellers = (from c in db.users where c.userType == 21 select c).ToList();

                    int[] array = { 1, 2, 3 };


                    for( int  i  = 1; i <=  14; i++)
                    {

                        for(int e = 0;  e < i*3;e++)
                        {
                            int nuPurchases = array[r.Next(4)];

                            for (  int n = 0;  n <  nuPurchases; n++)
                                await buyBulkVoucher(resellers[e % 43].User_ID,55 + r.Next(300));
                        }
                        start = (start.AddMonths( 1));
                        end = end.AddMonths(1);
                        toreturn = true;
                    }

                    return toreturn;
                }

                DateTime RandomDay()
                {
                    int range = (end - start).Hours;
                    return start.AddHours(r.Next(range));
                }


                public async Task<IHttpActionResult> SendVoucher(int senderID, int receiverID, decimal amountToSend, int transactionType_ID, int voucherTypeID)
                {
                    if (getVoucherAccountBalance(senderID) < amountToSend)
                    {
                        return BadRequest("invalid funds"); // or custom responce for invalid funds
                    }

                    Decimal toDeduct = amountToSend;
                    List<voucher> senderVouchers = (from c in db.vouchers where c.User_ID == senderID && c.voucherValue > 0 orderby c.voucherValue ascending select c).ToList();
                    TimeSpan ts;

                    voucher newVoucher = new voucher();
                    newVoucher.User_ID = receiverID;
                    newVoucher.voucherValue = amountToSend;
                    newVoucher.VoucherType_ID = voucherTypeID;
                    DateTime now =  DateTime.Now;
                    now.ToString("yyyy-MM-dd H:mm:ss");
                    newVoucher.voucherCreationDate = now;
                    db.vouchers.Add(newVoucher);
                    await db.SaveChangesAsync();


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
                    DateTime now = RandomDay();
                    now.ToString("yyyy-MM-dd H:mm:ss");
                    newTransaction.transactionDate = now;
                    //newTransaction.transactionDate = DateTime.Now;

                    transactiontype transactionTypeString = (from list in db.transactiontypes where list.TransactionType_ID == transactionTypeID select list).Single();
                    newTransaction.transactionDescription = transactionTypeString.transactionTypeDescription;

                    db.vouchertransactions.Add(newTransaction);
                    db.SaveChanges();
                }


                public async Task<IHttpActionResult> buyBulkVoucher(int userID, decimal BulkVoucherAmount)
                {
                    if (isUserReseller(userID))
                    {
                        //need to check if reseller has the available funds in bank account - else return bad request or err page
                        //transfer money from reseller bank account to nanoFin account

                        voucher newVoucher = new voucher();
                        newVoucher.User_ID = userID;
                        newVoucher.voucherValue = BulkVoucherAmount;
                        newVoucher.VoucherType_ID = 1;
                        newVoucher.voucherCreationDate = DateTime.Now;
                        db.vouchers.Add(newVoucher);
                        await db.SaveChangesAsync();

                        addVoucherTransaction(newVoucher.Voucher_ID, newVoucher.Voucher_ID, userID, 0, BulkVoucherAmount, 1);

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
                                await SendVoucher(resellerUserID, recipientUserID, transferAmount, 2, 2);
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
                                await SendVoucher(resellerUserID, recipientID, transferAmount, 2, 2);
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
                        await SendVoucher(consumerUserID, recipientID, transferAmount, 21, 2);
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
                            await SendVoucher(consumerUserID, recipientUserID, transferAmount, 21, 2);
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
                        List<vouchertransaction> rawVoucherTransactionList = (from c in db.vouchertransactions where c.Receiver_ID == resellerUserID && c.Sender_ID == 0 && c.TransactionType_ID == 1 select c).ToList();

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
                [ResponseType(typeof(DTOvoucher))]
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
                            return Ok(voucherTransactionList);
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
            */

       public async void generatePurchases()
        {

            List<voucher> vouchersList = (from c in db.vouchers where c.User_ID == 111 || c.User_ID == 21 && c.voucherValue > 0 select c).ToList();
            insuranceproduct[] catalogList = (from c in db.insuranceproducts where c.ipCoverAmount >= 40 select c).ToArray();
            int max = catalogList.Count();

           foreach (voucher v  in vouchersList)
            {
               await redeemProduct(v.User_ID, catalogList[r.Next(max)].Product_ID,1+ r.Next(4), v.voucherCreationDate.Value.AddHours(r.Next(60)));
            }
        }

        public void setProductsActiveState()
        {
            DateTime july = new DateTime(2016, 7, 15);
            List<activeproductitem> items = db.activeproductitems.ToList();
            int counter = 1000;

            foreach ( activeproductitem  p  in items)
            {
                if(p.activeProductItemStartDate  <  july)
                {
                    p.isActive = false;
                }
                else
                {
                    p.isActive = true;
                }
                counter++;
                p.activeProductItemPolicyNum = "P"+counter.ToString();
                db.SaveChanges();
            }

        }
        


        #region RedeemProduct Code
        public async Task<IHttpActionResult> redeemProduct(int userID, int productID, int numberUnits, DateTime? startdate)
        {
            //check that the minimum number of units is applied according to what is in db:
            if (!isValidNumUnits(productID, numberUnits))
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

                //Update the 1 voucher table, 2 voucherTransaction table and the 3 productRedemptionLog table
                for (int i = 0; (i < vouchersList.Count) && (amountToDeduct > 0); i++)
                {
                    decimal voucherVal = vouchersList.ElementAt(i).voucherValue;

                    if (amountToDeduct > vouchersList.ElementAt(i).voucherValue)
                    {
                        //will have to finish up this small voucher, it's value becomes 0
                        //this voucher is recorded individually in the productRedemptionLog

                        vouchersList.ElementAt(i).voucherValue = 0;
                        db.Entry(vouchersList.ElementAt(i)).State = EntityState.Modified;
                        await db.SaveChangesAsync();

                        //addVoucherTransactionLog()
                        //addVoucherTransactionLog(vouchersList.ElementAt(i).Voucher_ID, vouchersList.ElementAt(i).Voucher_ID, userID, 0, 31, voucherVal, "Voucher Redemption for Product Purchase", startdate);

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
                        await db.SaveChangesAsync();

                        //add voucherTransactionLog()
                        //addVoucherTransactionLog(vouchersList.ElementAt(i).Voucher_ID, vouchersList.ElementAt(i).Voucher_ID, userID, 0, 31, amountToDeduct, "Voucher Redemption for Product Purchase", startdate);

                        //add productRedemptionLog
                        addProductRedemptionLog(activeProdItem.ActiveProductItems_ID, vouchersList.ElementAt(i).Voucher_ID, amountToDeduct);

                        //update amount to deduct
                        amountToDeduct = 0;
                    }

                }//for loop
                return StatusCode(HttpStatusCode.OK);
            }
            return BadRequest("Insufficient voucher total");
        }//RedeemProduct method

        [HttpGet]
        public bool isValidNumUnits(int productId, int numUnits)
        {
            //var dbMinUnits = db.insuranceproducts.Where(c => c.Product_ID == productId).Select(c => c.ipMinimunNoOfUnits);
            //return (numUnits >= int.Parse(dbMinUnits.FirstOrDefault().ToString()));
            return true;
        }

        //send in product id, numUnits return: totalPrice for the numUnits
        private decimal calcProductPrice(int prodID, int numUnits)
        {
            var prodUnitCost = (from c  in  db.insuranceproducts where c.Product_ID == prodID select c.ipUnitCost).First();
            //var prodUnitCost = db.insuranceproducts.Where(b => b.Product_ID == prodID).Select(p => p.ipUnitCost);
            return Decimal.Parse((numUnits * (Decimal)prodUnitCost).ToString());
        }

        //get the vouchers that this user currently has where the value is>0 in ascending order
        private List<voucher> getUserVouchersList(int userId)
        {
            List<voucher> list = new List<voucher>();
            list = (from v in db.vouchers where v.User_ID == userId && v.voucherValue > 0 orderby v.voucherCreationDate ascending select v).ToList();

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

        private activeproductitem createActiveProductItem(int ConsumerID, int ProductID, string policyNum, bool isActive, decimal productValue, int duration, DateTime? startDate)
        {
            activeproductitem activeProdItem = new activeproductitem();
            activeProdItem.Consumer_ID = ConsumerID;
            activeProdItem.Product_ID = ProductID;
            activeProdItem.activeProductItemPolicyNum = policyNum;
            activeProdItem.isActive = isActive;
            activeProdItem.productValue = productValue;
            activeProdItem.duration = duration;
            //startDate.ToString("yyyy-MM-dd H:mm:ss");
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




    }
}
