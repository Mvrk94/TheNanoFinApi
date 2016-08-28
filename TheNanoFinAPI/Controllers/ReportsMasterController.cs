using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheNanoFinAPI.Models.DTOEnvironment;
using TheNanoFinAPI.Models;
using Extreme.Statistics;
using Extreme.Statistics.TimeSeriesAnalysis;

namespace TheNanoFinAPI.Controllers
{
    public class ReportsMasterController : ApiController
    {

        database_nanofinEntities db = new database_nanofinEntities();

        [HttpGet]
        public List<productTarget> TargetProgrees(int productProvider)
        {
            var toreturn = new List<productTarget>();
            var currentDate = DateTime.Now.AddMonths(-2);
            var salesPerProduct = (from c  in db.productsalespermonths where currentDate < c.activeProductItemStartDate.Value select c).ToList() ;
            

            foreach (var  p in salesPerProduct)
            {
                toreturn.Add(new productTarget
                {
                    ProductID = p.Product_ID,
                    currentSales = p.sales,
                    targetSales = db.products.Find(p.Product_ID).salesTargetAmount,
                    monthSate = p.activeProductItemStartDate,
                });
            }

            return toreturn;
        }

        [HttpGet]
        public List<productView> getProductList()
        {
            var toreturn = new List<productView>();
            var insuranceProductList = db.insuranceproducts.ToList();


            foreach (var  p in insuranceProductList)
            {
                toreturn.Add(new productView
                {
                    ProductID = p.Product_ID,
                    name = p.product.productName,
                    insuranceType = p.InsuranceType_ID
                });

            }

            return toreturn;
        }


        [HttpGet]
        public ProductForCast getProductPredictions( int productID, int numPredictions, int value1 = 1, int value2 = 5)
        {
            var toreturn = new ProductForCast();
            var pastSales = (from c in db.productsalespermonths where c.Product_ID== productID select c.sales.Value).ToList();

            toreturn.productID = productID;
            toreturn.name = db.products.Find(productID).productName;
            toreturn.previouse = Array.ConvertAll(pastSales.ToArray(), x => (double)x);          

            ArimaModel model = new ArimaModel(toreturn.previouse, value1, value2);
            model.Compute();

            toreturn.predictions = Array.ConvertAll(model.Forecast(numPredictions).ToArray(), x => (double)x);
            
            return toreturn;
        }


        [HttpGet]
        public overallForeCast getMonthlyForecast(int productProvider)
        {
            var toreturn = new overallForeCast();

            return toreturn;
        }


       [HttpGet]
       public List<LocationReports> getCurrentMonthSales()
        {
            var list = (from c in db.monthlylocationsales where c.activeProductItemStartDate.Value > DateTime.Now.AddMonths(-1) select c).ToList(); ;

            var toreturn = new List<LocationReports>();

            foreach (var temp in list)
            {
                toreturn.Add(new LocationReports
                {
                    date = temp.activeProductItemStartDate.Value,
                    latlng = db.locations.Find(temp.transactionLocation.Value).LatLng,
                    productID = temp.Product_ID,
                    sales = temp.sales.Value
                });
            }


            return toreturn;
        }

        [HttpGet]
        public List<LocationReports> getForecastMonthSales()
        {
            var list = (from c in db.monthlylocationsales select c).ToList(); ;

            var toreturn = new List<LocationReports>();

            foreach (var temp in list)
            {
                toreturn.Add(new LocationReports
                {
                    date = temp.activeProductItemStartDate.Value,
                    latlng = db.locations.Find(temp.transactionLocation.Value).LatLng,
                    productID = temp.Product_ID,
                    sales = temp.sales.Value
                });
            }

            return toreturn;
        }


      




        #region Utils
        private double []  generateTimeData(int size)
        {
            double[] timeS = new double[size ];
            var counter = 0;
            for (var i = 0; i < size; i++)
                timeS[i] = counter++;

            return timeS;
        }

        #endregion


























    }
}
