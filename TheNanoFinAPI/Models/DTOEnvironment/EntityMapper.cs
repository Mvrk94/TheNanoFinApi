using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheNanoFinAPI.Models.DTOEnvironment
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
            entityObjct.productValue = dto.productValue;
            entityObjct.duration = dto.duration;
            entityObjct.activeProductItemStartDate = dto.activeProductItemStartDate;
            entityObjct.transactionlocation = dto.transactionlocation;
            entityObjct.activeProductItemEndDate = dto.activeProductItemEndDate;
            entityObjct.PurchaseConfirmationDocPath = dto.PurchaseConfirmationDocPath;

            return entityObjct;
        }


        public static claim updateEntity(claim entityObjct, DTOclaim dto)
        {
            if (entityObjct == null) entityObjct = new claim();

            entityObjct.Claim_ID = dto.Claim_ID;
            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.capturedClaimFormDataJson = dto.capturedClaimFormDataJson;

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

            return entityObjct;
        }


        public static document updateEntity(document entityObjct, DTOdocument dto)
        {
            if (entityObjct == null) entityObjct = new document();

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


        public static insurancetype updateEntity(insurancetype entityObjct, DTOinsurancetype dto)
        {
            if (entityObjct == null) entityObjct = new insurancetype();

            entityObjct.InsuranceType_ID = dto.InsuranceType_ID;
            entityObjct.insuranctTypeDescription = dto.insuranctTypeDescription;

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


        public static monthlylocationsale updateEntity(monthlylocationsale entityObjct, DTOmonthlylocationsale dto)
        {
            if (entityObjct == null) entityObjct = new monthlylocationsale();

            entityObjct.datum = dto.datum;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.transactionLocation = dto.transactionLocation;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static monthlylocationsalessum updateEntity(monthlylocationsalessum entityObjct, DTOmonthlylocationsalessum dto)
        {
            if (entityObjct == null) entityObjct = new monthlylocationsalessum();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
            entityObjct.datum = dto.datum;
            entityObjct.transactionLocation = dto.transactionLocation;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static monthlyproductsalesperlocation updateEntity(monthlyproductsalesperlocation entityObjct, DTOmonthlyproductsalesperlocation dto)
        {
            if (entityObjct == null) entityObjct = new monthlyproductsalesperlocation();

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



        public static notificationlog updateEntity(notificationlog entityObjct, DTOnotificationlog dto)
        {
            if (entityObjct == null) entityObjct = new notificationlog();

            entityObjct.NotificationLog_ID = dto.NotificationLog_ID;
            entityObjct.notificationType = dto.notificationType;
            entityObjct.notificationReceiver = dto.notificationReceiver;
            entityObjct.notificationDateSent = dto.notificationDateSent;

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


        public static productprovider updateEntity(productprovider entityObjct, DTOproductprovider dto)
        {
            if (entityObjct == null) entityObjct = new productprovider();

            entityObjct.ProductProvider_ID = dto.ProductProvider_ID;
            entityObjct.User_ID = dto.User_ID;
            entityObjct.ppCompanyName = dto.ppCompanyName;
            entityObjct.ppVATnumber = dto.ppVATnumber;
            entityObjct.ppFaxNumber = dto.ppFaxNumber;
            entityObjct.ppAddress = dto.ppAddress;

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

            entityObjct.datum = dto.datum;
            entityObjct.Product_ID = dto.Product_ID;
            entityObjct.productName = dto.productName;
            entityObjct.sales = dto.sales;

            return entityObjct;
        }


        public static producttype updateEntity(producttype entityObjct, DTOproducttype dto)
        {
            if (entityObjct == null) entityObjct = new producttype();

            entityObjct.ProductType_ID = dto.ProductType_ID;
            entityObjct.ProductTypeName = dto.ProductTypeName;

            return entityObjct;
        }


        public static reseller updateEntity(reseller entityObjct, DTOreseller dto)
        {
            if (entityObjct == null) entityObjct = new reseller();

            entityObjct.Reseller_ID = dto.Reseller_ID;
            entityObjct.User_ID = dto.User_ID;
            entityObjct.resellerIsValidated = dto.resellerIsValidated;
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

            return entityObjct;
        }


        public static salespermonth updateEntity(salespermonth entityObjct, DTOsalespermonth dto)
        {
            if (entityObjct == null) entityObjct = new salespermonth();

            entityObjct.ActiveProductItems_ID = dto.ActiveProductItems_ID;
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

            return entityObjct;
        }


        public static usertype updateEntity(usertype entityObjct, DTOusertype dto)
        {
            if (entityObjct == null) entityObjct = new usertype();

            entityObjct.UserType_ID = dto.UserType_ID;
            entityObjct.UserTypeDescription = dto.UserTypeDescription;

            return entityObjct;
        }

        internal static object updateEntity(user putUser, DTOconsumerUserProfileInfo dtoConsumerProfile)
        {
            throw new NotImplementedException();
        }

        internal static object updateEntity(consumer putConsumer, DTOconsumerUserProfileInfo dtoConsumerProfile)
        {
            throw new NotImplementedException();
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