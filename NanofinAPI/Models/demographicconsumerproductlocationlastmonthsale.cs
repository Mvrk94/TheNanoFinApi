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
    
    public partial class demographicconsumerproductlocationlastmonthsale
    {
        public int ActiveProductItems_ID { get; set; }
        public string datum { get; set; }
        public Nullable<int> transactionLocation { get; set; }
        public long numConsumers { get; set; }
        public string gender { get; set; }
        public string maritalStatus { get; set; }
        public int Product_ID { get; set; }
        public long numMartialStatus { get; set; }
        public string employmentStatus { get; set; }
        public long numEmploymentStatus { get; set; }
        public Nullable<decimal> netIncome { get; set; }
        public Nullable<decimal> NumDependants { get; set; }
        public Nullable<decimal> sales { get; set; }
    }
}
