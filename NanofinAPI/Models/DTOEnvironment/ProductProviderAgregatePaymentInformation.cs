using NanofinAPI.Controllers;
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
        public Nullable<System.DateTime> lastPaymentMade { get; set; }
        
        public DTOProductProviderAgregatePaymentInformation(int userID)
        {
            //user tmpUser = (from l in db.users where l.User_ID == userID select l).SingleOrDefault();
            user tmpUser = db.users.Find(userID);
            productprovider tmpProductProvider = (from l in db.productproviders where l.User_ID == userID select l).SingleOrDefault();
            productproviderpayment Latestpayment = (from a in db.productproviderpayments where a.ProductProvider_ID == tmpProductProvider.ProductProvider_ID orderby a.DatePayed descending select a).First();
            ProductProviderController ctrl = new ProductProviderController();

            companyName = tmpProductProvider.ppCompanyName;
            cellPhoneNumber = tmpUser.userContactNumber;
            email = tmpUser.userEmail;

            totalCashedOwed = (int) ctrl.getTotalOwedToPP(tmpProductProvider.ProductProvider_ID).AmountToPay;

            lastPaymentMade = Latestpayment.DatePayed;

        }

    }
}