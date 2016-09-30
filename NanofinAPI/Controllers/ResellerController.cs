using NanofinAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NanofinAPI.Controllers
{
    public class DTOresellerLocation
    {
        public int Reseller_ID { get; set; }
        public int User_ID { get; set; }
        public string sellingLocation { get; set; }
        public string isSharingLocation { get; set; }
        public Nullable<System.DateTime> StartedSharingTime { get; set; }
        public Nullable<int> minutesAvailable { get; set; }

        public DTOresellerLocation() { }

        public DTOresellerLocation(reseller entityObjct)
        {
            Reseller_ID = entityObjct.Reseller_ID;
            User_ID = entityObjct.User_ID;
            sellingLocation = entityObjct.sellingLocation;
            isSharingLocation = entityObjct.isSharingLocation;
            StartedSharingTime = entityObjct.StartedSharingTime;
            minutesAvailable = entityObjct.minutesAvailable;
        }
    }


    public class ResellerController : ApiController
    {
        database_nanofinEntities db = new database_nanofinEntities();


        [HttpGet]
        public List<DTOresellerLocation> getResellerLocations()
        {
            var list = db.resellers.ToList();
            var toreturn = new List<DTOresellerLocation>();

            foreach(var temp  in list)
            {
                toreturn.Add(new DTOresellerLocation(temp));
            }
            return toreturn;
        }

        [HttpGet]
        public  void setResellerLocation(int userID, string latlng)
        {
            var res = (from c in db.resellers where userID == c.User_ID select c).ToArray()[0];

            res.isSharingLocation = "true";
            res.sellingLocation = latlng;
            db.Entry(res).State = EntityState.Modified;
            db.SaveChanges();

        }

        [HttpGet]
        public void deactivateLocation(int userID)
        {
            var res = (from c in db.resellers where userID == c.User_ID select c).ToArray()[0];

            res.isSharingLocation = "false";
            db.Entry(res).State = EntityState.Modified;
            db.SaveChanges();
        }

    }
}
