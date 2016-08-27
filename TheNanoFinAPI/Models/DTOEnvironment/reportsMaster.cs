using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheNanoFinAPI.Models.DTOEnvironment
{
    
    public class productTarget
    {
        public int ProductID { get; set; }
        public decimal? currentSales { get; set; }
        public decimal? targetSales { get; set; }
        public DateTime? monthSate { get; set; }
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

}