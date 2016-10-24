using NanofinAPI.Models;
using NanofinAPI.Models.DTOEnvironment;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Extreme.Statistics.TimeSeriesAnalysis;

namespace NanofinAPI.Controllers
{
    public class predictions
    {
        public string  values { get; set;}
    }

    public  class ClientMessage
    {
        public string message { get; set; }
        public string IDs { get; set; }
    }

    public class ConsumerProfilesController : ApiController
    {
        database_nanofinEntities db = new database_nanofinEntities();

        public List<consumerprofiledata> getConsumerProfileData()
        {
            return db.consumerprofiledatas.ToList();
        }

        public List<consumerpreferencesreport> getPreferencesReports()
        {
            return db.consumerpreferencesreports.ToList();
        }

        public List<DTOconsumergroup> getConsumerGroups()
        {
            var toreturn = new List<DTOconsumergroup>();
            var list = db.consumergroups;

            foreach(var temp in  list)
            {
                toreturn.Add(new DTOconsumergroup(temp));
            }

            return toreturn;
        }


        public DTOconsumergroup getSingleConsumerGroup( int consumerGroupID)
        {
            var temp = db.consumergroups.Find(consumerGroupID);
            return new DTOconsumergroup(temp);
        }

        public Boolean AddConsumerGroups(DTOconsumergroup newGroup)
        {
            var newGroupEntity = EntityMapper.updateEntity(null, newGroup);

            db.consumergroups.Add(newGroupEntity);
            db.SaveChanges();
            return true;
        }

        public Boolean updateConsumerGroup(DTOconsumergroup updateGroup)
        {

            var updateded = db.consumergroups.Find(updateGroup.idconsumerGroups);

            EntityMapper.updateEntity(updateded, updateGroup);

            db.Entry(updateded).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }

        public Boolean sendMessageToConsumer(ClientMessage advt)
        {
            var notificationH = new NotificationController();
            var consumerReferences = advt.IDs.Split(',').Select(Int32.Parse).ToList();

            foreach ( var  id  in consumerReferences)
            {
                var cons = db.consumers.Find(id);
                notificationH.SendSMS(cons.user.userContactNumber, advt.message);
            }

            return true;
        }

        public List<double> getPredictions(predictions prevValueStr, int numPredictions, int value1 = 1, int value2 = 1)
        {
            List<double> toreturn = new List<double>();

            var prevValues = prevValueStr.values.Split(',').Select(Int32.Parse).ToList();

            toreturn.AddRange(Array.ConvertAll(prevValues.ToArray(), c => (double)c));
            ArimaModel model = new ArimaModel(toreturn.ToArray(), value1, value2);
            model.Compute();

            toreturn.AddRange(Array.ConvertAll(model.Forecast(numPredictions).ToArray(), x => (double)x));
            return toreturn;
        }



    }
}
