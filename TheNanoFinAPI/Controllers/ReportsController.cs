using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NanofinAPI.Models;
using NanofinAPI.Models.DTO;
using System.Web.Http.Description;
using TheNanoFinAPI.Models;

namespace NanofinAPI.Controllers
{
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

        [HttpGet]
        public IEnumerable<ResellerSales> getBestReseller()
        {

            List<vouchertransaction> list = (from c in db.vouchertransactions where c.user.userType == 21 && c.TransactionType_ID == 2  select c).ToList();
            var toreturn = list.GroupBy(d => d.Sender_ID)
               .Select(
                        g => new ResellerSales
                        {
                            resellerID = g.Key,
                            voucherSent = g.Sum(s => s.transactionAmount),
                            address = db.resellers.SingleOrDefault(c => c.User_ID == g.Key).street,
                           
                        });

            string [] addresss;
            foreach ( ResellerSales p  in toreturn)
            {
                addresss = p.address.Split(':');
                p.lat = addresss[0];
                p.lng = addresss[1];
            }

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

        #endregion
    }
}
