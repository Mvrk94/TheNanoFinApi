using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NanofinAPI.Models;
using NanofinAPI.Models.DTO;
using System.Web.Http.Description;
using System.Text;
using System.IO;

namespace NanofinAPI.Controllers
{
    public class DashBoardDTO
    {
        public decimal monthsales;
        public decimal yearSale;
        public decimal numMembers;
        public decimal claims;
        public decimal salesToday;
    }


    public class ReportsController : ApiController
    {

        public nanofinEntities db = new nanofinEntities();

        #region dashBoard
        [HttpGet]
        public List<ProductStatus> getBestSellingProduct()
        {
            List<activeproductitem> list = (from c in db.activeproductitems select c).ToList();

            var datalist = list.GroupBy(d => d.Product_ID)
           .Select(
                    g => new ProductStatus
                    {
                        productID = g.Key,
                        overallUsage = g.Sum(s => s.productValue),
                        name = g.First().product.productName,
                    });

            return datalist.ToList();
        }

       [HttpGet]
        public OverallPurchases getOverallPurchases(int Provider_ID)
        {
            return new OverallPurchases
            {
                numOverallSales = db.activeproductitems.Where(c => c.product.ProductProvider_ID == Provider_ID).Count(),
                mySales = db.activeproductitems.Count()
            };
        }

       [HttpGet]
        public int getMembers(int ProviderID)
        {
            return (from c  in  db.activeproductitems where  c.product.ProductProvider_ID == ProviderID select c.Consumer_ID).Distinct().Count();
            //return db.activeproductitems.Where(c => c.product.ProductProvider_ID == ProviderID).Distinct().Count();
        }

        [HttpGet]
        public int getNumberOfUnprocessedApplications(int ProviderID)
        {
            return db.activeproductitems.Where(c => c.product.ProductProvider_ID == ProviderID && c.activeProductItemPolicyNum == "").Count();
        }

        //[HttpGet]
        //public IEnumerable<ResellerSales> getBestReseller()
        //{

        //    List<vouchertransaction> list = (from c in db.vouchertransactions where c.user.userType == 21 && c.TransactionType_ID == 2  select c).ToList();
        //    var toreturn = list.GroupBy(d => d.Sender_ID)
        //       .Select(
        //                g => new ResellerSales
        //                {
        //                    resellerID = g.Key,
        //                    voucherSent = g.Sum(s => s.transactionAmount),
        //                    address = db.resellers.SingleOrDefault(c => c.User_ID == g.Key).street,
                           
        //                });

        //    string [] addresss;
        //    foreach ( ResellerSales p  in toreturn)
        //    {
        //        addresss = p.address.Split(':');
        //        p.lat = addresss[0];
        //        p.lng = addresss[1];
        //    }

        //        return toreturn;
        //}

           [HttpGet]
           public int getCurrentNumberOfClaims()
            {
                return db.claims.Count();
            }
        
            [HttpGet]
            public decimal getYearSales(int productProviderID)
            {
                return (Decimal)db.productprovideryearlysales.Single(c=> c.ProductProvider_ID == productProviderID).yearSales;
            }

            [HttpGet]
            public decimal getThisMonthSales()
            {
                return (Decimal)(from c in db.salespermonths where c.datum == "2016-09" select c.sales).First();
            }


        private decimal getCurrentDaySales()
        {
            var datum = DateTime.Now;
            
            datum =  datum.AddHours(datum.TimeOfDay.Hours * -1);
            var sales = db.currentmonthdailysales.Where(x => x.activeProductItemStartDate > datum).ToList();
            var str = DateTime.Now.TimeOfDay;
            decimal toreturn = 0;

            foreach( var r  in sales)
            {
                toreturn += (Decimal)r.sales;
            }
            
            return toreturn;
        }

        [HttpGet]
        public DashBoardDTO getDashboard(int productProviderID)
        {
            
            var toreturn = new DashBoardDTO()
            {
                claims = getCurrentNumberOfClaims(),
                yearSale = getYearSales(productProviderID),
                numMembers = getMembers(productProviderID),
                monthsales = getThisMonthSales(),
                salesToday = getCurrentDaySales(),
            };
            return toreturn;
        }

        #endregion

        #region Invoices
        [HttpPost]
        public Boolean setResellerLocations(List<String> address)
        {
            Random b  =  new Random();
            List<reseller> list = (from c in db.resellers select c).ToList();
            int i = 0;

            foreach ( reseller r  in list)
            {
                r.street = address[i];
                i++;
                db.SaveChanges();
            }
            
            return true;
        }


        public IEnumerable<MonthlyInvoice> getInvoices(int providerID)
        {
            List<activeproductitem> purchasedProducts = (from c in db.activeproductitems where c.product.ProductProvider_ID ==  providerID orderby c.activeProductItemStartDate select c).ToList();
            IEnumerable<MonthlyInvoice> toreturn = new List<MonthlyInvoice>();
            int counter = 0;
            toreturn = purchasedProducts.GroupBy(d => d.activeProductItemStartDate.Value.ToString("yyyy-MM"))
          .Select(
                   g => new MonthlyInvoice
                   {
                       month = g.Key,
                       OverallSales = g.Sum(s => s.productValue),
                       NumberofPurchases = g.Count(),
                       unitsSold = g.Sum(s => s.duration),
                       index = counter++
                    });
            
            return toreturn;
        }



        public void writeToCSV()
        {

            var csv = new StringBuilder();
            List<productsalespermonth> prods = db.productsalespermonths.ToList();
            //in your loop
            foreach (var temp in prods)
            {
                var newLine = string.Format("{0},{1},{2}", temp.datum, temp.Product_ID, temp.sales);
                csv.AppendLine(newLine);


            }
            File.WriteAllText("data.csv", csv.ToString());
        }

        #endregion



        [HttpGet]
        public List<resellersalespermonth> getResellerProfitPerMonth(int resellerID)
        {
            return (from c in db.resellersalespermonths where c.Sender_ID == resellerID orderby c.transactionDate ascending select c ).ToList();
        }
    }
}
