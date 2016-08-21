using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NanofinAPI.Models;

namespace TheNanoFinAPI.Models.DTOEnvironment
{
    public class DTOresellerSentVoucher_withUserDetails
    {
        
        public String userFirstName { get; set; }
        public String userName { get; set; }
        public decimal amount { get; set; }
        public Nullable<System.DateTime> dateSent { get; set; }
        
        public DTOresellerSentVoucher_withUserDetails(DTOvouchertransaction voucherTransaction, DTOuser user)
        {
            amount = voucherTransaction.transactionAmount;
            dateSent = voucherTransaction.transactionDate;
            userFirstName = user.userFirstName;
            userName = user.userName;
        }
    }

    
  public class ResellerSendHistory
    {
        public string phoneNo { get; set; }
        public DateTime dateSend { get; set; }
        public decimal amountSent { get; set;}
    }    

}