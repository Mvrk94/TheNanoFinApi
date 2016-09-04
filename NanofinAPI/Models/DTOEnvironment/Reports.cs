using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NanofinAPI.Models.DTO
{
    public class ProductStatus
    {
        public int productID { get; set; }
        public decimal overallUsage { get; set; }
        public string name { get; set; }
    }

    public class OverallPurchases
    {
        public int numOverallSales { get; set; }
        public int mySales { get; set; }

    }

    public class ResellerSales
    {
        public int resellerID { get; set; }
        public decimal voucherSent { get; set; }
        public string  lat { get; set; }
        public string lng { get; set; }
        public  string address { get; set; }
    }


    public class MonthlyInvoice
    {
        public int index;
        public string month;
        public int NumberofPurchases;
        public int unitsSold;
        public decimal OverallSales;
    }

}