using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NanofinAPI.Models.DTOEnvironment
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
        public int User_ID { get; set; }
        public int Consumer_ID { get; set; }
        public int Product_ID { get; set; }
        public string activeProductItemPolicyNum { get; set; }
        public Nullable<bool> isActive { get; set; }
        public decimal productValue { get; set; }
        public int duration { get; set; }
        public Nullable<System.DateTime> activeProductItemStartDate { get; set; }
        public Nullable<System.DateTime> activeProductItemEndDate { get; set;}

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

        public DTOactiveProductItemWithDetail(activeproductitemswithdetail entityObjct)
        {
            ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
            User_ID = entityObjct.User_ID;
            Consumer_ID = entityObjct.Consumer_ID;
            Product_ID = entityObjct.Product_ID;
            activeProductItemPolicyNum = entityObjct.activeProductItemPolicyNum;
            isActive = entityObjct.isActive;
            productValue = entityObjct.productValue;
            duration = entityObjct.duration;
            activeProductItemStartDate = entityObjct.activeProductItemStartDate;
            activeProductItemEndDate = entityObjct.activeProductItemEndDate;

            ProductProvider_ID = entityObjct.ProductProvider_ID;
            ProductType_ID = entityObjct.ProductType_ID;
            productName = entityObjct.productName;
            productDescription = entityObjct.productDescription;
            productPolicyDocPath = entityObjct.productPolicyDocPath;
            isAvailableForPurchase = entityObjct.isAvailableForPurchase;

            insuranceTypeID = entityObjct.InsuranceType_ID;
            ipCoverAmount = entityObjct.ipCoverAmount;
            unitTypeID = entityObjct.ipUnitType;
            unitTypeDescription = entityObjct.UnitTypeDescription;
            unitCost = entityObjct.ipUnitCost;
            claimTimeFrame = entityObjct.claimTimeframe;
            claimContactNo = entityObjct.claimContactNo;
            claimtemplate_ID = entityObjct.claimtemplate_ID;


        }

       

    }

    public class DTOclaimdetails
    {
        public int Claim_ID { get; set; }
        public Nullable<int> ActiveProductItems_ID { get; set; }
        public string capturedClaimFormDataJson { get; set; }
        public Nullable<System.DateTime> claimDate { get; set; }
        public string claimStatus { get; set; }
        public string claimPaymentFinalised { get; set; }
        public Nullable<int> Consumer_ID { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
       

        public DTOclaimdetails() { }

        public DTOclaimdetails(claim entityObjct)
        {
            Claim_ID = entityObjct.Claim_ID;
            ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
            capturedClaimFormDataJson = entityObjct.capturedClaimFormDataJson;
            claimDate = entityObjct.claimDate;
            claimStatus = entityObjct.claimStatus;
            claimPaymentFinalised = entityObjct.claimPaymentFinalised;
            Consumer_ID = entityObjct.Consumer_ID;
            productName = entityObjct.activeproductitem.product.productName;
            productDescription = entityObjct.activeproductitem.product.productDescription;
           
        }
    }

}