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
    
    public partial class consumerprofiledata
    {
        public int idConsumer { get; set; }
        public string gender { get; set; }
        public string maritalStatus { get; set; }
        public string employmentStatus { get; set; }
        public Nullable<decimal> claimRate { get; set; }
        public Nullable<int> numUnprocessed { get; set; }
        public Nullable<int> ageRiskValue { get; set; }
        public Nullable<int> genderRiskValue { get; set; }
        public Nullable<int> maritalStatusRiskValue { get; set; }
        public Nullable<int> employmentStatusRiskValue { get; set; }
        public Nullable<decimal> locationClaimRate { get; set; }
        public string topProductCategoriesInterestedIn { get; set; }
        public string monthPurchases { get; set; }
    }
}
