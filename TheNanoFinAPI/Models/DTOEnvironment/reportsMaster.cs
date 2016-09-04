using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheNanoFinAPI.Models.DTOEnvironment
{
    
    public class productTarget
    {
        public string name { get; set; }
        public int ProductID { get; set; }
        public decimal? currentSales { get; set; }
        public decimal? targetSales { get; set; }
        public string monthSate { get; set; }
    }

    public class productView
    {
        public int ProductID { get; set; }
        public string name { get; set; }
        public int insuranceType { get; set; }
    }

    public class ProductForCast
    {
        public int productID { get; set; }
        public String name { get; set; }
        public  double [] predictions { get; set; }
        public double[] previouse { get; set; }
    }

    public class overallForeCast
    {
        public double[] predictions { get; set; }
        public double[] previouse { get; set; }
    }

    public class LocationReports
    {
        public DateTime? date { get; set; }
        public int productID { get; set; }
        public string latlng { get; set; }
        public decimal sales { get; set; }
    }

    public class MonthlyProvincialSales
    {
        public DateTime? activeProductItemStartDate { get; set; }
        public int ProductProvider_ID { get; set; }
        public string Province { get; set; }
        public string city { get; set; }
        public string LatLng { get; set; }
        public decimal sales { get; set; }

        public static explicit operator MonthlyProvincialSales(monthlyProvinceSales_Result v)
        {
            throw new NotImplementedException();
        }
    }


    public class DTOcompareProducts
    {
        public string name { get; set; }
        public double[] previouse { get; set; }
    }
}