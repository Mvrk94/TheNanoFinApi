using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NanofinAPI.Models;
using NanofinAPI.Models.DTOEnvironment;

namespace NanofinAPI.Controllers
{
    public class ProcessInsuranceApplicationsController : ApiController
    {
        database_nanofinEntities db = new database_nanofinEntities();

        [HttpGet]
        public List<DTOconsumerriskvalue> getUnprocessedApplications()
        {
            var toreturn = new List<DTOconsumerriskvalue>();
            var list = (from c in db.consumerriskvalues where c.numUnprocessed > 0 select c);

            foreach(var p  in list)
            {
                toreturn.Add(new DTOconsumerriskvalue(p));
            }

            return toreturn;
        }

        [HttpPost]
        public Boolean ProcessBatchApplication(int [] consumerList)
        {
          
            foreach( var  i  in consumerList)
            {
                db.processApplications1(i);
            }
            db.UpdateConsumerRiskValues();

            return true;
        }

        [HttpPost]
        public Boolean ProcessApplication(int  consumerID)
        {
            db.processApplications1(consumerID);
            db.UpdateConsumerRiskValues();
            return true;
        }

        [HttpGet]
        public List<unprocessedapplication> getConsummerUnProccessedPurchases(int ConsumerID)
        {
            return (from c in db.unprocessedapplications where c.Consumer_ID == ConsumerID select c).ToList();
        }

        [HttpPost]
        public Boolean ProcessSingleApplication(int activeProductID)
        {
            db.processSingleApplication(activeProductID);
            return true;
        }

        [HttpGet]
        public  List<consumerinfosummary> getUserInformation(int consumerID)
        {
            return db.consumerinfosummaries.Where(c => c.Consumer_ID == consumerID).ToList();
        }


    }
}
