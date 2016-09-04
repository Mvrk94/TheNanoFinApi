using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NanofinAPI.Models.DTOEnvironment
{
    public class InsuranceProductMobileDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal cost { get; set; }
        public int insuranceTypeID { get; set; }
        public string typeName { get; set; }
        public String unitType { get; set; }

        public InsuranceProductMobileDTO(insuranceproduct p )
        {
            product prod = p.product;
            id = p.Product_ID;
            name = prod.productName;
            description = prod.productDescription;
            cost = (Decimal)p.ipUnitCost;
            insuranceTypeID = p.InsuranceType_ID;
            typeName = p.insurancetype.insuranctTypeDescription;
            unitType = p.unittype.UnitTypeDescription;
        }
    }
}