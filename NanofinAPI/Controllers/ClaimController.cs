using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NanofinAPI.Models;
using NanofinAPI.Models.DTOEnvironment;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Data.Entity;

namespace NanofinAPI.Controllers
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


        // POST: api/claim
        [HttpPost]
        [ResponseType(typeof(DTOclaim))]
        public async Task<DTOclaim> Postclaim(DTOclaim newDTO)
        {

            claim newProd = EntityMapper.updateEntity(null, newDTO);
            db.claims.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }

        [HttpGet]
   
        public int GetClaimID(int activeProductID)
        {
          
            claim cl = (from c in db.claims where c.ActiveProductItems_ID==activeProductID select c).SingleOrDefault();
            return cl.Claim_ID;
            //DTOclaim toReturn = new DTOclaim(cl);
            //return toReturn;
        }


        [HttpPut]
        public async Task<IHttpActionResult> setIsActiveToFalse(int activeProductID)
        {
            activeproductitem toUpdate = (from c in db.activeproductitems where c.ActiveProductItems_ID == activeProductID select c).SingleOrDefault();
            DTOactiveproductitem dtoAct = new DTOactiveproductitem(toUpdate);
            dtoAct.isActive = false;
            toUpdate = EntityMapper.updateEntity(toUpdate,dtoAct);
            db.Entry(toUpdate).State = EntityState.Modified;
           await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }


        [HttpPut]
        public async Task<IHttpActionResult> setIsActiveToTrue(int activeProductID)
        {
            activeproductitem toUpdate = (from c in db.activeproductitems where c.ActiveProductItems_ID == activeProductID select c).SingleOrDefault();
            DTOactiveproductitem dtoAct = new DTOactiveproductitem(toUpdate);
            dtoAct.isActive = true;
            toUpdate = EntityMapper.updateEntity(toUpdate, dtoAct);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }


        //claim docs path:  "/UploadFiles/IM_id/productName/year/month/consumerUserID/activeProd_ID;
        [HttpPost]
        [ResponseType(typeof(DTOclaimuploaddocument))]
        public async Task<DTOclaimuploaddocument> Postclaimuploaddocument(int userID, int activeProductItemsID, string claimUploadDocPath, int claimID)
        {
           
            claimuploaddocument newProd = createClaimUploadDocumentEntity(userID, activeProductItemsID, claimUploadDocPath, claimID);
            DTOclaimuploaddocument newDTO = new DTOclaimuploaddocument(newProd);
            db.claimuploaddocuments.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO ;
        }


        private claimuploaddocument createClaimUploadDocumentEntity(int userID, int activeProductItemsID,string claimUploadDocPath,int claimID)
        {

            claimuploaddocument doc = new claimuploaddocument();
            doc.User_ID = userID;
            doc.ActiveProductItems_ID = activeProductItemsID;
            doc.claimUploadDocumentPath = claimUploadDocPath;
            doc.Claim_ID = claimID;
           
          
            return doc;
        }

        public int getCustomerID(int UserID)
        {
            consumer cons = (from c in db.consumers where c.User_ID == UserID select c).SingleOrDefault();
            int consumerId = cons.Consumer_ID;
            return consumerId;
        }

        [HttpPut]
        public async Task<IHttpActionResult> incrementConsumerNumClaims(int consumerID)
        {

            consumer toUpdate = (from c in db.consumers where c.Consumer_ID == consumerID select c).SingleOrDefault();
            DTOconsumer dtoCons = new DTOconsumer(toUpdate);
            dtoCons.numClaims = dtoCons.numClaims + 1;
            toUpdate = EntityMapper.updateEntity(toUpdate,dtoCons);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        #region Insurance Manager Claim Side Code

     
        [HttpGet]
        //Get list of all claim templates and info
        public List<DTOclaimtemplate> Getclaimtemplates()
        {
            List<DTOclaimtemplate> toReturn = new List<DTOclaimtemplate>();
            List<claimtemplate> list = (from c in db.claimtemplates select c).ToList();

            foreach (claimtemplate p in list)
            {
                toReturn.Add(new DTOclaimtemplate(p));
            }

            return toReturn;
        }

        [HttpGet]
        //View a specific claim for an active product
        public DTOclaim getClaim(int activeProductItem_ID)
        {
            claim cl = (from c in db.claims where c.ActiveProductItems_ID == activeProductItem_ID select c).SingleOrDefault();
            DTOclaim dtoClaim = new DTOclaim(cl);
            return dtoClaim;

        }

        //View all claims for this consumerID = ConsumerClaimHistory
        public List<DTOclaim> getCustomerClaims(int CustomerID)
        {
            List<claim> list = (from c in db.claims where c.Consumer_ID == CustomerID select c).ToList();

            List<DTOclaim> toReturn = new List<DTOclaim>();

            foreach (claim cl in list)
            {
                toReturn.Add(new DTOclaim(cl));
            }

            return toReturn;
        }

        //GetClaimFormCapturedData for this specific claim:
        public string getClaimCapturedData(int ClaimID)
        {
            claim cl = (from c in db.claims where c.Claim_ID == ClaimID select c).SingleOrDefault();
            return cl.capturedClaimFormDataJson;

        }


        //GetClaimDocuments


        #endregion


    }
}
