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
    
    public partial class claimuploaddocument
    {
        public int claimUploadDocument_ID { get; set; }
        public int User_ID { get; set; }
        public int ActiveProductItems_ID { get; set; }
        public int document_ID { get; set; }
        public string claimUploadDocumentPath { get; set; }
    
        public virtual activeproductitem activeproductitem { get; set; }
        public virtual user user { get; set; }
        public virtual document document { get; set; }
    }
}
