//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NanofinAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class reseller
    {
        public int Reseller_ID { get; set; }
        public int User_ID { get; set; }
        public Nullable<bool> resellerIsValidated { get; set; }
        public string cardNumber { get; set; }
        public string cardExpirationMonth_Year { get; set; }
        public string cardCVV { get; set; }
        public string nameOnCard { get; set; }
        public string resellerBankBranchName { get; set; }
        public string resellerBankAccountNumber { get; set; }
        public string resellerBankName { get; set; }
        public string resellerBankBranchCode { get; set; }
        public Nullable<System.DateTime> resellerDateOfBirth { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string sellingLocation { get; set; }
        public string isSharingLocation { get; set; }
        public Nullable<System.DateTime> StartedSharingTime { get; set; }
        public Nullable<int> minutesAvailable { get; set; }
        public Nullable<int> LocationID { get; set; }
        public string location { get; set; }
        public Nullable<bool> isLocationAvailable { get; set; }
        public string cardNumber { get; set; }
        public string cardExpirationMonth_Year { get; set; }
        public string cardCVV { get; set; }
        public string nameOnCard { get; set; }
    
        public virtual location location1 { get; set; }
        public virtual user user { get; set; }
    }
}
