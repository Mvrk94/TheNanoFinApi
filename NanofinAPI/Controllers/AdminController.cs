using NanofinAPI.Models;
using NanofinAPI.Models.DTOEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace NanofinAPI.Controllers
{
    public class AdminController : ApiController
    {

        database_nanofinEntities db = new database_nanofinEntities();

        //returns a list of all consumers who have not yet been accepted. Admin should either verify a user or reject a user
        [HttpGet]
        [ResponseType(typeof(List<DTOuser>))]
        public IHttpActionResult getUnvalidatedConsumers()
        {
            List<user> unvalidatedUsers = (from l in db.users where l.userActivationType == null || l.userActivationType == "" || l.userActivationType == "Pending" || l.userActivationType == String.Empty && l.userType == 11 select l).ToList();
            if(unvalidatedUsers.Count == 0)
            {
                return BadRequest("No unvalidated users");
            }else
            {
                List<DTOuser> unvalidatedDTOUsers = new List<DTOuser>();
                foreach (user u in unvalidatedUsers)
                {
                    unvalidatedDTOUsers.Add(new DTOuser(u));
                }
                return Ok(unvalidatedDTOUsers);
            }
        }

        //Admin reject consumer registration. 
        [HttpPut]
        public async Task<IHttpActionResult> rejectConsumer(int userID)
        {
            try
            {
                user toReject = db.users.Find(userID);
                toReject.userActivationType = "Rejected";
                await db.SaveChangesAsync();

                NotificationController notification = new NotificationController();
                string userNum = notification.getPhoneNumFromUserID(userID);
                notification.SendSMS(userNum, "Hello from NanoFin. We are sorry to inform you that your NanoFin application could not be processed. Please contact us at: smartquickbankless@gmail.com or visit us at: JoziHub - 44 Stanley Ave, Milpark, Johannesburg");
            }
            catch (NullReferenceException)
            {
                return BadRequest("User to reject not found.");
            }
            return Ok();
        }


        //Admin reject consumer registration. 
        [HttpPut]
        public async Task<IHttpActionResult> acceptConsumer(int userID)
        {
            try
            {
                user toAccept = db.users.Find(userID);
                toAccept.userActivationType = "Verified";
                await db.SaveChangesAsync();

                NotificationController notification = new NotificationController();
                string userNum = notification.getPhoneNumFromUserID(userID);
                notification.SendSMS(userNum, "Welcome to NanoFIn! Your application was accepted by our staff. Feel free to use NanoFin, if you are having trouble please email us at: smartquickbankless@gmail.com or contact us at 0110000000 - a trained professional will assist you.");
               
            }
            catch (NullReferenceException)
            {
                return BadRequest("User to reject not found.");
            }
            return Ok();
        }



    }
}