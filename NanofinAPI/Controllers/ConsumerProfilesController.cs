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

     



    }
}
