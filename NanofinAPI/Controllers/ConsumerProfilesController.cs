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

namespace NanofinAPI.Controllers
{
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

        public Boolean sendMessageToConsumer(string message , List<int> consumerReferences)
        {
            var notificationH = new NotificationController();

            foreach ( var  id  in consumerReferences)
            {
                var cons = db.consumers.Find(id);
                notificationH.SendSMS(cons.user.userContactNumber, message);
            }

            return true;
        }





    }
}
