using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Http.Description;
using NanofinAPI.Models;
using NanofinAPI.Models.DTOEnvironment;
using MultiChainLib.Controllers;
using TheNanoFinAPI.MultiChainLib.Controllers;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace NanofinAPI.Controllers
{
    public class PaymentGatewayController : ApiController
    {
        private nanofinEntities db = new nanofinEntities();

        [HttpGet]
        public  Dictionary<string, dynamic> InitRequest(double amount)
        {
            Dictionary<string, dynamic> responseData;
            string data = "authentication.userId=8a82941757d792dd0157d797d57d002d" +
                "&authentication.password=bCyBZ25TXr" +
                "&authentication.entityId=8a82941757d792dd0157d799bb04003b" +
                "&amount=" + amount +
                "&currency=ZAR" +
                "&paymentType=DB";
            string url = "https://test.oppwa.com/v1/checkouts";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            Stream PostData = request.GetRequestStream();
            PostData.Write(buffer, 0, buffer.Length);
            PostData.Close();
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                responseData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(reader.ReadToEnd());
                reader.Close();
                dataStream.Close();
            }
            return responseData;
        }

        [HttpGet]
        public Dictionary<string, dynamic> getStatus(string ID)
        {
            Dictionary<string, dynamic> responseData;
            string data = "authentication.userId=8a82941757d792dd0157d797d57d002d" +
                "&authentication.password=bCyBZ25TXr" +
                "&authentication.entityId=8a82941757d792dd0157d799bb04003b";
            string url = "https://test.oppwa.com/v1/checkouts/" + ID + "/payment?" + data;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                responseData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(reader.ReadToEnd());
                reader.Close();
                dataStream.Close();
            }
            return responseData;
        }

    }

}