﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheNanoFinAPI.Models;
using TheNanoFinAPI.Models.DTOEnvironment;

namespace TheNanoFinAPI.Controllers
{
    public class ClaimController : ApiController
    {

        database_nanofinEntities db = new database_nanofinEntities();
        //get claim template object for a specific productID
        public DTOclaimtemplate getClaimTemplateForProduct(int productID)
        {
            DTOclaimtemplate toReturn=null;
            int templateID = getClaimTemplateID(productID);
            if(templateID!=0)
            {
                claimtemplate entityTemplate = (from t in db.claimtemplates where t.claimtemplate_ID == templateID select t).SingleOrDefault();
                toReturn = new DTOclaimtemplate(entityTemplate);
            }
           
            return toReturn;
        }

        //Helper method for getClaimTemplateFromProductID
        public int getClaimTemplateID(int productID)
        { //get the claimTemplateID for the specified product.
            int templateID = 0;

            insuranceproduct insProd = (from i in db.insuranceproducts where i.Product_ID == productID select i).SingleOrDefault();
            if(insProd.claimtemplate_ID!=null)
            {
                templateID = insProd.claimtemplate_ID.Value;
            }
           
            return templateID;
        }

        public string getClaimTemplateJustJson(int productID)
        {
            

            DTOclaimtemplate claimTemplate = getClaimTemplateForProduct(productID);

            if (claimTemplate != null)
            {
                return claimTemplate.formDataRequiredJson;
            }
            else
            {
                return "No template available";
            }
            



        }




    }
}
