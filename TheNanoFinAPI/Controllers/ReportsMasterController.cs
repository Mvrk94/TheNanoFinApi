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
        public List<productTarget> TargetProgrees(int productProvider, int numMonths)
        {
            var toreturn = new List<productTarget>();
            var currentDate = DateTime.Now.AddMonths(numMonths*-1);
            var salesPerProduct = (from c  in db.saleslastmonths where c.datum == "2016-08" select c).ToList() ;
            

            foreach (var  p in salesPerProduct)
            {
                toreturn.Add(new productTarget
                {
                    name = p.productName, 
                    ProductID = p.Product_ID,
                    currentSales = p.sales,
                    targetSales = db.products.Find(p.Product_ID).salesTargetAmount,
                    monthSate = p.datum,
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
        public DTOcompareProducts getMonthyProductSales(int productID)
        {
            var toreturn = new DTOcompareProducts();
            var pastSales = (from c in db.productsalespermonths where c.Product_ID == productID
                             select c.sales.Value).ToList();

            toreturn.name = db.products.Find(productID).productName;
            toreturn.previouse = Array.ConvertAll(pastSales.ToArray(), x => (double)x);

            return toreturn;
        }

        [HttpGet]
        public ProductForCast getProductSalesPredictions( int productID, int numPredictions, int value1 = 1, int value2 = 5)
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
        public overallForeCast getPPMonthlySalesForecast(int productProvider, int numPredictions,int value1 = 1, int value2 = 5)
        {
            var toreturn = new overallForeCast();
            var monthlysales = (from c in db.salespermonths select c.sales.Value).ToList();

            toreturn.previouse = Array.ConvertAll(monthlysales.ToArray(), c => (double)c);
            ArimaModel model = new ArimaModel(toreturn.previouse, value1, value2);
            model.Compute();

            toreturn.predictions = Array.ConvertAll(model.Forecast(numPredictions).ToArray(), x => (double)x);

            return toreturn;
        }


        [HttpGet]
        public List<DTOlastmonthprovincesale> get_PP_ProvincialSales()
        {
            var sales = (from c in db.lastmonthprovincesales select c).ToList();
            var toreturn = new List<DTOlastmonthprovincesale>();

            foreach( var temp  in  sales)
            {
                toreturn.Add(new DTOlastmonthprovincesale(temp));
            }

            return toreturn;
        }

       [HttpGet]
       public List<DTOmonthlylocationsale> GetMonthlyProductsalesperlocation(int productID , int locationID)
        {
            DateTime current = DateTime.Now.AddMonths(-1);
            var list = (from c in db.monthlylocationsales where c.Product_ID == productID && c.transactionLocation == locationID select c).ToList(); ;
            var locals = db.locations;
            var toreturn = new List<DTOmonthlylocationsale>();

            foreach (var temp in list)
            {
                toreturn.Add(new DTOmonthlylocationsale(temp));
            }
            
            return toreturn;
        }



        [HttpGet]
        public overallForeCast PredictLocationProductSales( int  productID, int locationID, int numPredictions, int value1 = 1, int value2 = 5)
        {
            var toreturn = new overallForeCast();
            var monthlysales = (from c in db.monthlylocationsales where c.transactionLocation == locationID && c.Product_ID == productID select c.sales.Value).ToList();

            toreturn.previouse = Array.ConvertAll(monthlysales.ToArray(), c => (double)c);
            ArimaModel model = new ArimaModel(toreturn.previouse, value1, value2);
            model.Compute();

            toreturn.predictions = Array.ConvertAll(model.Forecast(numPredictions).ToArray(), x => (double)x);

            return toreturn;
        }


        [HttpGet]
        public List<DTOmonthlyproductsalesperlocation> getProductLocationExpenditure(int productID)
        {
            var list = (from c in db.monthlyproductsalesperlocations where c.datum == "2016-08" && c.Product_ID == productID select c).ToList();

            var toreturn = new List<DTOmonthlyproductsalesperlocation>();

            foreach (var temp in list)
            {
                toreturn.Add(new DTOmonthlyproductsalesperlocation(temp));
            }

            return toreturn;
        }




        [HttpGet]
          public List<lastmonthinsurancetypesale>  getLastMonthInsuranceTypeSales()
        {
            var toreturn = new List<lastmonthinsurancetypesale>();

            toreturn = db.lastmonthinsurancetypesales.ToList();

            return toreturn;
        }

        [HttpGet]
        public overallForeCast PredictInsuranceTypeSales(int insuranceTypeID, int numPredictions, int value1 = 1, int value2 = 5)
        {
            var toreturn = new overallForeCast();
            var monthlysales = (from c in db.insuranceproducttypemonthlysales where c.InsuranceType_ID == insuranceTypeID select c.monthSales.Value).ToList();

            toreturn.previouse = Array.ConvertAll(monthlysales.ToArray(), c => (double)c);
            ArimaModel model = new ArimaModel(toreturn.previouse, value1, value2);
            model.Compute();

            toreturn.predictions = Array.ConvertAll(model.Forecast(numPredictions).ToArray(), x => (double)x);

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
