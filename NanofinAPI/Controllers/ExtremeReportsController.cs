using Extreme.DataAnalysis;
using Extreme.Mathematics;
using Extreme.Mathematics.LinearAlgebra.IO;
using Extreme.Statistics;
using Extreme.Statistics.TimeSeriesAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Http;
using NanofinAPI.Models;

namespace NanofinAPI.Controllers
{
    public class ExtremeReportsController : ApiController
    {

        database_nanofinEntities db = new database_nanofinEntities();

        [HttpGet]
        public void Writetofile()
        {

            var list = db.productsalespermonths.ToList();
            var counter = 0;
            //before your loop
            var csv = new StringBuilder();

            foreach ( var temp in list)
            {
                var newLine = string.Format("{0};{1};{2}", temp.Product_ID, counter, temp.sales);
                csv.AppendLine(newLine);
                counter++;
            }

            var path = System.Web.Hosting.HostingEnvironment.MapPath("~/Reports/productSales.csv");            
            File.WriteAllText(path, csv.ToString());
        } 

        private  double []  convertToDouble(decimal? [] data)
        {
            double[] toreturn =  new double[data.Length];

            for (int i = 0; i < data.Length; i++)
                toreturn[i] = (double)data[i];

            //for (int i = data.Length; i < toreturn.Length; i++)
            //    toreturn[i] = (double)data[i - data.Length] + Math.Pow(2,i);

            return toreturn;
        }


        [HttpGet]
        public String ArimaModelPrediction (int productID, int numPredict,  int value1 =2 , int value2 =1)
        {
            var toreturn = new StringBuilder();

            // This QuickStart Sample fits an ARMA(2,1) model and
            // an ARIMA(0,1,1) model to sunspot data.

            var list = (from c  in db.productsalespermonths where c.Product_ID ==  productID select c.sales).ToArray();

            // The time series data is stored in a numerical variable:
            var data = convertToDouble(list);
            var sunspots = Vector.Create(data);

           
            ArimaModel model = new ArimaModel(sunspots, value1, value2);

            // The Compute methods fits the model.
            model.Compute();
            
           
            toreturn.AppendLine(String.Format("Log-likelihood: {0}", model.LogLikelihood));
            // as is the Akaike Information Criterion (AIC):
            toreturn.AppendLine(String.Format("AIC: {0}", model.GetAkaikeInformationCriterion()));
            // and the Baysian Information Criterion (BIC):
           toreturn.AppendLine(String.Format("BIC: {0}", model.GetBayesianInformationCriterion()));

            // The Forecast method can be used to predict the next value in the series...
            double nextValue = model.Forecast();
            toreturn.AppendLine(String.Format("One step ahead forecast: {0}", nextValue));

            // or to predict a specified number of values:
            var nextValues = model.Forecast(numPredict);
           // toreturn.AppendLine(String.Format("First five forecasts: {0}", nextValues));

            for (int i = 0; i < numPredict; i++)
                toreturn.Append((int)nextValues[i] + " - ");

            toreturn.AppendLine(sunspots.ToArray().ToString());

            toreturn.AppendLine("values : ");
            for (int i = 0; i < data.Length; i++)
                toreturn.Append((int)data[i] + " - ");


             return toreturn.ToString();
        }


        [HttpGet]
        public String consumerFile()
        {
            var data = ReadAttendanceData();

            // Now create the regression model. Parameters are the name 
            // of the dependent variable, a string array containing 
            // the names of the independent variables, and the VariableCollection
            // containing all variables.

            
                


           var model = new GeneralizedLinearModel(data,
                "sales ~ id + date");

            // The ModelFamily specifies the distribution of the dependent variable.
            // Since we're dealing with count data, we use a Poisson model:
            model.ModelFamily = ModelFamily.Poisson;

            // The LinkFunction specifies the relationship between the dependent variable
            // and the linear predictor of independent variables. In this case,
            // we use the canonical link function, which is the default.
            model.LinkFunction = ModelFamily.Poisson.CanonicalLinkFunction;

            // The Compute method performs the actual regression analysis.
            model.Compute();

            StringBuilder toreturn = new StringBuilder();
            
            // The Parameters collection contains information about the regression 
            // parameters.
            toreturn.AppendLine("Variable              Value    Std.Error    z     p-Value");
            foreach (Parameter parameter in model.Parameters)
            {
                // Parameter objects have the following properties:
                toreturn.AppendLine(String.Format(  "{0,-20}{1,10:F6}{2,10:F6}{3,8:F2} {4,7:F5}",
                    // Name, usually the name of the variable:
                    parameter.Name.ToString(),
                    // Estimated value of the parameter:
                    parameter.Value.ToString(),
                    // Standard error:
                    parameter.StandardError.ToString(),
                    // The value of the z score for the hypothesis that the parameter
                    // is zero.
                    parameter.Statistic.ToString(),
                    // Probability corresponding to the t statistic.
                    parameter.PValue.ToString()));
            }
            toreturn.AppendLine("\r\n");
            // In addition to these properties, Parameter objects have a GetConfidenceInterval
            // method that returns a confidence interval at a specified confidence level.
            // Notice that individual parameters can be accessed using their numeric index.
            // Parameter 0 is the intercept, if it was included.
            Interval confidenceInterval = model.Parameters[0].GetConfidenceInterval(0.95);
            toreturn.AppendLine("95% confidence interval for math score: " +
                confidenceInterval.LowerBound.ToString() + " " + confidenceInterval.UpperBound.ToString());

            // Parameters can also be accessed by name:
            confidenceInterval = model.Parameters.Get("date").GetConfidenceInterval(0.95);
            toreturn.AppendLine("95% confidence interval for math score: " + 
                confidenceInterval.LowerBound.ToString() + " " + confidenceInterval.UpperBound.ToString());

            toreturn.AppendLine("\r\n");
            // There is also a wealth of information about the analysis available
            // through various properties of the GeneralizedLinearModel object:
            toreturn.AppendLine("Log likelihood:         " +  model.LogLikelihood);
            toreturn.AppendLine("Kernel log likelihood:  "+ model.GetKernelLogLikelihood());

            

            toreturn.AppendLine("\r\n");
            // Note that some statistical applications (notably stata) use 
            // a different definition of some of these "information criteria":
            toreturn.AppendLine("\"Information Criteria\"");
            toreturn.AppendLine("Akaike (AIC):           " +model.GetAkaikeInformationCriterion());
            toreturn.AppendLine("Corrected AIC:          "+ model.GetCorrectedAkaikeInformationCriterion());
            toreturn.AppendLine("Bayesian (BIC):         " +model.GetBayesianInformationCriterion());
            toreturn.AppendLine("Chi Square:             "+ model.GetChiSquare());
            toreturn.AppendLine("\r\n");
            toreturn.AppendLine(model.Predictions.ToString());

            return toreturn.ToString();
        }

        public static DataFrame<long, string> ReadAttendanceData()
        {
            DelimitedTextMatrixReader reader = new DelimitedTextMatrixReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Reports/productSales.csv"));
            reader.StartRow = 1;
            reader.SetColumnDelimiters(new char[] { ';' });
            reader.SetRowDelimiters(new char[] { '\r', '\n' });
            reader.MergeConsecutiveDelimiters = false;
            var m = reader.ReadMatrix();
            var columnIndex = Index.Create(new string[] {
                "id", "date", "sales"});
            return m.ToDataFrame(Index.Default(m.RowCount), columnIndex);

        }
    }


   
}
