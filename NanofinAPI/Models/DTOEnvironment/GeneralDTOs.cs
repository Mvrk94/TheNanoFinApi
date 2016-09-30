using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NanofinAPI.Models;

namespace NanofinAPI.Models.DTOEnvironment
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


        public class DTOactiveproductitemswithdetail
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
            public Nullable<System.DateTime> activeProductItemEndDate { get; set; }
            public int ProductProvider_ID { get; set; }
            public Nullable<int> ProductType_ID { get; set; }
            public string productName { get; set; }
            public string productDescription { get; set; }
            public string productPolicyDocPath { get; set; }
            public Nullable<bool> isAvailableForPurchase { get; set; }
            public int InsuranceType_ID { get; set; }
            public Nullable<decimal> ipCoverAmount { get; set; }
            public Nullable<int> ipUnitType { get; set; }
            public string UnitTypeDescription { get; set; }
            public Nullable<decimal> ipUnitCost { get; set; }
            public Nullable<System.DateTime> claimTimeframe { get; set; }
            public string claimContactNo { get; set; }
            public Nullable<int> claimtemplate_ID { get; set; }

            public DTOactiveproductitemswithdetail() { }

            public DTOactiveproductitemswithdetail(activeproductitemswithdetail entityObjct)
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
                InsuranceType_ID = entityObjct.InsuranceType_ID;
                ipCoverAmount = entityObjct.ipCoverAmount;
                ipUnitType = entityObjct.ipUnitType;
                UnitTypeDescription = entityObjct.UnitTypeDescription;
                ipUnitCost = entityObjct.ipUnitCost;
                claimTimeframe = entityObjct.claimTimeframe;
                claimContactNo = entityObjct.claimContactNo;
                claimtemplate_ID = entityObjct.claimtemplate_ID;
            }
        }


        public class DTOchrisviewconsumeractiveproduct
        {
            public int ActiveProductItems_ID { get; set; }
            public Nullable<System.DateTime> activeProductItemStartDate { get; set; }
            public int Consumer_ID { get; set; }
            public int Product_ID { get; set; }
            public int duration { get; set; }
            public decimal productValue { get; set; }
            public string productName { get; set; }
            public string productDescription { get; set; }
            public int InsuranceType_ID { get; set; }
            public Nullable<decimal> ipCoverAmount { get; set; }
            public string UnitTypeDescription { get; set; }

            public DTOchrisviewconsumeractiveproduct() { }

            public DTOchrisviewconsumeractiveproduct(chrisviewconsumeractiveproduct entityObjct)
            {
                ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
                activeProductItemStartDate = entityObjct.activeProductItemStartDate;
                Consumer_ID = entityObjct.Consumer_ID;
                Product_ID = entityObjct.Product_ID;
                duration = entityObjct.duration;
                productValue = entityObjct.productValue;
                productName = entityObjct.productName;
                productDescription = entityObjct.productDescription;
                InsuranceType_ID = entityObjct.InsuranceType_ID;
                ipCoverAmount = entityObjct.ipCoverAmount;
                UnitTypeDescription = entityObjct.UnitTypeDescription;
            }
        }


        public class DTOclaim
        {
            public int Claim_ID { get; set; }
            public Nullable<int> Consumer_ID { get; set; }
            public Nullable<int> ActiveProductItems_ID { get; set; }
            public string capturedClaimFormDataJson { get; set; }
            public Nullable<System.DateTime> claimDate { get; set; }
            public string claimStatus { get; set; }
            public string claimPaymentFinalised { get; set; }

            public DTOclaim() { }

            public DTOclaim(claim entityObjct)
            {
                Claim_ID = entityObjct.Claim_ID;
                Consumer_ID = entityObjct.Consumer_ID;
                ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
                capturedClaimFormDataJson = entityObjct.capturedClaimFormDataJson;
                claimDate = entityObjct.claimDate;
                claimStatus = entityObjct.claimStatus;
                claimPaymentFinalised = entityObjct.claimPaymentFinalised;
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
            public Nullable<int> document_ID { get; set; }
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
            public Nullable<int> numClaims { get; set; }
            public Nullable<int> ageGroup_ID { get; set; }

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
                numClaims = entityObjct.numClaims;
                ageGroup_ID = entityObjct.ageGroup_ID;
            }
        }


        public class DTOconsumernumclaim
        {
            public int Consumer_ID { get; set; }
            public long numClaims { get; set; }

            public DTOconsumernumclaim() { }

            public DTOconsumernumclaim(consumernumclaim entityObjct)
            {
                Consumer_ID = entityObjct.Consumer_ID;
                numClaims = entityObjct.numClaims;
            }
        }


        public class DTOconsumerriskvalue
        {
            public int idConsumer { get; set; }
            public Nullable<int> age { get; set; }
            public string ageGroup { get; set; }
            public Nullable<int> ageRiskValue { get; set; }
            public string gender { get; set; }
            public Nullable<int> genderRiskValue { get; set; }
            public string maritalStatus { get; set; }
            public Nullable<int> maritalStatusRiskValue { get; set; }
            public string employmentStatus { get; set; }
            public Nullable<int> employmentStatusRiskValue { get; set; }
            public Nullable<decimal> claimRate { get; set; }
            public Nullable<int> claimRiskValue { get; set; }
            public string RiskCategory { get; set; }
            public Nullable<int> OverallRiskValue { get; set; }
            public Nullable<int> Consumer_ID { get; set; }

            public DTOconsumerriskvalue() { }

            public DTOconsumerriskvalue(consumerriskvalue entityObjct)
            {
                idConsumer = entityObjct.idConsumer;
                age = entityObjct.age;
                ageGroup = entityObjct.ageGroup;
                ageRiskValue = entityObjct.ageRiskValue;
                gender = entityObjct.gender;
                genderRiskValue = entityObjct.genderRiskValue;
                maritalStatus = entityObjct.maritalStatus;
                maritalStatusRiskValue = entityObjct.maritalStatusRiskValue;
                employmentStatus = entityObjct.employmentStatus;
                employmentStatusRiskValue = entityObjct.employmentStatusRiskValue;
                claimRate = entityObjct.claimRate;
                claimRiskValue = entityObjct.claimRiskValue;
                RiskCategory = entityObjct.RiskCategory;
                OverallRiskValue = entityObjct.OverallRiskValue;
                Consumer_ID = entityObjct.Consumer_ID;
            }
        }


        public class DTOcontactlist
        {
            public int idcontactlist { get; set; }
            public Nullable<int> UserID { get; set; }
            public Nullable<int> ContactsUserID { get; set; }

            public DTOcontactlist() { }

            public DTOcontactlist(contactlist entityObjct)
            {
                idcontactlist = entityObjct.idcontactlist;
                UserID = entityObjct.UserID;
                ContactsUserID = entityObjct.ContactsUserID;
            }
        }


        public class DTOcurrentmonthdailysale
        {
            public int ActiveProductItems_ID { get; set; }
            public Nullable<System.DateTime> activeProductItemStartDate { get; set; }
            public Nullable<decimal> sales { get; set; }

            public DTOcurrentmonthdailysale() { }

            public DTOcurrentmonthdailysale(currentmonthdailysale entityObjct)
            {
                ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
                activeProductItemStartDate = entityObjct.activeProductItemStartDate;
                sales = entityObjct.sales;
            }
        }


        public class DTOdemographicconsumerproductlocationlastmonthsale
        {
            public int ActiveProductItems_ID { get; set; }
            public string datum { get; set; }
            public Nullable<int> transactionLocation { get; set; }
            public long numConsumers { get; set; }
            public string gender { get; set; }
            public string maritalStatus { get; set; }
            public int Product_ID { get; set; }
            public long numMartialStatus { get; set; }
            public string employmentStatus { get; set; }
            public long numEmploymentStatus { get; set; }
            public Nullable<decimal> netIncome { get; set; }
            public Nullable<decimal> NumDependants { get; set; }
            public Nullable<decimal> sales { get; set; }

            public DTOdemographicconsumerproductlocationlastmonthsale() { }

            public DTOdemographicconsumerproductlocationlastmonthsale(demographicconsumerproductlocationlastmonthsale entityObjct)
            {
                ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
                datum = entityObjct.datum;
                transactionLocation = entityObjct.transactionLocation;
                numConsumers = entityObjct.numConsumers;
                gender = entityObjct.gender;
                maritalStatus = entityObjct.maritalStatus;
                Product_ID = entityObjct.Product_ID;
                numMartialStatus = entityObjct.numMartialStatus;
                employmentStatus = entityObjct.employmentStatus;
                numEmploymentStatus = entityObjct.numEmploymentStatus;
                netIncome = entityObjct.netIncome;
                NumDependants = entityObjct.NumDependants;
                sales = entityObjct.sales;
            }
        }


        public class DTOdemographicconsumerproductlocationmonthlysale
        {
            public int ActiveProductItems_ID { get; set; }
            public string datum { get; set; }
            public Nullable<int> transactionLocation { get; set; }
            public long numConsumers { get; set; }
            public string gender { get; set; }
            public string maritalStatus { get; set; }
            public int Product_ID { get; set; }
            public long numMartialStatus { get; set; }
            public string employmentStatus { get; set; }
            public long numEmploymentStatus { get; set; }
            public Nullable<decimal> netIncome { get; set; }
            public Nullable<decimal> NumDependants { get; set; }
            public Nullable<decimal> sales { get; set; }

            public DTOdemographicconsumerproductlocationmonthlysale() { }

            public DTOdemographicconsumerproductlocationmonthlysale(demographicconsumerproductlocationmonthlysale entityObjct)
            {
                ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
                datum = entityObjct.datum;
                transactionLocation = entityObjct.transactionLocation;
                numConsumers = entityObjct.numConsumers;
                gender = entityObjct.gender;
                maritalStatus = entityObjct.maritalStatus;
                Product_ID = entityObjct.Product_ID;
                numMartialStatus = entityObjct.numMartialStatus;
                employmentStatus = entityObjct.employmentStatus;
                numEmploymentStatus = entityObjct.numEmploymentStatus;
                netIncome = entityObjct.netIncome;
                NumDependants = entityObjct.NumDependants;
                sales = entityObjct.sales;
            }
        }


        public class DTOdemographicconsumerproductlocationsaleslastmonth
        {
            public int ActiveProductItems_ID { get; set; }
            public string datum { get; set; }
            public Nullable<int> transactionLocation { get; set; }
            public long numConsumers { get; set; }
            public string gender { get; set; }
            public string maritalStatus { get; set; }
            public int Product_ID { get; set; }
            public long numMartialStatus { get; set; }
            public string employmentStatus { get; set; }
            public long numEmploymentStatus { get; set; }
            public Nullable<decimal> netIncome { get; set; }
            public Nullable<decimal> NumDependants { get; set; }
            public Nullable<decimal> sales { get; set; }

            public DTOdemographicconsumerproductlocationsaleslastmonth() { }

            public DTOdemographicconsumerproductlocationsaleslastmonth(demographicconsumerproductlocationsaleslastmonth entityObjct)
            {
                ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
                datum = entityObjct.datum;
                transactionLocation = entityObjct.transactionLocation;
                numConsumers = entityObjct.numConsumers;
                gender = entityObjct.gender;
                maritalStatus = entityObjct.maritalStatus;
                Product_ID = entityObjct.Product_ID;
                numMartialStatus = entityObjct.numMartialStatus;
                employmentStatus = entityObjct.employmentStatus;
                numEmploymentStatus = entityObjct.numEmploymentStatus;
                netIncome = entityObjct.netIncome;
                NumDependants = entityObjct.NumDependants;
                sales = entityObjct.sales;
            }
        }


        public class DTOdemographicriskvalue
        {
            public int DRV_ID { get; set; }
            public string type { get; set; }
            public Nullable<decimal> value { get; set; }
            public Nullable<int> riskvalue { get; set; }
            public Nullable<decimal> riskvalues { get; set; }

            public DTOdemographicriskvalue() { }

            public DTOdemographicriskvalue(demographicriskvalue entityObjct)
            {
                DRV_ID = entityObjct.DRV_ID;
                type = entityObjct.type;
                value = entityObjct.value;
                riskvalue = entityObjct.riskvalue;
                riskvalues = entityObjct.riskvalues;
            }
        }


        public class DTOdocumentspecification
        {
            public int document_ID { get; set; }
            public int Product_ID { get; set; }
            public string documentName { get; set; }
            public string documentDescription { get; set; }
            public string docPreferredFormat { get; set; }
            public string docPreparationRequired { get; set; }

            public DTOdocumentspecification() { }

            public DTOdocumentspecification(documentspecification entityObjct)
            {
                document_ID = entityObjct.document_ID;
                Product_ID = entityObjct.Product_ID;
                documentName = entityObjct.documentName;
                documentDescription = entityObjct.documentDescription;
                docPreferredFormat = entityObjct.docPreferredFormat;
                docPreparationRequired = entityObjct.docPreparationRequired;
            }
        }


        public class DTOindividualconsumerprofilerrawdata
        {
            public int Consumer_ID { get; set; }
            public int User_ID { get; set; }
            public string gender { get; set; }
            public string maritalStatus { get; set; }
            public string employmentStatus { get; set; }
            public long numPurchases { get; set; }
            public Nullable<int> age { get; set; }
            public Nullable<int> numClaims { get; set; }
            public Nullable<decimal> ratio { get; set; }

            public DTOindividualconsumerprofilerrawdata() { }

            public DTOindividualconsumerprofilerrawdata(individualconsumerprofilerrawdata entityObjct)
            {
                Consumer_ID = entityObjct.Consumer_ID;
                User_ID = entityObjct.User_ID;
                gender = entityObjct.gender;
                maritalStatus = entityObjct.maritalStatus;
                employmentStatus = entityObjct.employmentStatus;
                numPurchases = entityObjct.numPurchases;
                age = entityObjct.age;
                numClaims = entityObjct.numClaims;
                ratio = entityObjct.ratio;
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


        public class DTOinsuranceproducttypemonthlysale
        {
            public int ActiveProductItems_ID { get; set; }
            public Nullable<System.DateTime> monthDate { get; set; }
            public int InsuranceType_ID { get; set; }
            public string insuranctTypeDescription { get; set; }
            public string datum { get; set; }
            public Nullable<decimal> sales { get; set; }

            public DTOinsuranceproducttypemonthlysale() { }

            public DTOinsuranceproducttypemonthlysale(insuranceproducttypemonthlysale entityObjct)
            {
                ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
                monthDate = entityObjct.monthDate;
                InsuranceType_ID = entityObjct.InsuranceType_ID;
                insuranctTypeDescription = entityObjct.insuranctTypeDescription;
                datum = entityObjct.datum;
                sales = entityObjct.sales;
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


        public class DTOlastmonthinsurancetypesale
        {
            public int ActiveProductItems_ID { get; set; }
            public int InsuranceType_ID { get; set; }
            public Nullable<decimal> monthSales { get; set; }

            public DTOlastmonthinsurancetypesale() { }

            public DTOlastmonthinsurancetypesale(lastmonthinsurancetypesale entityObjct)
            {
                ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
                InsuranceType_ID = entityObjct.InsuranceType_ID;
                monthSales = entityObjct.monthSales;
            }
        }


        public class DTOlastmonthprovincesale
        {
            public int ActiveProductItems_ID { get; set; }
            public string datum { get; set; }
            public int ProductProvider_ID { get; set; }
            public string Province { get; set; }
            public string LatLng { get; set; }
            public Nullable<decimal> sales { get; set; }

            public DTOlastmonthprovincesale() { }

            public DTOlastmonthprovincesale(lastmonthprovincesale entityObjct)
            {
                ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
                datum = entityObjct.datum;
                ProductProvider_ID = entityObjct.ProductProvider_ID;
                Province = entityObjct.Province;
                LatLng = entityObjct.LatLng;
                sales = entityObjct.sales;
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


        public class DTOmonlthlocationsalessum
        {
            public Nullable<System.DateTime> purchaseDate { get; set; }
            public string datum { get; set; }
            public int ProductProvider_ID { get; set; }
            public int Location_ID { get; set; }
            public string Province { get; set; }
            public string city { get; set; }
            public string LatLng { get; set; }
            public Nullable<decimal> sales { get; set; }

            public DTOmonlthlocationsalessum() { }

            public DTOmonlthlocationsalessum(monlthlocationsalessum entityObjct)
            {
                purchaseDate = entityObjct.purchaseDate;
                datum = entityObjct.datum;
                ProductProvider_ID = entityObjct.ProductProvider_ID;
                Location_ID = entityObjct.Location_ID;
                Province = entityObjct.Province;
                city = entityObjct.city;
                LatLng = entityObjct.LatLng;
                sales = entityObjct.sales;
            }
        }


        public class DTOmonthlylocationsale
        {
            public int ActiveProductItems_ID { get; set; }
            public string dateM { get; set; }
            public Nullable<System.DateTime> datum { get; set; }
            public int Product_ID { get; set; }
            public string productName { get; set; }
            public Nullable<int> transactionLocation { get; set; }
            public Nullable<decimal> sales { get; set; }

            public DTOmonthlylocationsale() { }

            public DTOmonthlylocationsale(monthlylocationsale entityObjct)
            {
                ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
                dateM = entityObjct.dateM;
                datum = entityObjct.datum;
                Product_ID = entityObjct.Product_ID;
                productName = entityObjct.productName;
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
            public string productName { get; set; }
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
                productName = entityObjct.productName;
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


        public class DTOmonthlyProvinceSales_Result
        {

            public DTOmonthlyProvinceSales_Result() { }

            public DTOmonthlyProvinceSales_Result(monthlyProvinceSales_Result entityObjct)
            {
            }
        }


        public class DTOmonthlyprovincialproducttypedistribution
        {
            public int ActiveProductItems_ID { get; set; }
            public Nullable<System.DateTime> activeProductItemEndDate { get; set; }
            public int InsuranceType_ID { get; set; }
            public string Province { get; set; }
            public string insuranctTypeDescription { get; set; }
            public Nullable<decimal> sales { get; set; }

            public DTOmonthlyprovincialproducttypedistribution() { }

            public DTOmonthlyprovincialproducttypedistribution(monthlyprovincialproducttypedistribution entityObjct)
            {
                ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
                activeProductItemEndDate = entityObjct.activeProductItemEndDate;
                InsuranceType_ID = entityObjct.InsuranceType_ID;
                Province = entityObjct.Province;
                insuranctTypeDescription = entityObjct.insuranctTypeDescription;
                sales = entityObjct.sales;
            }
        }


        public class DTOnanofinEntities
        {

            public DTOnanofinEntities() { }

            public DTOnanofinEntities(nanofinEntities entityObjct)
            {
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


        public class DTOotpview
        {
            public int User_ID { get; set; }
            public string otpCode { get; set; }
            public Nullable<int> otpRetryCount { get; set; }
            public Nullable<System.DateTime> otpExpirationTime { get; set; }
            public Nullable<System.DateTime> otpNextAllowedTime { get; set; }
            public Nullable<System.DateTime> otpRecordCreated { get; set; }

            public DTOotpview() { }

            public DTOotpview(otpview entityObjct)
            {
                User_ID = entityObjct.User_ID;
                otpCode = entityObjct.otpCode;
                otpRetryCount = entityObjct.otpRetryCount;
                otpExpirationTime = entityObjct.otpExpirationTime;
                otpNextAllowedTime = entityObjct.otpNextAllowedTime;
                otpRecordCreated = entityObjct.otpRecordCreated;
            }
        }


        public class DTOoverallproductlocationsale
        {
            public int ActiveProductItems_ID { get; set; }
            public Nullable<int> transactionLocation { get; set; }
            public long numConsumers { get; set; }
            public string gender { get; set; }
            public string maritalStatus { get; set; }
            public int Product_ID { get; set; }
            public long numMartialStatus { get; set; }
            public string employmentStatus { get; set; }
            public long numEmploymentStatus { get; set; }
            public Nullable<decimal> netIncome { get; set; }
            public Nullable<decimal> NumDependants { get; set; }
            public Nullable<decimal> sales { get; set; }

            public DTOoverallproductlocationsale() { }

            public DTOoverallproductlocationsale(overallproductlocationsale entityObjct)
            {
                ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
                transactionLocation = entityObjct.transactionLocation;
                numConsumers = entityObjct.numConsumers;
                gender = entityObjct.gender;
                maritalStatus = entityObjct.maritalStatus;
                Product_ID = entityObjct.Product_ID;
                numMartialStatus = entityObjct.numMartialStatus;
                employmentStatus = entityObjct.employmentStatus;
                numEmploymentStatus = entityObjct.numEmploymentStatus;
                netIncome = entityObjct.netIncome;
                NumDependants = entityObjct.NumDependants;
                sales = entityObjct.sales;
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


        public class DTOproductlocationmonthlysale
        {
            public int ActiveProductItems_ID { get; set; }
            public string date_format_activeProductItemStartDate___Y__b__ { get; set; }
            public Nullable<int> transactionLocation { get; set; }
            public string productName { get; set; }
            public int Product_ID { get; set; }
            public Nullable<decimal> sales { get; set; }

            public DTOproductlocationmonthlysale() { }

            public DTOproductlocationmonthlysale(productlocationmonthlysale entityObjct)
            {
                ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
                date_format_activeProductItemStartDate___Y__b__ = entityObjct.date_format_activeProductItemStartDate___Y__b__;
                transactionLocation = entityObjct.transactionLocation;
                productName = entityObjct.productName;
                Product_ID = entityObjct.Product_ID;
                sales = entityObjct.sales;
            }
        }


        public class DTOProductLocationSales_Result
        {

            public DTOProductLocationSales_Result() { }

            public DTOProductLocationSales_Result(ProductLocationSales_Result entityObjct)
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


        public class DTOproductprovideryearlysale
        {
            public int ActiveProductItems_ID { get; set; }
            public int ProductProvider_ID { get; set; }
            public Nullable<System.DateTime> activeProductItemStartDate { get; set; }
            public Nullable<decimal> yearSales { get; set; }

            public DTOproductprovideryearlysale() { }

            public DTOproductprovideryearlysale(productprovideryearlysale entityObjct)
            {
                ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
                ProductProvider_ID = entityObjct.ProductProvider_ID;
                activeProductItemStartDate = entityObjct.activeProductItemStartDate;
                yearSales = entityObjct.yearSales;
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


        public class DTOproductswithpurchas
        {
            public int Product_ID { get; set; }
            public string name { get; set; }
            public int InsuranceType_ID { get; set; }
            public string insuranctTypeDescription { get; set; }

            public DTOproductswithpurchas() { }

            public DTOproductswithpurchas(productswithpurchas entityObjct)
            {
                Product_ID = entityObjct.Product_ID;
                name = entityObjct.name;
                InsuranceType_ID = entityObjct.InsuranceType_ID;
                insuranctTypeDescription = entityObjct.insuranctTypeDescription;
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


        public class DTOprovincialinsurancetypesale
        {
            public int ActiveProductItems_ID { get; set; }
            public string datum { get; set; }
            public int InsuranceType_ID { get; set; }
            public string insuranctTypeDescription { get; set; }
            public string Province { get; set; }
            public Nullable<decimal> sales { get; set; }

            public DTOprovincialinsurancetypesale() { }

            public DTOprovincialinsurancetypesale(provincialinsurancetypesale entityObjct)
            {
                ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
                datum = entityObjct.datum;
                InsuranceType_ID = entityObjct.InsuranceType_ID;
                insuranctTypeDescription = entityObjct.insuranctTypeDescription;
                Province = entityObjct.Province;
                sales = entityObjct.sales;
            }
        }


        public class DTOprovincialproducttypedistributionlastmonth
        {
            public int ActiveProductItems_ID { get; set; }
            public Nullable<System.DateTime> activeProductItemEndDate { get; set; }
            public int InsuranceType_ID { get; set; }
            public string Province { get; set; }
            public string insuranctTypeDescription { get; set; }
            public Nullable<decimal> sales { get; set; }

            public DTOprovincialproducttypedistributionlastmonth() { }

            public DTOprovincialproducttypedistributionlastmonth(provincialproducttypedistributionlastmonth entityObjct)
            {
                ActiveProductItems_ID = entityObjct.ActiveProductItems_ID;
                activeProductItemEndDate = entityObjct.activeProductItemEndDate;
                InsuranceType_ID = entityObjct.InsuranceType_ID;
                Province = entityObjct.Province;
                insuranctTypeDescription = entityObjct.insuranctTypeDescription;
                sales = entityObjct.sales;
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
            public string location { get; set; }
            public Nullable<bool> isLocationAvailable { get; set; }

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
                location = entityObjct.location;
                isLocationAvailable = entityObjct.isLocationAvailable;
            }
        }


        public class DTOresellersalespermonth
        {
            public int VoucherSentTo { get; set; }
            public System.DateTime transactionDate { get; set; }
            public int Sender_ID { get; set; }
            public Nullable<decimal> sold { get; set; }

            public DTOresellersalespermonth() { }

            public DTOresellersalespermonth(resellersalespermonth entityObjct)
            {
                VoucherSentTo = entityObjct.VoucherSentTo;
                transactionDate = entityObjct.transactionDate;
                Sender_ID = entityObjct.Sender_ID;
                sold = entityObjct.sold;
            }
        }


        public class DTOresellersendmonthlysale
        {
            public int VoucherSentTo { get; set; }
            public int Sender_ID { get; set; }
            public string date_format_vouchertransaction_transactionDate___Y__b__ { get; set; }
            public Nullable<decimal> sales { get; set; }

            public DTOresellersendmonthlysale() { }

            public DTOresellersendmonthlysale(resellersendmonthlysale entityObjct)
            {
                VoucherSentTo = entityObjct.VoucherSentTo;
                Sender_ID = entityObjct.Sender_ID;
                date_format_vouchertransaction_transactionDate___Y__b__ = entityObjct.date_format_vouchertransaction_transactionDate___Y__b__;
                sales = entityObjct.sales;
            }
        }


        public class DTOresellersendvouchergenderspecific
        {
            public int VoucherSentTo { get; set; }
            public string date_format_vouchertransaction_transactionDate___Y__b__ { get; set; }
            public string gender { get; set; }
            public Nullable<decimal> sales { get; set; }

            public DTOresellersendvouchergenderspecific() { }

            public DTOresellersendvouchergenderspecific(resellersendvouchergenderspecific entityObjct)
            {
                VoucherSentTo = entityObjct.VoucherSentTo;
                date_format_vouchertransaction_transactionDate___Y__b__ = entityObjct.date_format_vouchertransaction_transactionDate___Y__b__;
                gender = entityObjct.gender;
                sales = entityObjct.sales;
            }
        }


        public class DTOrisk_agegroup
        {
            public int ageGroup_ID { get; set; }
            public string description { get; set; }
            public Nullable<int> lowest { get; set; }
            public Nullable<int> highest { get; set; }

            public DTOrisk_agegroup() { }

            public DTOrisk_agegroup(risk_agegroup entityObjct)
            {
                ageGroup_ID = entityObjct.ageGroup_ID;
                description = entityObjct.description;
                lowest = entityObjct.lowest;
                highest = entityObjct.highest;
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
            public string otpCode { get; set; }
            public Nullable<int> otpRetryCount { get; set; }
            public Nullable<System.DateTime> otpExpirationTime { get; set; }
            public Nullable<System.DateTime> otpNextAllowedTime { get; set; }
            public Nullable<System.DateTime> otpRecordCreated { get; set; }

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
                otpCode = entityObjct.otpCode;
                otpRetryCount = entityObjct.otpRetryCount;
                otpExpirationTime = entityObjct.otpExpirationTime;
                otpNextAllowedTime = entityObjct.otpNextAllowedTime;
                otpRecordCreated = entityObjct.otpRecordCreated;
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