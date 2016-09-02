using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheNanoFinAPI.Models.DTOEnvironment
{

    public class DTOactiveproductitem
    {
        public int ActiveProductItems_ID { get; set; }
        public int Consumer_ID { get; set; }
        public int Product_ID { get; set; }
        public string activeProductItemPolicyNum { get; set; }
        public Nullable<bool> isActive { get; set; }
        public decimal productValue { get; set; }
        public int duration { get; set; }
        public Nullable<System.DateTime> activeProductItemStartDate { get; set; }
        public Nullable<int> transactionlocation { get; set; }
        public Nullable<System.DateTime> activeProductItemEndDate { get; set; }
        public string PurchaseConfirmationDocPath { get; set; }

        public DTOactiveproductitem() { }

        public DTOactiveproductitem(activeproductitem entityObjct)
        {
            ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
            Consumer_ID = entityObjct.Consumer_ID;
            Product_ID = entityObjct.Product_ID;
            activeProductItemPolicyNum = entityObjct.activeProductItemPolicyNum;
            isActive = entityObjct.isActive;
            productValue = entityObjct.productValue;
            duration = entityObjct.duration;
            activeProductItemStartDate = entityObjct.activeProductItemStartDate;
            transactionlocation = entityObjct.transactionlocation;
            activeProductItemEndDate = entityObjct.activeProductItemEndDate;
            PurchaseConfirmationDocPath = entityObjct.PurchaseConfirmationDocPath;
        }
    }


    public class DTOclaim
    {
        public int Claim_ID { get; set; }
        public Nullable<int> ActiveProductItems_ID { get; set; }
        public string capturedClaimFormDataJson { get; set; }

        public DTOclaim() { }

        public DTOclaim(claim entityObjct)
        {
            Claim_ID = entityObjct.Claim_ID;
            ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
            capturedClaimFormDataJson = entityObjct.capturedClaimFormDataJson;
        }
    }


    public class DTOclaimtemplate
    {
        public int claimtemplate_ID { get; set; }
        public string templateName { get; set; }
        public string formDataRequiredJson { get; set; }

        public DTOclaimtemplate() { }

        public DTOclaimtemplate(claimtemplate entityObjct)
        {
            claimtemplate_ID = entityObjct.claimtemplate_ID;
            templateName = entityObjct.templateName;
            formDataRequiredJson = entityObjct.formDataRequiredJson;
        }
    }


    public class DTOclaimuploaddocument
    {
        public int claimUploadDocument_ID { get; set; }
        public int User_ID { get; set; }
        public int ActiveProductItems_ID { get; set; }
        public int document_ID { get; set; }
        public string claimUploadDocumentPath { get; set; }
        public Nullable<int> Claim_ID { get; set; }

        public DTOclaimuploaddocument() { }

        public DTOclaimuploaddocument(claimuploaddocument entityObjct)
        {
            claimUploadDocument_ID = entityObjct.claimUploadDocument_ID;
            User_ID = entityObjct.User_ID;
            ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
            document_ID = entityObjct.document_ID;
            claimUploadDocumentPath = entityObjct.claimUploadDocumentPath;
            Claim_ID = entityObjct.Claim_ID;
        }
    }


    public class DTOconsumer
    {
        public int Consumer_ID { get; set; }
        public int User_ID { get; set; }
        public System.DateTime consumerDateOfBirth { get; set; }
        public string consumerAddress { get; set; }
        public string gender { get; set; }
        public string maritalStatus { get; set; }
        public string topProductCategoriesInterestedIn { get; set; }
        public string homeOwnerType { get; set; }
        public string employmentStatus { get; set; }
        public Nullable<decimal> grossMonthlyIncome { get; set; }
        public Nullable<decimal> nettMonthlyIncome { get; set; }
        public Nullable<decimal> totalMonthlyExpenses { get; set; }
        public Nullable<int> Location_ID { get; set; }
        public Nullable<int> numDependant { get; set; }

        public DTOconsumer() { }

        public DTOconsumer(consumer entityObjct)
        {
            Consumer_ID = entityObjct.Consumer_ID;
            User_ID = entityObjct.User_ID;
            consumerDateOfBirth = entityObjct.consumerDateOfBirth;
            consumerAddress = entityObjct.consumerAddress;
            gender = entityObjct.gender;
            maritalStatus = entityObjct.maritalStatus;
            topProductCategoriesInterestedIn = entityObjct.topProductCategoriesInterestedIn;
            homeOwnerType = entityObjct.homeOwnerType;
            employmentStatus = entityObjct.employmentStatus;
            grossMonthlyIncome = entityObjct.grossMonthlyIncome;
            nettMonthlyIncome = entityObjct.nettMonthlyIncome;
            totalMonthlyExpenses = entityObjct.totalMonthlyExpenses;
            Location_ID = entityObjct.Location_ID;
            numDependant = entityObjct.numDependant;
        }
    }


    public class DTOdocument
    {
        public int document_ID { get; set; }
        public int Product_ID { get; set; }
        public string documentName { get; set; }
        public string documentDescription { get; set; }
        public string docPreferredFormat { get; set; }
        public string docPreparationRequired { get; set; }

        public DTOdocument() { }

        public DTOdocument(document entityObjct)
        {
            document_ID = entityObjct.document_ID;
            Product_ID = entityObjct.Product_ID;
            documentName = entityObjct.documentName;
            documentDescription = entityObjct.documentDescription;
            docPreferredFormat = entityObjct.docPreferredFormat;
            docPreparationRequired = entityObjct.docPreparationRequired;
        }
    }


    public class DTOinsuranceproduct
    {
        public int InsuranceProduct_ID { get; set; }
        public int ProductProvider_ID { get; set; }
        public int InsuranceType_ID { get; set; }
        public int Product_ID { get; set; }
        public Nullable<decimal> ipCoverAmount { get; set; }
        public Nullable<decimal> ipUnitCost { get; set; }
        public Nullable<int> ipUnitType { get; set; }
        public Nullable<int> ipMinimunNoOfUnits { get; set; }
        public string ipClaimInfoPath { get; set; }
        public Nullable<System.DateTime> claimTimeframe { get; set; }
        public string policyNumberApiLink { get; set; }
        public string ApiKey { get; set; }
        public string claimContactNo { get; set; }
        public string claimFormPath { get; set; }
        public Nullable<int> claimtemplate_ID { get; set; }

        public DTOinsuranceproduct() { }

        public DTOinsuranceproduct(insuranceproduct entityObjct)
        {
            InsuranceProduct_ID = entityObjct.InsuranceProduct_ID;
            ProductProvider_ID = entityObjct.ProductProvider_ID;
            InsuranceType_ID = entityObjct.InsuranceType_ID;
            Product_ID = entityObjct.Product_ID;
            ipCoverAmount = entityObjct.ipCoverAmount;
            ipUnitCost = entityObjct.ipUnitCost;
            ipUnitType = entityObjct.ipUnitType;
            ipMinimunNoOfUnits = entityObjct.ipMinimunNoOfUnits;
            ipClaimInfoPath = entityObjct.ipClaimInfoPath;
            claimTimeframe = entityObjct.claimTimeframe;
            policyNumberApiLink = entityObjct.policyNumberApiLink;
            ApiKey = entityObjct.ApiKey;
            claimContactNo = entityObjct.claimContactNo;
            claimFormPath = entityObjct.claimFormPath;
            claimtemplate_ID = entityObjct.claimtemplate_ID;
        }
    }


    public class DTOinsurancetype
    {
        public int InsuranceType_ID { get; set; }
        public string insuranctTypeDescription { get; set; }

        public DTOinsurancetype() { }

        public DTOinsurancetype(insurancetype entityObjct)
        {
            InsuranceType_ID = entityObjct.InsuranceType_ID;
            insuranctTypeDescription = entityObjct.insuranctTypeDescription;
        }
    }


    public class DTOlocation
    {
        public int Location_ID { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string LatLng { get; set; }
        public string PostalCode { get; set; }
        public Nullable<int> GDP { get; set; }
        public Nullable<decimal> UnemploymentRate { get; set; }

        public DTOlocation() { }

        public DTOlocation(location entityObjct)
        {
            Location_ID = entityObjct.Location_ID;
            Province = entityObjct.Province;
            City = entityObjct.City;
            LatLng = entityObjct.LatLng;
            PostalCode = entityObjct.PostalCode;
            GDP = entityObjct.GDP;
            UnemploymentRate = entityObjct.UnemploymentRate;
        }
    }


    public class DTOlocationsaleslastmonth
    {
        public Nullable<System.DateTime> purchaseDate { get; set; }
        public string datum { get; set; }
        public int ProductProvider_ID { get; set; }
        public int Product_ID { get; set; }
        public int Location_ID { get; set; }
        public string Province { get; set; }
        public string city { get; set; }
        public string LatLng { get; set; }
        public Nullable<decimal> sales { get; set; }

        public DTOlocationsaleslastmonth() { }

        public DTOlocationsaleslastmonth(locationsaleslastmonth entityObjct)
        {
            purchaseDate = entityObjct.purchaseDate;
            datum = entityObjct.datum;
            ProductProvider_ID = entityObjct.ProductProvider_ID;
            Product_ID = entityObjct.Product_ID;
            Location_ID = entityObjct.Location_ID;
            Province = entityObjct.Province;
            city = entityObjct.city;
            LatLng = entityObjct.LatLng;
            sales = entityObjct.sales;
        }
    }


    public class DTOmonthlylocationsale
    {
        public Nullable<System.DateTime> datum { get; set; }
        public int Product_ID { get; set; }
        public Nullable<int> transactionLocation { get; set; }
        public Nullable<decimal> sales { get; set; }

        public DTOmonthlylocationsale() { }

        public DTOmonthlylocationsale(monthlylocationsale entityObjct)
        {
            datum = entityObjct.datum;
            Product_ID = entityObjct.Product_ID;
            transactionLocation = entityObjct.transactionLocation;
            sales = entityObjct.sales;
        }
    }


    public class DTOmonthlylocationsalessum
    {
        public int ActiveProductItems_ID { get; set; }
        public string datum { get; set; }
        public Nullable<int> transactionLocation { get; set; }
        public Nullable<decimal> sales { get; set; }

        public DTOmonthlylocationsalessum() { }

        public DTOmonthlylocationsalessum(monthlylocationsalessum entityObjct)
        {
            ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
            datum = entityObjct.datum;
            transactionLocation = entityObjct.transactionLocation;
            sales = entityObjct.sales;
        }
    }


    public class DTOmonthlyproductsalesperlocation
    {
        public Nullable<System.DateTime> purchaseDate { get; set; }
        public string datum { get; set; }
        public int ProductProvider_ID { get; set; }
        public int Product_ID { get; set; }
        public int Location_ID { get; set; }
        public string Province { get; set; }
        public string city { get; set; }
        public string LatLng { get; set; }
        public Nullable<decimal> sales { get; set; }

        public DTOmonthlyproductsalesperlocation() { }

        public DTOmonthlyproductsalesperlocation(monthlyproductsalesperlocation entityObjct)
        {
            purchaseDate = entityObjct.purchaseDate;
            datum = entityObjct.datum;
            ProductProvider_ID = entityObjct.ProductProvider_ID;
            Product_ID = entityObjct.Product_ID;
            Location_ID = entityObjct.Location_ID;
            Province = entityObjct.Province;
            city = entityObjct.city;
            LatLng = entityObjct.LatLng;
            sales = entityObjct.sales;
        }
    }


    public class DTOmonthlyprovincesalesview
    {
        public string datum { get; set; }
        public int ProductProvider_ID { get; set; }
        public string Province { get; set; }
        public string LatLng { get; set; }
        public Nullable<decimal> sales { get; set; }

        public DTOmonthlyprovincesalesview() { }

        public DTOmonthlyprovincesalesview(monthlyprovincesalesview entityObjct)
        {
            datum = entityObjct.datum;
            ProductProvider_ID = entityObjct.ProductProvider_ID;
            Province = entityObjct.Province;
            LatLng = entityObjct.LatLng;
            sales = entityObjct.sales;
        }
    }

    public class DTOlastmonthprovincesale
    {
        public string datum { get; set; }
        public int ProductProvider_ID { get; set; }
        public string Province { get; set; }
        public string LatLng { get; set; }
        public Nullable<decimal> sales { get; set; }

        public DTOlastmonthprovincesale() { }

        public DTOlastmonthprovincesale(lastmonthprovincesale entityObjct)
        {
            datum = entityObjct.datum;
            ProductProvider_ID = entityObjct.ProductProvider_ID;
            Province = entityObjct.Province;
            LatLng = entityObjct.LatLng;
            sales = entityObjct.sales;
        }
    }


    public class DTOnotificationlog
    {
        public int NotificationLog_ID { get; set; }
        public string notificationType { get; set; }
        public int notificationReceiver { get; set; }
        public System.DateTime notificationDateSent { get; set; }

        public DTOnotificationlog() { }

        public DTOnotificationlog(notificationlog entityObjct)
        {
            NotificationLog_ID = entityObjct.NotificationLog_ID;
            notificationType = entityObjct.notificationType;
            notificationReceiver = entityObjct.notificationReceiver;
            notificationDateSent = entityObjct.notificationDateSent;
        }
    }


    public class DTOprocessapplication
    {
        public int ProcessApplication_ID { get; set; }
        public int Product_ID { get; set; }
        public string OperationType { get; set; }
        public decimal value1 { get; set; }
        public Nullable<System.DateTime> value2 { get; set; }

        public DTOprocessapplication() { }

        public DTOprocessapplication(processapplication entityObjct)
        {
            ProcessApplication_ID = entityObjct.ProcessApplication_ID;
            Product_ID = entityObjct.Product_ID;
            OperationType = entityObjct.OperationType;
            value1 = entityObjct.value1;
            value2 = entityObjct.value2;
        }
    }


    public class DTOproduct
    {
        public int Product_ID { get; set; }
        public int ProductProvider_ID { get; set; }
        public Nullable<int> ProductType_ID { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public string detailedDescription { get; set; }
        public string productPolicyDocPath { get; set; }
        public Nullable<bool> isAvailableForPurchase { get; set; }
        public Nullable<decimal> salesTargetAmount { get; set; }
        public Nullable<decimal> ratingAverage { get; set; }
        public Nullable<int> numTimesRated { get; set; }

        public DTOproduct() { }

        public DTOproduct(product entityObjct)
        {
            Product_ID = entityObjct.Product_ID;
            ProductProvider_ID = entityObjct.ProductProvider_ID;
            ProductType_ID = entityObjct.ProductType_ID;
            productName = entityObjct.productName;
            productDescription = entityObjct.productDescription;
            detailedDescription = entityObjct.detailedDescription;
            productPolicyDocPath = entityObjct.productPolicyDocPath;
            isAvailableForPurchase = entityObjct.isAvailableForPurchase;
            salesTargetAmount = entityObjct.salesTargetAmount;
            ratingAverage = entityObjct.ratingAverage;
            numTimesRated = entityObjct.numTimesRated;
        }
    }


    public class DTOProductLocation1_Result
    {

        public DTOProductLocation1_Result() { }

        public DTOProductLocation1_Result(ProductLocation1_Result entityObjct)
        {
        }
    }


    public class DTOProductLocationSales_Result
    {

        public DTOProductLocationSales_Result() { }

        public DTOProductLocationSales_Result(ProductLocationSales_Result entityObjct)
        {
        }
    }


    public class DTOProductLocation_Result
    {

        public DTOProductLocation_Result() { }

        public DTOProductLocation_Result(ProductLocation_Result entityObjct)
        {
        }
    }


    public class DTOproductPredictedSalesPerLocation_Result
    {

        public DTOproductPredictedSalesPerLocation_Result() { }

        public DTOproductPredictedSalesPerLocation_Result(productPredictedSalesPerLocation_Result entityObjct)
        {
        }
    }


    public class DTOproductprovider
    {
        public int ProductProvider_ID { get; set; }
        public int User_ID { get; set; }
        public string ppCompanyName { get; set; }
        public string ppVATnumber { get; set; }
        public string ppFaxNumber { get; set; }
        public string ppAddress { get; set; }

        public DTOproductprovider() { }

        public DTOproductprovider(productprovider entityObjct)
        {
            ProductProvider_ID = entityObjct.ProductProvider_ID;
            User_ID = entityObjct.User_ID;
            ppCompanyName = entityObjct.ppCompanyName;
            ppVATnumber = entityObjct.ppVATnumber;
            ppFaxNumber = entityObjct.ppFaxNumber;
            ppAddress = entityObjct.ppAddress;
        }
    }


    public class DTOproductproviderpayment
    {
        public int Productproviderpayment_ID { get; set; }
        public Nullable<int> ProductProvider_ID { get; set; }
        public Nullable<int> ActiveProductItems_ID { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> AmountToPay { get; set; }
        public Nullable<bool> hasBeenPayed { get; set; }

        public DTOproductproviderpayment() { }

        public DTOproductproviderpayment(productproviderpayment entityObjct)
        {
            Productproviderpayment_ID = entityObjct.Productproviderpayment_ID;
            ProductProvider_ID = entityObjct.ProductProvider_ID;
            ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
            Description = entityObjct.Description;
            AmountToPay = entityObjct.AmountToPay;
            hasBeenPayed = entityObjct.hasBeenPayed;
        }
    }


    public class DTOproductredemptionlog
    {
        public int Voucher_ID { get; set; }
        public int ActiveProductItems_ID { get; set; }
        public decimal transactionAmount { get; set; }

        public DTOproductredemptionlog() { }

        public DTOproductredemptionlog(productredemptionlog entityObjct)
        {
            Voucher_ID = entityObjct.Voucher_ID;
            ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
            transactionAmount = entityObjct.transactionAmount;
        }
    }


    public class DTOproductsalespermonth
    {
        public Nullable<System.DateTime> activeProductItemStartDate { get; set; }
        public string datum { get; set; }
        public int Product_ID { get; set; }
        public string productName { get; set; }
        public Nullable<decimal> sales { get; set; }

        public DTOproductsalespermonth() { }

        public DTOproductsalespermonth(productsalespermonth entityObjct)
        {
            activeProductItemStartDate = entityObjct.activeProductItemStartDate;
            datum = entityObjct.datum;
            Product_ID = entityObjct.Product_ID;
            productName = entityObjct.productName;
            sales = entityObjct.sales;
        }
    }


    public class DTOproducttype
    {
        public int ProductType_ID { get; set; }
        public string ProductTypeName { get; set; }

        public DTOproducttype() { }

        public DTOproducttype(producttype entityObjct)
        {
            ProductType_ID = entityObjct.ProductType_ID;
            ProductTypeName = entityObjct.ProductTypeName;
        }
    }


    public class DTOreseller
    {
        public int Reseller_ID { get; set; }
        public int User_ID { get; set; }
        public Nullable<bool> resellerIsValidated { get; set; }
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

        public DTOreseller() { }

        public DTOreseller(reseller entityObjct)
        {
            Reseller_ID = entityObjct.Reseller_ID;
            User_ID = entityObjct.User_ID;
            resellerIsValidated = entityObjct.resellerIsValidated;
            resellerBankBranchName = entityObjct.resellerBankBranchName;
            resellerBankAccountNumber = entityObjct.resellerBankAccountNumber;
            resellerBankName = entityObjct.resellerBankName;
            resellerBankBranchCode = entityObjct.resellerBankBranchCode;
            resellerDateOfBirth = entityObjct.resellerDateOfBirth;
            street = entityObjct.street;
            city = entityObjct.city;
            postalCode = entityObjct.postalCode;
            province = entityObjct.province;
            country = entityObjct.country;
            sellingLocation = entityObjct.sellingLocation;
            isSharingLocation = entityObjct.isSharingLocation;
            StartedSharingTime = entityObjct.StartedSharingTime;
            minutesAvailable = entityObjct.minutesAvailable;
            LocationID = entityObjct.LocationID;
        }
    }


    public class DTOsaleslastmonth
    {
        public Nullable<System.DateTime> activeProductItemStartDate { get; set; }
        public string datum { get; set; }
        public int Product_ID { get; set; }
        public string productName { get; set; }
        public Nullable<decimal> sales { get; set; }

        public DTOsaleslastmonth() { }

        public DTOsaleslastmonth(saleslastmonth entityObjct)
        {
            activeProductItemStartDate = entityObjct.activeProductItemStartDate;
            datum = entityObjct.datum;
            Product_ID = entityObjct.Product_ID;
            productName = entityObjct.productName;
            sales = entityObjct.sales;
        }
    }


    public class DTOsalespermonth
    {
        public int activeProductItems_ID { get; set; }
        public string datum { get; set; }
        public Nullable<decimal> sales { get; set; }

        public DTOsalespermonth() { }

        public DTOsalespermonth(salespermonth entityObjct)
        {
            activeProductItems_ID = entityObjct.activeProductItems_ID;
            datum = entityObjct.datum;
            sales = entityObjct.sales;
        }
    }


    public class DTOsystemadmin
    {
        public int SystemAdmin_ID { get; set; }
        public int User_ID { get; set; }
        public string systemAdminOTP { get; set; }
        public Nullable<bool> systemAdminHasTwoPartAuth { get; set; }

        public DTOsystemadmin() { }

        public DTOsystemadmin(systemadmin entityObjct)
        {
            SystemAdmin_ID = entityObjct.SystemAdmin_ID;
            User_ID = entityObjct.User_ID;
            systemAdminOTP = entityObjct.systemAdminOTP;
            systemAdminHasTwoPartAuth = entityObjct.systemAdminHasTwoPartAuth;
        }
    }


    public class DTOtransactiontype
    {
        public int TransactionType_ID { get; set; }
        public string transactionTypeDescription { get; set; }

        public DTOtransactiontype() { }

        public DTOtransactiontype(transactiontype entityObjct)
        {
            TransactionType_ID = entityObjct.TransactionType_ID;
            transactionTypeDescription = entityObjct.transactionTypeDescription;
        }
    }


    public class DTOunittype
    {
        public int UnitType_ID { get; set; }
        public string UnitTypeDescription { get; set; }

        public DTOunittype() { }

        public DTOunittype(unittype entityObjct)
        {
            UnitType_ID = entityObjct.UnitType_ID;
            UnitTypeDescription = entityObjct.UnitTypeDescription;
        }
    }


    public class DTOuser
    {
        public int User_ID { get; set; }
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string userContactNumber { get; set; }
        public string userPassword { get; set; }
        public Nullable<bool> userIsActive { get; set; }
        public Nullable<int> userType { get; set; }
        public string userActivationType { get; set; }
        public string IDnumber { get; set; }
        public string IdDocumentPath { get; set; }
        public Nullable<System.DateTime> IdDocumentLastUpdated { get; set; }
        public Nullable<System.DateTime> timeStap { get; set; }
        public string resetPasswordKey { get; set; }
        public string blockchainAddress { get; set; }

        public DTOuser() { }

        public DTOuser(user entityObjct)
        {
            User_ID = entityObjct.User_ID;
            userFirstName = entityObjct.userFirstName;
            userLastName = entityObjct.userLastName;
            userName = entityObjct.userName;
            userEmail = entityObjct.userEmail;
            userContactNumber = entityObjct.userContactNumber;
            userPassword = entityObjct.userPassword;
            userIsActive = entityObjct.userIsActive;
            userType = entityObjct.userType;
            userActivationType = entityObjct.userActivationType;
            IDnumber = entityObjct.IDnumber;
            IdDocumentPath = entityObjct.IdDocumentPath;
            IdDocumentLastUpdated = entityObjct.IdDocumentLastUpdated;
            timeStap = entityObjct.timeStap;
            resetPasswordKey = entityObjct.resetPasswordKey;
            blockchainAddress = entityObjct.blockchainAddress;
        }
    }


    public class DTOusertype
    {
        public int UserType_ID { get; set; }
        public string UserTypeDescription { get; set; }

        public DTOusertype() { }

        public DTOusertype(usertype entityObjct)
        {
            UserType_ID = entityObjct.UserType_ID;
            UserTypeDescription = entityObjct.UserTypeDescription;
        }
    }


    public class DTOvalidator
    {
        public int Validator_ID { get; set; }
        public int User_ID { get; set; }
        public string validatiorCompany { get; set; }
        public string validatorLicenseNumber { get; set; }
        public string validatorLicenseProvider { get; set; }
        public Nullable<System.DateTime> validatorValidUntil { get; set; }
        public string validatorVATNumber { get; set; }
        public string validatorAddress { get; set; }
        public string validatorBankName { get; set; }
        public string validatorBankAccNumber { get; set; }
        public string validatorBankBranchName { get; set; }
        public string validatorBankBranchCode { get; set; }

        public DTOvalidator() { }

        public DTOvalidator(validator entityObjct)
        {
            Validator_ID = entityObjct.Validator_ID;
            User_ID = entityObjct.User_ID;
            validatiorCompany = entityObjct.validatiorCompany;
            validatorLicenseNumber = entityObjct.validatorLicenseNumber;
            validatorLicenseProvider = entityObjct.validatorLicenseProvider;
            validatorValidUntil = entityObjct.validatorValidUntil;
            validatorVATNumber = entityObjct.validatorVATNumber;
            validatorAddress = entityObjct.validatorAddress;
            validatorBankName = entityObjct.validatorBankName;
            validatorBankAccNumber = entityObjct.validatorBankAccNumber;
            validatorBankBranchName = entityObjct.validatorBankBranchName;
            validatorBankBranchCode = entityObjct.validatorBankBranchCode;
        }
    }


    public class DTOvoucher
    {
        public int Voucher_ID { get; set; }
        public int VoucherType_ID { get; set; }
        public int User_ID { get; set; }
        public decimal voucherValue { get; set; }
        public Nullable<System.DateTime> voucherCreationDate { get; set; }
        public Nullable<int> OTP { get; set; }
        public Nullable<System.DateTime> OTPtimeStap { get; set; }
        public string QRdata { get; set; }
        public Nullable<System.DateTime> QRtimeStap { get; set; }

        public DTOvoucher() { }

        public DTOvoucher(voucher entityObjct)
        {
            Voucher_ID = entityObjct.Voucher_ID;
            VoucherType_ID = entityObjct.VoucherType_ID;
            User_ID = entityObjct.User_ID;
            voucherValue = entityObjct.voucherValue;
            voucherCreationDate = entityObjct.voucherCreationDate;
            OTP = entityObjct.OTP;
            OTPtimeStap = entityObjct.OTPtimeStap;
            QRdata = entityObjct.QRdata;
            QRtimeStap = entityObjct.QRtimeStap;
        }
    }


    public class DTOvouchertransaction
    {
        public int Voucher_ID { get; set; }
        public int VoucherSentTo { get; set; }
        public int Sender_ID { get; set; }
        public int Receiver_ID { get; set; }
        public int TransactionType_ID { get; set; }
        public decimal transactionAmount { get; set; }
        public string transactionDescription { get; set; }
        public System.DateTime transactionDate { get; set; }

        public DTOvouchertransaction() { }

        public DTOvouchertransaction(vouchertransaction entityObjct)
        {
            Voucher_ID = entityObjct.Voucher_ID;
            VoucherSentTo = entityObjct.VoucherSentTo;
            Sender_ID = entityObjct.Sender_ID;
            Receiver_ID = entityObjct.Receiver_ID;
            TransactionType_ID = entityObjct.TransactionType_ID;
            transactionAmount = entityObjct.transactionAmount;
            transactionDescription = entityObjct.transactionDescription;
            transactionDate = entityObjct.transactionDate;
        }
    }


    public class DTOvouchertype
    {
        public int VoucherType_ID { get; set; }
        public string voucherTypeDescription { get; set; }

        public DTOvouchertype() { }

        public DTOvouchertype(vouchertype entityObjct)
        {
            VoucherType_ID = entityObjct.VoucherType_ID;
            voucherTypeDescription = entityObjct.voucherTypeDescription;
        }
    }


}