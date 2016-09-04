using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheNanoFinAPI.Models.DTOEnvironment
{


    public class DTOcatalogInsuranceProduct
    {
        public int productID { get; set; }
        public String productName { get; set; }
        public String productDescription { get; set; }
        public Nullable<decimal> coverAmount { get; set; }
        public Nullable<decimal> unitCost { get; set; }
        public String unitType { get; set; }
        public Nullable<int> minUnits { get; set; }

        public DTOcatalogInsuranceProduct()
        { }

        public DTOcatalogInsuranceProduct(insuranceproduct prod)
        {
            productID = prod.Product_ID;
            productName = prod.product.productName;
            productDescription = prod.product.productDescription;
            coverAmount = prod.ipCoverAmount;
            unitCost = prod.ipUnitCost;
            unitType = prod.unittype.UnitTypeDescription;
            minUnits = prod.ipMinimunNoOfUnits;
        }
    }

    //scratch
    public class DTOconsumerActiveProductItems
    {
        //product table info
        public int productID { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }

        //activeProductItems table info
        public Decimal productValue { get; set; }
        public int UnitDuration { get; set; }
        public Nullable<DateTime> itemStartDate { get; set; }

        public ICollection<insuranceproduct> insuranceprods { get; set; }

        ////insuranceproduct table info
        //public string insuranceType { get; set; }
        //public Decimal coverAmount { get; set; }
        //public Decimal productUnitCost { get; set; }
        //public string unitTypeDesc { get; set; }

        public DTOconsumerActiveProductItems() { }

        public DTOconsumerActiveProductItems(activeproductitem prod)
        {
            productID = prod.Product_ID;
            productName = prod.product.productName;
            productDescription = prod.product.productDescription;

            productValue = prod.productValue;
            UnitDuration = prod.duration;
            itemStartDate = prod.activeProductItemStartDate;

        }


    }

    //without fields: userType and userActivationType
    public class DTOconsumerUserProfileInfo
    {       
        public int consumerID { get; set; }
        public int userID { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string address { get; set; }
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string userContactNumber { get; set; }
        public string userPassword { get; set; }
        public Nullable<bool> isuserActive { get; set; }

        public DTOconsumerUserProfileInfo()
        { }
        public DTOconsumerUserProfileInfo(consumer c)
        {
            consumerID = c.Consumer_ID;
            userID = c.User_ID;
            dateOfBirth = c.consumerDateOfBirth;
            address = c.consumerAddress;
            userFirstName = c.user.userFirstName;
            userLastName = c.user.userLastName;
            userName = c.user.userName;
            userEmail = c.user.userEmail;
            userContactNumber = c.user.userContactNumber;
            userPassword = c.user.userPassword;
            isuserActive = c.user.userIsActive;
        }

    }


    public class DTOactiveProductItemWithDetail
    {
        public int ActiveProductItems_ID { get; set; }
        public int Consumer_ID { get; set; }
        public int Product_ID { get; set; }
        public string activeProductItemPolicyNum { get; set; }
        public Nullable<bool> isActive { get; set; }
        public decimal productValue { get; set; }
        public int duration { get; set; }
        public Nullable<System.DateTime> activeProductItemStartDate { get; set; }

        public int ProductProvider_ID { get; set; }
        public Nullable<int> ProductType_ID { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public string productPolicyDocPath { get; set; }
        public Nullable<bool> isAvailableForPurchase { get; set; }

        //added for more detail
        public Nullable<int> insuranceTypeID { get; set; }
        public Nullable<decimal> ipCoverAmount { get; set; }
        public Nullable<int> unitTypeID { get; set; }
        public string unitTypeDescription { get; set; }
        public Nullable<decimal> unitCost { get; set; }
        public Nullable<DateTime> claimTimeFrame { get; set; }
        public string claimContactNo { get; set; }
        public Nullable<int> claimtemplate_ID { get; set; }

        public DTOactiveProductItemWithDetail()
        { }

        public DTOactiveProductItemWithDetail(activeproductitem entityObjct, insuranceproduct insPoductEntityObj)
        {
            ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
            Consumer_ID = entityObjct.Consumer_ID;
            Product_ID = entityObjct.Product_ID;
            activeProductItemPolicyNum = entityObjct.activeProductItemPolicyNum;
            isActive = entityObjct.isActive;
            productValue = entityObjct.productValue;
            duration = entityObjct.duration;
            activeProductItemStartDate = entityObjct.activeProductItemStartDate;


            ProductProvider_ID = entityObjct.product.ProductProvider_ID;
            ProductType_ID = entityObjct.product.ProductType_ID;
            productName = entityObjct.product.productName;
            productDescription = entityObjct.product.productDescription;
            productPolicyDocPath = entityObjct.product.productPolicyDocPath;
            isAvailableForPurchase = entityObjct.product.isAvailableForPurchase;

            insuranceTypeID = insPoductEntityObj.InsuranceType_ID;
            ipCoverAmount = insPoductEntityObj.ipCoverAmount;
            unitTypeID = insPoductEntityObj.ipUnitType;
            unitTypeDescription = insPoductEntityObj.unittype.UnitTypeDescription;
            unitCost = insPoductEntityObj.ipUnitCost;
            claimTimeFrame = insPoductEntityObj.claimTimeframe;
            claimContactNo = insPoductEntityObj.claimContactNo;
            claimtemplate_ID = insPoductEntityObj.claimtemplate_ID;


        }

    }

}