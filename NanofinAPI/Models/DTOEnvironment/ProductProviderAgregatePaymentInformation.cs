using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NanofinAPI.Models.DTOEnvironment
{
    public class DTOProductProviderAgregatePaymentInformation
    {
        private database_nanofinEntities db = new database_nanofinEntities();

        public int id { get; set; }
        public string companyName { get; set; }
        public string cellPhoneNumber { get; set; }
        public string email { get; set; }
        public int totalCashedOwed { get; set; } //total amount NanoFin needs to pay product provider
        public DateTime lastPaymentMade { get; set; }
        
        public DTOProductProviderAgregatePaymentInformation(int userID)
        {
            user tmpUser = (from l in db.users where l.User_ID == userID select l).SingleOrDefault();
            productprovider tmpProductProvider = (from l in db.productproviders where l.User_ID == userID select l).SingleOrDefault();


            
        }

    }
}