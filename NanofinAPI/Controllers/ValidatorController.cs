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
    public class ValidatorController : ApiController
    {
        private nanofinEntities db = new nanofinEntities();

        [HttpGet] 
        public List<unValidatedUser> getUnValidatedUsers()
        {
            List<unValidatedUser> toreturn = new List<unValidatedUser>();
            List<reseller> unvalidatedUsers = (from c in db.resellers where c.user.userActivationType != "Verified"  && c.user.userType == 21 select c).ToList();
            
            foreach ( reseller  res  in unvalidatedUsers)
            {
                toreturn.Add(new unValidatedUser(res));
            }
            return toreturn;
        }

    }
}
