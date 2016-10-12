using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NanofinAPI.Models;

namespace NanofinAPI.Models.DTOEnvironment
{
    public class DTOInsuranceProductTransfer
    {
        public int ID { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public decimal coverAmount { get; set; }
        public decimal dailyCost { get; set; }
        public String typeOfInsurance { get; set; }
    }

    public class ViewApplication
    {
        public int activeProductID;
        public string customerIDNo;
        public string customerName;
        public string address;
        public string InsurnceName;
        public string startdate;

        public ViewApplication(activeproductitem prod)
        {
            this.activeProductID = prod.ActiveProductItems_ID;
            this.customerIDNo = prod.consumer.user.IDnumber;
            this.customerName = prod.consumer.user.userFirstName + " " + prod.consumer.user.userLastName;
            this.address = prod.consumer.consumerAddress;
            this.startdate = prod.activeProductItemStartDate.ToString(); 
            this.InsurnceName = prod.product.productName;
        }
    }

    public class ProcessedInsuranceProd
    {
        public int providerID { get; set; }
        public int activeproductID { get; set; }
        public string policyNo { get; set; }

        public ProcessedInsuranceProd(int providerID, int activeproductID, string policyNo)
        {
            this.providerID = providerID;
            this.activeproductID = activeproductID;
            this.policyNo = policyNo;
        }

    }


    public class InsuranceManagerViewProducts
    {
        public int id { get; set; }
        public string name { get; set; }
        public String typeOfInsurance { get; set; }
        public String icon { get; set; }
        public String boxColor { get; set; }
        public int typeID;

        public InsuranceManagerViewProducts(insuranceproduct prod)
        {
            id = prod.Product_ID;
            name = prod.product.productName;
            typeOfInsurance = prod.insurancetype.insuranctTypeDescription;
            icon = IDtoIcon(prod.insurancetype.InsuranceType_ID);
        }

        public String IDtoIcon(int insuranceTyp)
        {
            String toreturn = "";

            typeID = insuranceTyp;

            switch (insuranceTyp)
            {

                case 1:
                    {
                        toreturn = "fa fa fa-home";
                        boxColor = "small-box bg-aqua";

                        break;
                    }

                case 2:
                    {
                        toreturn = "fa fa-briefcase";
                        boxColor = "small-box bg-green";
                        break;
                    }

                case 11:
                    {
                        toreturn = "fa fa-legal";
                        boxColor = "small-box bg-yellow";
                        break;
                    }

                case 21:
                    {
                        toreturn = "fa fa-ambulance";
                        boxColor = "small-box bg-red";
                        break;
                    }

                case 31:
                    {
                        toreturn = "fa fa-heartbeat";
                        boxColor = "small-box bg-purple-gradient";
                        break;
                    }

                case 41:
                    {
                        toreturn = "fa fa-plus";
                        boxColor = "small-box bg-teal-active";
                        break;
                    }
            }

            return toreturn;
        }


    }

    public class InsuranceProviderSession
    {
        public int userID { get; set; }
        public int userType { get; set; }
        public int providerID { get; set; }
        public string userName { get; set; }
    }

    public class ViewUnprocessedApplications
    {
        public List<ViewApplication> unprocessed { get; set;}
        public string lastestPolicyNo { get; set;}
        
        public ViewUnprocessedApplications(List<activeproductitem> list)
        {
            unprocessed = new List<ViewApplication>();

            foreach (activeproductitem temp in list)
            {
                unprocessed.Add(new ViewApplication(temp));
            }
            if (list.Count >0)
            lastestPolicyNo = list.Last().activeProductItemPolicyNum;
        }

    }

    public class dtoViewClaimApplication
    {
        public int claimID;
        public Nullable<DateTime> claimDate;
        public string consumerName;
        public string consumerSurname;
        public Nullable<int> activeProductID;
        public string productName;
        public string capturedClaimFormData;
       

      

        public dtoViewClaimApplication(claim cl)
        {
            this.claimID = cl.Claim_ID;
            this.claimDate = cl.claimDate;
            this.consumerName = cl.consumer.user.userFirstName;
            this.consumerSurname = cl.consumer.user.userLastName;
            this.activeProductID = cl.ActiveProductItems_ID;
            this.productName = cl.activeproductitem.product.productName;
            this.capturedClaimFormData = cl.capturedClaimFormDataJson;
           
        }
    }



}
