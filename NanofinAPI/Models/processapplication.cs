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
    
    public partial class processapplication
    {
        public int ProcessApplication_ID { get; set; }
        public int Product_ID { get; set; }
        public string OperationType { get; set; }
        public decimal value1 { get; set; }
        public Nullable<System.DateTime> value2 { get; set; }
    
        public virtual product product { get; set; }
    }
}
