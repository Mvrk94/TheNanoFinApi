﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class database_nanofinEntities : DbContext
    {
        public database_nanofinEntities()
            : base("name=database_nanofinEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<activeproductitem> activeproductitems { get; set; }
        public virtual DbSet<claim> claims { get; set; }
        public virtual DbSet<claimtemplate> claimtemplates { get; set; }
        public virtual DbSet<claimuploaddocument> claimuploaddocuments { get; set; }
        public virtual DbSet<consumer> consumers { get; set; }
        public virtual DbSet<document> documents { get; set; }
        public virtual DbSet<insuranceproduct> insuranceproducts { get; set; }
        public virtual DbSet<insurancetype> insurancetypes { get; set; }
        public virtual DbSet<location> locations { get; set; }
        public virtual DbSet<notificationlog> notificationlogs { get; set; }
        public virtual DbSet<processapplication> processapplications { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<productprovider> productproviders { get; set; }
        public virtual DbSet<productproviderpayment> productproviderpayments { get; set; }
        public virtual DbSet<productredemptionlog> productredemptionlogs { get; set; }
        public virtual DbSet<producttype> producttypes { get; set; }
        public virtual DbSet<reseller> resellers { get; set; }
        public virtual DbSet<systemadmin> systemadmins { get; set; }
        public virtual DbSet<transactiontype> transactiontypes { get; set; }
        public virtual DbSet<unittype> unittypes { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<usertype> usertypes { get; set; }
        public virtual DbSet<validator> validators { get; set; }
        public virtual DbSet<voucher> vouchers { get; set; }
        public virtual DbSet<vouchertransaction> vouchertransactions { get; set; }
        public virtual DbSet<vouchertype> vouchertypes { get; set; }
        public virtual DbSet<activeproductitemswithdetail> activeproductitemswithdetails { get; set; }
        public virtual DbSet<demographicconsumerproductlocationlastmonthsale> demographicconsumerproductlocationlastmonthsales { get; set; }
        public virtual DbSet<demographicconsumerproductlocationmonthlysale> demographicconsumerproductlocationmonthlysales { get; set; }
        public virtual DbSet<insuranceproducttypemonthlysale> insuranceproducttypemonthlysales { get; set; }
        public virtual DbSet<lastmonthinsurancetypesale> lastmonthinsurancetypesales { get; set; }
        public virtual DbSet<lastmonthprovincesale> lastmonthprovincesales { get; set; }
        public virtual DbSet<locationsaleslastmonth> locationsaleslastmonths { get; set; }
        public virtual DbSet<monlthlocationsalessum> monlthlocationsalessums { get; set; }
        public virtual DbSet<monthlylocationsale> monthlylocationsales { get; set; }
        public virtual DbSet<monthlyproductsalesperlocation> monthlyproductsalesperlocations { get; set; }
        public virtual DbSet<monthlyprovincesalesview> monthlyprovincesalesviews { get; set; }
        public virtual DbSet<monthlyprovincialproducttypedistribution> monthlyprovincialproducttypedistributions { get; set; }
        public virtual DbSet<productsalespermonth> productsalespermonths { get; set; }
        public virtual DbSet<productswithpurchas> productswithpurchases { get; set; }
        public virtual DbSet<saleslastmonth> saleslastmonths { get; set; }
        public virtual DbSet<salespermonth> salespermonths { get; set; }
    
        public virtual ObjectResult<monthlyProvinceSales_Result> monthlyProvinceSales(Nullable<int> providerID)
        {
            var providerIDParameter = providerID.HasValue ?
                new ObjectParameter("providerID", providerID) :
                new ObjectParameter("providerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<monthlyProvinceSales_Result>("monthlyProvinceSales", providerIDParameter);
        }
    
        public virtual ObjectResult<ProductLocationSales_Result> ProductLocationSales(Nullable<int> productID)
        {
            var productIDParameter = productID.HasValue ?
                new ObjectParameter("productID", productID) :
                new ObjectParameter("productID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductLocationSales_Result>("ProductLocationSales", productIDParameter);
        }
    
        public virtual ObjectResult<productPredictedSalesPerLocation_Result> productPredictedSalesPerLocation(Nullable<int> productID, Nullable<int> locationID)
        {
            var productIDParameter = productID.HasValue ?
                new ObjectParameter("productID", productID) :
                new ObjectParameter("productID", typeof(int));
    
            var locationIDParameter = locationID.HasValue ?
                new ObjectParameter("locationID", locationID) :
                new ObjectParameter("locationID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<productPredictedSalesPerLocation_Result>("productPredictedSalesPerLocation", productIDParameter, locationIDParameter);
        }
    }
}
