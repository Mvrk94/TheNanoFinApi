
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NanofinAPI.Models.DTOEnvironment
{
   public static class EntityMapper
    {
        public static activeproductitem updateEntity(activeproductitem entityObjct, DTOactiveproductitem dto)
        {
            if (entityObjct == null) entityObjct = new activeproductitem();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.Consumer_ID = dto.Consumer_ID;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.activeProductItemPolicyNum = dto.activeProductItemPolicyNum;
            entityObjct.isActive = dto.isActive;
            entityObjct.Accepted = dto.Accepted;
            entityObjct.productValue = dto.productValue;
            entityObjct.duration = dto.duration;
            entityObjct.activeProductItemStartDate = dto.activeProductItemStartDate;
            entityObjct.transactionlocation = dto.transactionlocation;
            entityObjct.activeProductItemEndDate = dto.activeProductItemEndDate;
            entityObjct.PurchaseConfirmationDocPath = dto.PurchaseConfirmationDocPath;

            return entityObjct;
        }


        public static activeproductitemswithdetail updateEntity(activeproductitemswithdetail entityObjct, DTOactiveproductitemswithdetail dto)
        {
            if (entityObjct == null) entityObjct = new activeproductitemswithdetail();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.User_ID = dto.User_ID;
            entityObjct.Consumer_ID = dto.Consumer_ID;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.activeProductItemPolicyNum = dto.activeProductItemPolicyNum;
            entityObjct.isActive = dto.isActive;
            entityObjct.Accepted = dto.Accepted;
            entityObjct.productValue = dto.productValue;
            entityObjct.duration = dto.duration;
            entityObjct.activeProductItemStartDate = dto.activeProductItemStartDate;
            entityObjct.activeProductItemEndDate = dto.activeProductItemEndDate;
            entityObjct.ProductProvider_ID = dto.ProductProvider_ID;
            entityObjct.ProductType_ID = dto.ProductType_ID;
            entityObjct.productName = dto.productName;
            entityObjct.productDescription = dto.productDescription;
            entityObjct.productPolicyDocPath = dto.productPolicyDocPath;
            entityObjct.isAvailableForPurchase = dto.isAvailableForPurchase;
            entityObjct.InsuranceType_ID = dto.InsuranceType_ID;
            entityObjct.ipCoverAmount = dto.ipCoverAmount;
            entityObjct.ipUnitType = dto.ipUnitType;
            entityObjct.UnitTypeDescription = dto.UnitTypeDescription;
            entityObjct.ipUnitCost = dto.ipUnitCost;
            entityObjct.claimTimeframe = dto.claimTimeframe;
            entityObjct.claimContactNo = dto.claimContactNo;
            entityObjct.claimtemplate_ID = dto.claimtemplate_ID;

            return entityObjct;
        }


        public static chrisviewconsumeractiveproduct updateEntity(chrisviewconsumeractiveproduct entityObjct, DTOchrisviewconsumeractiveproduct dto)
        {
            if (entityObjct == null) entityObjct = new chrisviewconsumeractiveproduct();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.activeProductItemStartDate = dto.activeProductItemStartDate;
            entityObjct.Consumer_ID = dto.Consumer_ID;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.duration = dto.duration;
            entityObjct.productValue = dto.productValue;
            entityObjct.productName = dto.productName;
            entityObjct.productDescription = dto.productDescription;
            entityObjct.InsuranceType_ID = dto.InsuranceType_ID;
            entityObjct.ipCoverAmount = dto.ipCoverAmount;
            entityObjct.UnitTypeDescription = dto.UnitTypeDescription;

            return entityObjct;
        }


        public static claim updateEntity(claim entityObjct, DTOclaim dto)
        {
            if (entityObjct == null) entityObjct = new claim();

            entityObjct.Claim_ID = dto.Claim_ID;
            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.capturedClaimFormDataJson = dto.capturedClaimFormDataJson;
            entityObjct.claimDate = dto.claimDate;
            entityObjct.claimStatus = dto.claimStatus;
            entityObjct.claimPaymentFinalised = dto.claimPaymentFinalised;
            entityObjct.Consumer_ID = dto.Consumer_ID;

            return entityObjct;
        }


        public static claimtemplate updateEntity(claimtemplate entityObjct, DTOclaimtemplate dto)
        {
            if (entityObjct == null) entityObjct = new claimtemplate();

            entityObjct.claimtemplate_ID = dto.claimtemplate_ID;
            entityObjct.templateName = dto.templateName;
            entityObjct.formDataRequiredJson = dto.formDataRequiredJson;

            return entityObjct;
        }


        public static claimuploaddocument updateEntity(claimuploaddocument entityObjct, DTOclaimuploaddocument dto)
        {
            if (entityObjct == null) entityObjct = new claimuploaddocument();

            entityObjct.claimUploadDocument_ID = dto.claimUploadDocument_ID;
            entityObjct.User_ID = dto.User_ID;
            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.document_ID = dto.document_ID;
            entityObjct.claimUploadDocumentPath = dto.claimUploadDocumentPath;
            entityObjct.Claim_ID = dto.Claim_ID;

            return entityObjct;
        }


        public static consumer updateEntity(consumer entityObjct, DTOconsumer dto)
        {
            if (entityObjct == null) entityObjct = new consumer();

            entityObjct.Consumer_ID = dto.Consumer_ID;
            entityObjct.User_ID = dto.User_ID;
            entityObjct.consumerDateOfBirth = dto.consumerDateOfBirth;
            entityObjct.consumerAddress = dto.consumerAddress;
            entityObjct.gender = dto.gender;
            entityObjct.maritalStatus = dto.maritalStatus;
            entityObjct.topProductCategoriesInterestedIn = dto.topProductCategoriesInterestedIn;
            entityObjct.homeOwnerType = dto.homeOwnerType;
            entityObjct.employmentStatus = dto.employmentStatus;
            entityObjct.grossMonthlyIncome = dto.grossMonthlyIncome;
            entityObjct.nettMonthlyIncome = dto.nettMonthlyIncome;
            entityObjct.totalMonthlyExpenses = dto.totalMonthlyExpenses;
            entityObjct.Location_ID = dto.Location_ID;
            entityObjct.numDependant = dto.numDependant;
            entityObjct.numClaims = dto.numClaims;
            entityObjct.ageGroup_ID = dto.ageGroup_ID;

            return entityObjct;
        }

        public static consumernumclaim updateEntity(consumernumclaim entityObjct, DTOconsumernumclaim dto)
        {
            if (entityObjct == null) entityObjct = new consumernumclaim();

            entityObjct.Consumer_ID = dto.Consumer_ID;
            entityObjct.numClaims = dto.numClaims;

            return entityObjct;
        }


        public static consumerriskvalue updateEntity(consumerriskvalue entityObjct, DTOconsumerriskvalue dto)
        {
            if (entityObjct == null) entityObjct = new consumerriskvalue();

            entityObjct.idConsumer = dto.idConsumer;
            entityObjct.Consumer_ID = dto.Consumer_ID;
            entityObjct.age = dto.age;
            entityObjct.ageGroup = dto.ageGroup;
            entityObjct.ageRiskValue = dto.ageRiskValue;
            entityObjct.gender = dto.gender;
            entityObjct.genderRiskValue = dto.genderRiskValue;
            entityObjct.maritalStatus = dto.maritalStatus;
            entityObjct.maritalStatusRiskValue = dto.maritalStatusRiskValue;
            entityObjct.employmentStatus = dto.employmentStatus;
            entityObjct.employmentStatusRiskValue = dto.employmentStatusRiskValue;
            entityObjct.claimRate = dto.claimRate;
            entityObjct.claimRiskValue = dto.claimRiskValue;
            entityObjct.RiskCategory = dto.RiskCategory;
            entityObjct.OverallRiskValue = dto.OverallRiskValue;
            entityObjct.purchasedProducts = dto.purchasedProducts;
            entityObjct.numUnprocessed = dto.numUnprocessed;
            entityObjct.consumerName = dto.consumerName;
            entityObjct.purchasedProductIDs = dto.purchasedProductIDs;

            return entityObjct;
        }


        public static contactlist updateEntity(contactlist entityObjct, DTOcontactlist dto)
        {
            if (entityObjct == null) entityObjct = new contactlist();

            entityObjct.idcontactlist = dto.idcontactlist;
            entityObjct.UserID = dto.UserID;
            entityObjct.ContactsUserID = dto.ContactsUserID;

            return entityObjct;
        }


        public static currentmonthdailysale updateEntity(currentmonthdailysale entityObjct, DTOcurrentmonthdailysale dto)
        {
            if (entityObjct == null) entityObjct = new currentmonthdailysale();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.activeProductItemStartDate = dto.activeProductItemStartDate;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static demographicconsumerproductlocationlastmonthsale updateEntity(demographicconsumerproductlocationlastmonthsale entityObjct, DTOdemographicconsumerproductlocationlastmonthsale dto)
        {
            if (entityObjct == null) entityObjct = new demographicconsumerproductlocationlastmonthsale();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.datum = dto.datum;
            entityObjct.transactionLocation = dto.transactionLocation;
            entityObjct.numConsumers = dto.numConsumers;
            entityObjct.gender = dto.gender;
            entityObjct.maritalStatus = dto.maritalStatus;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.numMartialStatus = dto.numMartialStatus;
            entityObjct.employmentStatus = dto.employmentStatus;
            entityObjct.numEmploymentStatus = dto.numEmploymentStatus;
            entityObjct.netIncome = dto.netIncome;
            entityObjct.NumDependants = dto.NumDependants;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static demographicconsumerproductlocationmonthlysale updateEntity(demographicconsumerproductlocationmonthlysale entityObjct, DTOdemographicconsumerproductlocationmonthlysale dto)
        {
            if (entityObjct == null) entityObjct = new demographicconsumerproductlocationmonthlysale();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.datum = dto.datum;
            entityObjct.transactionLocation = dto.transactionLocation;
            entityObjct.numConsumers = dto.numConsumers;
            entityObjct.gender = dto.gender;
            entityObjct.maritalStatus = dto.maritalStatus;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.numMartialStatus = dto.numMartialStatus;
            entityObjct.employmentStatus = dto.employmentStatus;
            entityObjct.numEmploymentStatus = dto.numEmploymentStatus;
            entityObjct.netIncome = dto.netIncome;
            entityObjct.NumDependants = dto.NumDependants;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static demographicconsumerproductlocationsaleslastmonth updateEntity(demographicconsumerproductlocationsaleslastmonth entityObjct, DTOdemographicconsumerproductlocationsaleslastmonth dto)
        {
            if (entityObjct == null) entityObjct = new demographicconsumerproductlocationsaleslastmonth();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.datum = dto.datum;
            entityObjct.transactionLocation = dto.transactionLocation;
            entityObjct.numConsumers = dto.numConsumers;
            entityObjct.gender = dto.gender;
            entityObjct.maritalStatus = dto.maritalStatus;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.numMartialStatus = dto.numMartialStatus;
            entityObjct.employmentStatus = dto.employmentStatus;
            entityObjct.numEmploymentStatus = dto.numEmploymentStatus;
            entityObjct.netIncome = dto.netIncome;
            entityObjct.NumDependants = dto.NumDependants;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


       


        public static documentspecification updateEntity(documentspecification entityObjct, DTOdocumentspecification dto)
        {
            if (entityObjct == null) entityObjct = new documentspecification();

            entityObjct.document_ID = dto.document_ID;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.documentName = dto.documentName;
            entityObjct.documentDescription = dto.documentDescription;
            entityObjct.docPreferredFormat = dto.docPreferredFormat;
            entityObjct.docPreparationRequired = dto.docPreparationRequired;

            return entityObjct;
        }


        public static insuranceproduct updateEntity(insuranceproduct entityObjct, DTOinsuranceproduct dto)
        {
            if (entityObjct == null) entityObjct = new insuranceproduct();

            entityObjct.InsuranceProduct_ID = dto.InsuranceProduct_ID;
            entityObjct.ProductProvider_ID = dto.ProductProvider_ID;
            entityObjct.InsuranceType_ID = dto.InsuranceType_ID;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.ipCoverAmount = dto.ipCoverAmount;
            entityObjct.ipUnitCost = dto.ipUnitCost;
            entityObjct.ipUnitType = dto.ipUnitType;
            entityObjct.ipMinimunNoOfUnits = dto.ipMinimunNoOfUnits;
            entityObjct.ipClaimInfoPath = dto.ipClaimInfoPath;
            entityObjct.claimTimeframe = dto.claimTimeframe;
            entityObjct.policyNumberApiLink = dto.policyNumberApiLink;
            entityObjct.ApiKey = dto.ApiKey;
            entityObjct.claimContactNo = dto.claimContactNo;
            entityObjct.claimFormPath = dto.claimFormPath;
            entityObjct.claimtemplate_ID = dto.claimtemplate_ID;

            return entityObjct;
        }


        public static insuranceproducttypemonthlysale updateEntity(insuranceproducttypemonthlysale entityObjct, DTOinsuranceproducttypemonthlysale dto)
        {
            if (entityObjct == null) entityObjct = new insuranceproducttypemonthlysale();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.monthDate = dto.monthDate;
            entityObjct.InsuranceType_ID = dto.InsuranceType_ID;
            entityObjct.insuranctTypeDescription = dto.insuranctTypeDescription;
            entityObjct.datum = dto.datum;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static insurancetype updateEntity(insurancetype entityObjct, DTOinsurancetype dto)
        {
            if (entityObjct == null) entityObjct = new insurancetype();

            entityObjct.InsuranceType_ID = dto.InsuranceType_ID;
            entityObjct.insuranctTypeDescription = dto.insuranctTypeDescription;
            entityObjct.RequirementsForPurchase = dto.RequirementsForPurchase;

            return entityObjct;
        }


        public static lastmonthinsurancetypesale updateEntity(lastmonthinsurancetypesale entityObjct, DTOlastmonthinsurancetypesale dto)
        {
            if (entityObjct == null) entityObjct = new lastmonthinsurancetypesale();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.InsuranceType_ID = dto.InsuranceType_ID;
            entityObjct.monthSales = dto.monthSales;

            return entityObjct;
        }


        public static lastmonthprovincesale updateEntity(lastmonthprovincesale entityObjct, DTOlastmonthprovincesale dto)
        {
            if (entityObjct == null) entityObjct = new lastmonthprovincesale();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.datum = dto.datum;
            entityObjct.ProductProvider_ID = dto.ProductProvider_ID;
            entityObjct.Province = dto.Province;
            entityObjct.LatLng = dto.LatLng;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static location updateEntity(location entityObjct, DTOlocation dto)
        {
            if (entityObjct == null) entityObjct = new location();

            entityObjct.Location_ID = dto.Location_ID;
            entityObjct.Province = dto.Province;
            entityObjct.City = dto.City;
            entityObjct.LatLng = dto.LatLng;
            entityObjct.PostalCode = dto.PostalCode;
            entityObjct.GDP = dto.GDP;
            entityObjct.UnemploymentRate = dto.UnemploymentRate;

            return entityObjct;
        }


        public static locationsaleslastmonth updateEntity(locationsaleslastmonth entityObjct, DTOlocationsaleslastmonth dto)
        {
            if (entityObjct == null) entityObjct = new locationsaleslastmonth();

            entityObjct.purchaseDate = dto.purchaseDate;
            entityObjct.datum = dto.datum;
            entityObjct.ProductProvider_ID = dto.ProductProvider_ID;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.Location_ID = dto.Location_ID;
            entityObjct.Province = dto.Province;
            entityObjct.city = dto.city;
            entityObjct.LatLng = dto.LatLng;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static monlthlocationsalessum updateEntity(monlthlocationsalessum entityObjct, DTOmonlthlocationsalessum dto)
        {
            if (entityObjct == null) entityObjct = new monlthlocationsalessum();

            entityObjct.purchaseDate = dto.purchaseDate;
            entityObjct.datum = dto.datum;
            entityObjct.ProductProvider_ID = dto.ProductProvider_ID;
            entityObjct.Location_ID = dto.Location_ID;
            entityObjct.Province = dto.Province;
            entityObjct.city = dto.city;
            entityObjct.LatLng = dto.LatLng;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static monthlylocationsale updateEntity(monthlylocationsale entityObjct, DTOmonthlylocationsale dto)
        {
            if (entityObjct == null) entityObjct = new monthlylocationsale();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.datum = dto.datum;
            entityObjct.dateM = dto.dateM;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.productName = dto.productName;
            entityObjct.transactionLocation = dto.transactionLocation;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static monthlyproductsalesperlocation updateEntity(monthlyproductsalesperlocation entityObjct, DTOmonthlyproductsalesperlocation dto)
        {
            if (entityObjct == null) entityObjct = new monthlyproductsalesperlocation();

            entityObjct.purchaseDate = dto.purchaseDate;
            entityObjct.datum = dto.datum;
            entityObjct.ProductProvider_ID = dto.ProductProvider_ID;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.productName = dto.productName;
            entityObjct.Location_ID = dto.Location_ID;
            entityObjct.Province = dto.Province;
            entityObjct.city = dto.city;
            entityObjct.LatLng = dto.LatLng;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static monthlyprovincesalesview updateEntity(monthlyprovincesalesview entityObjct, DTOmonthlyprovincesalesview dto)
        {
            if (entityObjct == null) entityObjct = new monthlyprovincesalesview();

            entityObjct.datum = dto.datum;
            entityObjct.ProductProvider_ID = dto.ProductProvider_ID;
            entityObjct.Province = dto.Province;
            entityObjct.LatLng = dto.LatLng;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static monthlyprovincialproducttypedistribution updateEntity(monthlyprovincialproducttypedistribution entityObjct, DTOmonthlyprovincialproducttypedistribution dto)
        {
            if (entityObjct == null) entityObjct = new monthlyprovincialproducttypedistribution();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.activeProductItemEndDate = dto.activeProductItemEndDate;
            entityObjct.InsuranceType_ID = dto.InsuranceType_ID;
            entityObjct.Province = dto.Province;
            entityObjct.insuranctTypeDescription = dto.insuranctTypeDescription;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }



        public static notificationlog updateEntity(notificationlog entityObjct, DTOnotificationlog dto)
        {
            if (entityObjct == null) entityObjct = new notificationlog();

            entityObjct.NotificationLog_ID = dto.NotificationLog_ID;
            entityObjct.notificationType = dto.notificationType;
            entityObjct.notificationReceiver = dto.notificationReceiver;
            entityObjct.notificationDateSent = dto.notificationDateSent;

            return entityObjct;
        }


        public static otpview updateEntity(otpview entityObjct, DTOotpview dto)
        {
            if (entityObjct == null) entityObjct = new otpview();

            entityObjct.User_ID = dto.User_ID;
            entityObjct.otpCode = dto.otpCode;
            entityObjct.otpRetryCount = dto.otpRetryCount;
            entityObjct.otpExpirationTime = dto.otpExpirationTime;
            entityObjct.otpNextAllowedTime = dto.otpNextAllowedTime;
            entityObjct.otpRecordCreated = dto.otpRecordCreated;

            return entityObjct;
        }


        public static overallproductlocationsale updateEntity(overallproductlocationsale entityObjct, DTOoverallproductlocationsale dto)
        {
            if (entityObjct == null) entityObjct = new overallproductlocationsale();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.transactionLocation = dto.transactionLocation;
            entityObjct.numConsumers = dto.numConsumers;
            entityObjct.gender = dto.gender;
            entityObjct.maritalStatus = dto.maritalStatus;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.numMartialStatus = dto.numMartialStatus;
            entityObjct.employmentStatus = dto.employmentStatus;
            entityObjct.numEmploymentStatus = dto.numEmploymentStatus;
            entityObjct.netIncome = dto.netIncome;
            entityObjct.NumDependants = dto.NumDependants;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static processapplication updateEntity(processapplication entityObjct, DTOprocessapplication dto)
        {
            if (entityObjct == null) entityObjct = new processapplication();

            entityObjct.ProcessApplication_ID = dto.ProcessApplication_ID;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.OperationType = dto.OperationType;
            entityObjct.value1 = dto.value1;
            entityObjct.value2 = dto.value2;

            return entityObjct;
        }


        public static product updateEntity(product entityObjct, DTOproduct dto)
        {
            if (entityObjct == null) entityObjct = new product();

            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.ProductProvider_ID = dto.ProductProvider_ID;
            entityObjct.ProductType_ID = dto.ProductType_ID;
            entityObjct.productName = dto.productName;
            entityObjct.productDescription = dto.productDescription;
            entityObjct.detailedDescription = dto.detailedDescription;
            entityObjct.productPolicyDocPath = dto.productPolicyDocPath;
            entityObjct.isAvailableForPurchase = dto.isAvailableForPurchase;
            entityObjct.salesTargetAmount = dto.salesTargetAmount;
            entityObjct.ratingAverage = dto.ratingAverage;
            entityObjct.numTimesRated = dto.numTimesRated;

            return entityObjct;
        }

        public static productlocationmonthlysale updateEntity(productlocationmonthlysale entityObjct, DTOproductlocationmonthlysale dto)
        {
            if (entityObjct == null) entityObjct = new productlocationmonthlysale();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.date_format_activeProductItemStartDate___Y__b__ = dto.date_format_activeProductItemStartDate___Y__b__;
            entityObjct.transactionLocation = dto.transactionLocation;
            entityObjct.productName = dto.productName;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


       


        public static productprovider updateEntity(productprovider entityObjct, DTOproductprovider dto)
        {
            if (entityObjct == null) entityObjct = new productprovider();

            entityObjct.ProductProvider_ID = dto.ProductProvider_ID;
            entityObjct.User_ID = dto.User_ID;
            entityObjct.ppCompanyName = dto.ppCompanyName;
            entityObjct.ppVATnumber = dto.ppVATnumber;
            entityObjct.ppFaxNumber = dto.ppFaxNumber;
            entityObjct.ppAddress = dto.ppAddress;
            entityObjct.lastAssignedPolicyNumber = dto.lastAssignedPolicyNumber;

            return entityObjct;
        }


        public static productproviderpayment updateEntity(productproviderpayment entityObjct, DTOproductproviderpayment dto)
        {
            if (entityObjct == null) entityObjct = new productproviderpayment();

            entityObjct.Productproviderpayment_ID = dto.Productproviderpayment_ID;
            entityObjct.ProductProvider_ID = dto.ProductProvider_ID;
            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.Description = dto.Description;
            entityObjct.AmountToPay = dto.AmountToPay;
            entityObjct.hasBeenPayed = dto.hasBeenPayed;

            return entityObjct;
        }


        public static productprovideryearlysale updateEntity(productprovideryearlysale entityObjct, DTOproductprovideryearlysale dto)
        {
            if (entityObjct == null) entityObjct = new productprovideryearlysale();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.ProductProvider_ID = dto.ProductProvider_ID;
            entityObjct.activeProductItemStartDate = dto.activeProductItemStartDate;
            entityObjct.yearSales = dto.yearSales;

            return entityObjct;
        }


        public static productredemptionlog updateEntity(productredemptionlog entityObjct, DTOproductredemptionlog dto)
        {
            if (entityObjct == null) entityObjct = new productredemptionlog();

            entityObjct.Voucher_ID = dto.Voucher_ID;
            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.transactionAmount = dto.transactionAmount;

            return entityObjct;
        }


        public static productsalespermonth updateEntity(productsalespermonth entityObjct, DTOproductsalespermonth dto)
        {
            if (entityObjct == null) entityObjct = new productsalespermonth();

            entityObjct.activeProductItemStartDate = dto.activeProductItemStartDate;
            entityObjct.datum = dto.datum;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.productName = dto.productName;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static productswithpurchas updateEntity(productswithpurchas entityObjct, DTOproductswithpurchas dto)
        {
            if (entityObjct == null) entityObjct = new productswithpurchas();

            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.name = dto.name;
            entityObjct.InsuranceType_ID = dto.InsuranceType_ID;
            entityObjct.insuranctTypeDescription = dto.insuranctTypeDescription;

            return entityObjct;
        }


        public static producttype updateEntity(producttype entityObjct, DTOproducttype dto)
        {
            if (entityObjct == null) entityObjct = new producttype();

            entityObjct.ProductType_ID = dto.ProductType_ID;
            entityObjct.ProductTypeName = dto.ProductTypeName;

            return entityObjct;
        }


        public static provincialinsurancetypesale updateEntity(provincialinsurancetypesale entityObjct, DTOprovincialinsurancetypesale dto)
        {
            if (entityObjct == null) entityObjct = new provincialinsurancetypesale();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.datum = dto.datum;
            entityObjct.InsuranceType_ID = dto.InsuranceType_ID;
            entityObjct.insuranctTypeDescription = dto.insuranctTypeDescription;
            entityObjct.Province = dto.Province;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static provincialproducttypedistributionlastmonth updateEntity(provincialproducttypedistributionlastmonth entityObjct, DTOprovincialproducttypedistributionlastmonth dto)
        {
            if (entityObjct == null) entityObjct = new provincialproducttypedistributionlastmonth();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.activeProductItemEndDate = dto.activeProductItemEndDate;
            entityObjct.InsuranceType_ID = dto.InsuranceType_ID;
            entityObjct.Province = dto.Province;
            entityObjct.insuranctTypeDescription = dto.insuranctTypeDescription;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static reseller updateEntity(reseller entityObjct, DTOreseller dto)
        {
            if (entityObjct == null) entityObjct = new reseller();

            entityObjct.Reseller_ID = dto.Reseller_ID;
            entityObjct.User_ID = dto.User_ID;
            entityObjct.resellerIsValidated = dto.resellerIsValidated;
            entityObjct.cardNumber = dto.cardNumber;
            entityObjct.cardExpirationMonth_Year = dto.cardExpirationMonth_Year;
            entityObjct.cardCVV = dto.cardCVV;
            entityObjct.nameOnCard = dto.nameOnCard;
            entityObjct.resellerBankBranchName = dto.resellerBankBranchName;
            entityObjct.resellerBankAccountNumber = dto.resellerBankAccountNumber;
            entityObjct.resellerBankName = dto.resellerBankName;
            entityObjct.resellerBankBranchCode = dto.resellerBankBranchCode;
            entityObjct.resellerDateOfBirth = dto.resellerDateOfBirth;
            entityObjct.street = dto.street;
            entityObjct.city = dto.city;
            entityObjct.postalCode = dto.postalCode;
            entityObjct.province = dto.province;
            entityObjct.country = dto.country;
            entityObjct.sellingLocation = dto.sellingLocation;
            entityObjct.isSharingLocation = dto.isSharingLocation;
            entityObjct.StartedSharingTime = dto.StartedSharingTime;
            entityObjct.minutesAvailable = dto.minutesAvailable;
            entityObjct.LocationID = dto.LocationID;
            entityObjct.isLocationAvailable = dto.isLocationAvailable;

            return entityObjct;
        }


        public static resellersalespermonth updateEntity(resellersalespermonth entityObjct, DTOresellersalespermonth dto)
        {
            if (entityObjct == null) entityObjct = new resellersalespermonth();

            entityObjct.VoucherSentTo = dto.VoucherSentTo;
            entityObjct.transactionDate = dto.transactionDate;
            entityObjct.Sender_ID = dto.Sender_ID;
            entityObjct.sold = dto.sold;

            return entityObjct;
        }


        public static resellersendmonthlysale updateEntity(resellersendmonthlysale entityObjct, DTOresellersendmonthlysale dto)
        {
            if (entityObjct == null) entityObjct = new resellersendmonthlysale();

            entityObjct.VoucherSentTo = dto.VoucherSentTo;
            entityObjct.Sender_ID = dto.Sender_ID;
            entityObjct.date_format_vouchertransaction_transactionDate___Y__b__ = dto.date_format_vouchertransaction_transactionDate___Y__b__;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static resellersendvouchergenderspecific updateEntity(resellersendvouchergenderspecific entityObjct, DTOresellersendvouchergenderspecific dto)
        {
            if (entityObjct == null) entityObjct = new resellersendvouchergenderspecific();

            entityObjct.VoucherSentTo = dto.VoucherSentTo;
            entityObjct.date_format_vouchertransaction_transactionDate___Y__b__ = dto.date_format_vouchertransaction_transactionDate___Y__b__;
            entityObjct.gender = dto.gender;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static risk_agegroup updateEntity(risk_agegroup entityObjct, DTOrisk_agegroup dto)
        {
            if (entityObjct == null) entityObjct = new risk_agegroup();

            entityObjct.ageGroup_ID = dto.ageGroup_ID;
            entityObjct.description = dto.description;
            entityObjct.lowest = dto.lowest;
            entityObjct.highest = dto.highest;

            return entityObjct;
        }


       


        public static salespermonth updateEntity(salespermonth entityObjct, DTOsalespermonth dto)
        {
            if (entityObjct == null) entityObjct = new salespermonth();

            entityObjct.activeProductItems_ID = dto.activeProductItems_ID;
            entityObjct.datum = dto.datum;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static systemadmin updateEntity(systemadmin entityObjct, DTOsystemadmin dto)
        {
            if (entityObjct == null) entityObjct = new systemadmin();

            entityObjct.SystemAdmin_ID = dto.SystemAdmin_ID;
            entityObjct.User_ID = dto.User_ID;
            entityObjct.systemAdminOTP = dto.systemAdminOTP;
            entityObjct.systemAdminHasTwoPartAuth = dto.systemAdminHasTwoPartAuth;

            return entityObjct;
        }


        public static transactiontype updateEntity(transactiontype entityObjct, DTOtransactiontype dto)
        {
            if (entityObjct == null) entityObjct = new transactiontype();

            entityObjct.TransactionType_ID = dto.TransactionType_ID;
            entityObjct.transactionTypeDescription = dto.transactionTypeDescription;

            return entityObjct;
        }


        public static unittype updateEntity(unittype entityObjct, DTOunittype dto)
        {
            if (entityObjct == null) entityObjct = new unittype();

            entityObjct.UnitType_ID = dto.UnitType_ID;
            entityObjct.UnitTypeDescription = dto.UnitTypeDescription;

            return entityObjct;
        }


        public static unprocessedapplication updateEntity(unprocessedapplication entityObjct, DTOunprocessedapplication dto)
        {
            if (entityObjct == null) entityObjct = new unprocessedapplication();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.Consumer_ID = dto.Consumer_ID;
            entityObjct.productName = dto.productName;
            entityObjct.datum = dto.datum;
            entityObjct.productValue = dto.productValue;

            return entityObjct;
        }


        public static user updateEntity(user entityObjct, DTOuser dto)
        {
            if (entityObjct == null) entityObjct = new user();

            entityObjct.User_ID = dto.User_ID;
            entityObjct.userFirstName = dto.userFirstName;
            entityObjct.userLastName = dto.userLastName;
            entityObjct.userName = dto.userName;
            entityObjct.userEmail = dto.userEmail;
            entityObjct.userContactNumber = dto.userContactNumber;
            entityObjct.userPassword = dto.userPassword;
            entityObjct.userIsActive = dto.userIsActive;
            entityObjct.userType = dto.userType;
            entityObjct.userActivationType = dto.userActivationType;
            entityObjct.IDnumber = dto.IDnumber;
            entityObjct.IdDocumentPath = dto.IdDocumentPath;
            entityObjct.IdDocumentLastUpdated = dto.IdDocumentLastUpdated;
            entityObjct.timeStap = dto.timeStap;
            entityObjct.resetPasswordKey = dto.resetPasswordKey;
            entityObjct.blockchainAddress = dto.blockchainAddress;
            entityObjct.otpCode = dto.otpCode;
            entityObjct.otpRetryCount = dto.otpRetryCount;
            entityObjct.otpExpirationTime = dto.otpExpirationTime;
            entityObjct.otpNextAllowedTime = dto.otpNextAllowedTime;
            entityObjct.otpRecordCreated = dto.otpRecordCreated;

            return entityObjct;
        }


        public static usertype updateEntity(usertype entityObjct, DTOusertype dto)
        {
            if (entityObjct == null) entityObjct = new usertype();

            entityObjct.UserType_ID = dto.UserType_ID;
            entityObjct.UserTypeDescription = dto.UserTypeDescription;

            return entityObjct;
        }


        public static validator updateEntity(validator entityObjct, DTOvalidator dto)
        {
            if (entityObjct == null) entityObjct = new validator();

            entityObjct.Validator_ID = dto.Validator_ID;
            entityObjct.User_ID = dto.User_ID;
            entityObjct.validatiorCompany = dto.validatiorCompany;
            entityObjct.validatorLicenseNumber = dto.validatorLicenseNumber;
            entityObjct.validatorLicenseProvider = dto.validatorLicenseProvider;
            entityObjct.validatorValidUntil = dto.validatorValidUntil;
            entityObjct.validatorVATNumber = dto.validatorVATNumber;
            entityObjct.validatorAddress = dto.validatorAddress;
            entityObjct.validatorBankName = dto.validatorBankName;
            entityObjct.validatorBankAccNumber = dto.validatorBankAccNumber;
            entityObjct.validatorBankBranchName = dto.validatorBankBranchName;
            entityObjct.validatorBankBranchCode = dto.validatorBankBranchCode;

            return entityObjct;
        }


        public static voucher updateEntity(voucher entityObjct, DTOvoucher dto)
        {
            if (entityObjct == null) entityObjct = new voucher();

            entityObjct.Voucher_ID = dto.Voucher_ID;
            entityObjct.VoucherType_ID = dto.VoucherType_ID;
            entityObjct.User_ID = dto.User_ID;
            entityObjct.voucherValue = dto.voucherValue;
            entityObjct.voucherCreationDate = dto.voucherCreationDate;
            entityObjct.OTP = dto.OTP;
            entityObjct.OTPtimeStap = dto.OTPtimeStap;
            entityObjct.QRdata = dto.QRdata;
            entityObjct.QRtimeStap = dto.QRtimeStap;

            return entityObjct;
        }


        public static vouchertransaction updateEntity(vouchertransaction entityObjct, DTOvouchertransaction dto)
        {
            if (entityObjct == null) entityObjct = new vouchertransaction();

            entityObjct.Voucher_ID = dto.Voucher_ID;
            entityObjct.VoucherSentTo = dto.VoucherSentTo;
            entityObjct.Sender_ID = dto.Sender_ID;
            entityObjct.Receiver_ID = dto.Receiver_ID;
            entityObjct.TransactionType_ID = dto.TransactionType_ID;
            entityObjct.transactionAmount = dto.transactionAmount;
            entityObjct.transactionDescription = dto.transactionDescription;
            entityObjct.transactionDate = dto.transactionDate;

            return entityObjct;
        }


        public static vouchertype updateEntity(vouchertype entityObjct, DTOvouchertype dto)
        {
            if (entityObjct == null) entityObjct = new vouchertype();

            entityObjct.VoucherType_ID = dto.VoucherType_ID;
            entityObjct.voucherTypeDescription = dto.voucherTypeDescription;

            return entityObjct;
        }



    }
}