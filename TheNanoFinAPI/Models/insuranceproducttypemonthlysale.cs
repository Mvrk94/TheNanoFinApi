//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheNanoFinAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class insuranceproducttypemonthlysale
    {
        public int ActiveProductItems_ID { get; set; }
        public Nullable<System.DateTime> activeProductItemStartDate { get; set; }
        public string datum { get; set; }
        public int InsuranceType_ID { get; set; }
        public Nullable<decimal> monthSales { get; set; }
    }
}
