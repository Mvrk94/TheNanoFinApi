using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheNanoFinAPI.Models.DTOEnvironment;
using TheNanoFinAPI.Models;
using Extreme.Statistics;

namespace TheNanoFinAPI.Controllers
{
    public class ReportsMasterController : ApiController
    {

        database_nanofinEntities db = new database_nanofinEntities();

        [HttpGet]
        public List<productTarget> TargetProgrees(int productProvider)
        {
            var toreturn = new List<productTarget>();
            var currentDate = DateTime.Now.AddMonths(-1);
            var salesPerProduct = (from c  in db.productsalespermonths where currentDate > c.activeProductItemStartDate select c).ToList() ;
            

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
        public ProductForCast getProductPredictions( int productID, int numPredictions)
        {
            var toreturn = new ProductForCast();
            var pastSales = (from c in db.productsalespermonths where c.Product_ID== productID select c).ToList();

            toreturn.productID = productID;
            toreturn.name = pastSales.ElementAt(0).productName;

            toreturn.previouse = Array.ConvertAll(pastSales.Select(c => c.sales).ToArray(), x => (double)x);

            int arrayLenght = toreturn.previouse.Length;
            SimpleRegressionModel model1 = new SimpleRegressionModel(generateTimeData(arrayLenght + numPredictions), toreturn.previouse);
            model1.NoIntercept = true;
            model1.Compute();
           toreturn.predictions =  Array.ConvertAll(model1.Predictions.ToArray(), x => (double)x);

            return toreturn;
        }


        [HttpGet]
        public overallForeCast getMonthlyForecast(int productProvider)
        {
            var toreturn = new overallForeCast();


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
