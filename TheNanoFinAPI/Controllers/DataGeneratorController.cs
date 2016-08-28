using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheNanoFinAPI.Models;
using TheNanoFinAPI.Models.DTOEnvironment;
using NanoFinAPI.Controllers;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace TheNanoFinAPI.Controllers
{
    public class DTOlocationTemp
    {
        public int Location_ID { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string LatLng { get; set; }
        public string PostalCode { get; set; }
    }


        public class ConsumerPurchases
    {
        public  int ConsumerID;
        public int[] data = { 0, 0, 0, 0 };
        public decimal? cost;
    }



    public class DataGeneratorController : ApiController
    {

        nanofinEntities db = new nanofinEntities();

        [HttpGet]
        public async Task<Boolean> SetConsumerData()
        {
            Boolean toreturn = false;
            String[] gender = { "Male", "Female" };
            Random r = new Random();
            var list = db.consumers.ToList();

            foreach ( consumer  temp  in list)
            {
                //temp.gender = gender[r.Next() % 2];
                //temp.maritalStatus = (r.Next(100) > 55) ? "Single" : "Married";
                //temp.employmentStatus = (r.Next(100) > 80) ? "Unemplyed" : "Employed";
                temp.numDependant = (r.Next(4));
                //int gross = 1200 + (r.Next(3800));
                //temp.grossMonthlyIncome = gross;
                //temp.nettMonthlyIncome = (Decimal) (1 +  gross*  0.15) ;
                //temp.totalMonthlyExpenses = gross *(Decimal) 0.5;
               await db.SaveChangesAsync();
               
            }

 toreturn = true;
            return toreturn;
        }


        [HttpPost]
        public Boolean  PostLOcations(List<DTOlocationTemp> locat)
        {
            Boolean toreturn = true;

            foreach(DTOlocationTemp dto in locat)
            {
                var entityObjct = new location();

                entityObjct.Location_ID = dto.Location_ID;
                entityObjct.Province = dto.Province;
                entityObjct.City = dto.City;
                entityObjct.LatLng = dto.LatLng;
                entityObjct.PostalCode = dto.PostalCode;
                db.locations.Add(entityObjct);
                db.SaveChangesAsync();
            }
            
            return toreturn;
        }

        [HttpGet]
        public Boolean ResellerPurchaseBulk()
        {

            List<reseller> res = db.resellers.ToList();
            WalletHandlerController wc = new WalletHandlerController();
            

            foreach(reseller temp  in res)
            {
              //  wc.buyBulkVoucher(temp.User_ID, );
            }



            return true;
        }


        [HttpGet]
        public  int DeactivteProducts()
        {
            int toreturn = 0;

            List<activeproductitem> prod = db.activeproductitems.ToList();

            foreach (var temp  in prod)
            {
                temp.isActive = false;
                temp.activeProductItemPolicyNum = "PP-IM-" + toreturn;
                db.SaveChanges();
                toreturn++;
            }

            return toreturn;
        }



        [HttpGet]
        public Boolean GenerateTransactiions()
        {

            var res = db.resellers.ToArray();
            var con = db.consumers.ToArray();
            WalletHandlerController wc = new WalletHandlerController();
            ConsumerWalletHandlerController cw = new ConsumerWalletHandlerController();

            int[] healthOptions = { 211, 221, 231 };
            int[] familyOptions = { 161, 151, 141 };
            int[] individualOptions = { 131, 121, 101 };



            int ConsumerIncrement = 3;
            int ConsumerThisMonth = 0;

            Random random = new Random();
            int resllerCounter;

            DateTime  LASTYear  =  new DateTime (2016,08,01);
             resllerCounter = 14;
            while(LASTYear < DateTime.Now)
            {

                
                for (int  r = 0;  r < resllerCounter; r++)
                {
                    
                    List<ConsumerPurchases> purchases = new List<ConsumerPurchases>();
                    consumer temp;
                    List<product> prod = db.products.ToList();
                    decimal? overallCost = 0;

                    for(int c  = r* ConsumerIncrement; c <  (r+1)*ConsumerIncrement ; c++)
                    {
                        temp = con.ElementAt(c);
                        int assets = random.Next(100);
                        int travel = random.Next(100);
                        int funeral = random.Next(100);
                        int medical = random.Next(100);
                        decimal? salary = temp.grossMonthlyIncome;
                        ConsumerPurchases purchase = new ConsumerPurchases();
                        purchase.ConsumerID = con[c].User_ID;
                        purchase.cost = 0;
                        int purchaseNo = 0;

                        if (assets < 85)
                        {
                            if (salary > 3000)
                            {
                                purchase.cost += db.insuranceproducts.Single(e => e.Product_ID == (31)).ipUnitCost;
                                purchase.data[purchaseNo] = 31;
                            }
                            else
                            {
                                purchase.cost += db.insuranceproducts.Single(e => e.Product_ID == (241)).ipUnitCost;
                                purchase.data[purchaseNo] = 241;
                            }
                            purchaseNo++;
                        }

                        if (travel <76)
                        {
                            if (salary > 4000)
                            {
                                purchase.data[purchaseNo] = 91;
                                purchase.cost += db.insuranceproducts.Single(e => e.Product_ID == (91)).ipUnitCost;
                            }
                            else if ( temp.employmentStatus == "Employed")
                            {
                                purchase.cost += db.insuranceproducts.Single(e => e.Product_ID == (41)).ipUnitCost;
                                purchase.data[purchaseNo] = 41;
                            }
                            purchaseNo++;
                        }

                        if (medical < 69)
                        {
                            if (salary > 4000)
                            {
                                purchase.data[purchaseNo] = healthOptions[medical%3];
                                int pos = healthOptions[medical % 3];
                                purchase.cost += db.insuranceproducts.Single(e => e.Product_ID == pos).ipUnitCost;
                            }
                            else if (salary > 2600)
                            {
                                purchase.data[purchaseNo] = 71;
                                purchase.cost += db.insuranceproducts.Single(e => e.Product_ID == (71)).ipUnitCost;
                            }
                            purchaseNo++;
                        }

                        if (funeral < 95)
                        {

                            if (temp.maritalStatus.CompareTo("Single") == 1)
                            {
                                if (salary > 4000)
                                {
                                    purchase.data[purchaseNo] = 131;
                                    purchase.cost += db.insuranceproducts.Single(e => e.Product_ID == (131)).ipUnitCost;
                                }
                                else if (salary > 3000)
                                {
                                    purchase.data[purchaseNo] = 121;
                                    purchase.cost += db.insuranceproducts.Single(e => e.Product_ID == (121)).ipUnitCost;
                                }
                                else if (salary > 2000)
                                {
                                    purchase.data[purchaseNo] = 101;
                                    purchase.cost += db.insuranceproducts.Single(e => e.Product_ID == (101)).ipUnitCost;
                                }
                            }

                            if (temp.maritalStatus.CompareTo("Married") == 1 || temp.numDependant >= 2)
                            {
                                if (salary > 4000)
                                {
                                    purchase.data[purchaseNo] = 161;
                                    purchase.cost += db.insuranceproducts.Single(e => e.Product_ID == (161)).ipUnitCost;
                                }
                                else if (salary > 3000)
                                {
                                    purchase.data[purchaseNo] = 151;
                                    purchase.cost += db.insuranceproducts.Single(e => e.Product_ID == (151)).ipUnitCost;
                                }
                                else if (salary > 2000)
                                {
                                    purchase.data[purchaseNo] = 141;
                                    purchase.cost += db.insuranceproducts.Single(e => e.Product_ID == (141)).ipUnitCost;
                                }
                            }

                            purchaseNo++;
                        }
                        overallCost += purchase.cost;
                        purchases.Add(purchase);
                    }
                    int resID = res.ElementAt(r).User_ID;
                    wc.buyBulkVoucher(resID, (Decimal)overallCost + 60, LASTYear.AddHours( random.Next(24))); 


                    foreach( var s  in purchases)
                    {
                        wc.sendBulkVoucher(resID, s.ConsumerID, (Decimal)s.cost +3 , LASTYear.AddHours(24 + random.Next(45)));
                        int count = 0;
                        foreach( var pur  in s.data)
                        {
                            count++;
                            if (pur != 0) cw.redeemProduct(s.ConsumerID, pur, 1, LASTYear.AddHours(168*count +random.Next(150)));
                        }
                    }

                }

                if (resllerCounter < 14) resllerCounter++;
                else
                    resllerCounter =9;
           
                LASTYear = LASTYear.AddMonths(1);
            }


           


            return true;
        }


        

        // POST: api/testManager
        [ResponseType(typeof(DTOactiveproductitem))]
        public async Task<DTOactiveproductitem> Postactiveproductitem(DTOactiveproductitem newDTO)
        {
            activeproductitem newProd = EntityMapper.updateEntity(null, newDTO);
            db.activeproductitems.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        public Boolean setReseller_Consumer_Location()
        {
            var consm = (from c in db.consumers select c).ToArray();
            var res = (from c in db.resellers select c).ToArray();
            var loc = (from c in db.locations select c).ToArray();
            var leng = loc.Length;
            var rand = new Random();
            var resLeng = res.Length;
            var conLeng = consm.Length;
            var locationID = 0;

            for( int r = 0;  r <  15; r++)
            {
                locationID = loc[rand.Next(leng)].Location_ID;
                res[r].LocationID = locationID;
                for (int c = r*3; c < (r+1)*3; c++)
                {
                    consm[c].Location_ID = locationID;
                }
                db.SaveChangesAsync();
            }

            return true;
        }


        // GET: api/testManager
        public List<DTOactiveproductitem> Getactiveproductitem()
        {
            List<DTOactiveproductitem> toReturn = new List<DTOactiveproductitem>();
            List<activeproductitem> list = (from c in db.activeproductitems select c).ToList();

            foreach (activeproductitem p in list)
            {
                toReturn.Add(new DTOactiveproductitem(p));
            }

            return toReturn;
        }

    }



}
