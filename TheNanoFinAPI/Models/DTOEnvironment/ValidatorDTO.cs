using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheNanoFinAPI.Models.DTOEnvironment
{
    public class unValidatedUser
    {
        public int userID { get;set;}
        public String name { get;set;}
        public String IDNumber { get;set;}
        public String  bankName { get; set; }
        public String brankNo { get;set;}
        public String accountNo { get;set;}
        public String address { get;set;}
        public String contactInfo { get;set;} 

        public unValidatedUser(reseller res)
        {
            user temp = res.user;
            userID = res.User_ID;
            name = temp.userFirstName + " " + temp.userLastName;
            IDNumber = temp.IDnumber;
            bankName = res.resellerBankName;
            brankNo = res.resellerBankBranchCode;
            accountNo = res.resellerBankAccountNumber;
            address = res.street.Split(':')[2];
            contactInfo = temp.userContactNumber;
        }
    }
}