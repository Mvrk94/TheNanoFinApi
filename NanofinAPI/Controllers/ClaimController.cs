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

        #region Customer Side Claim Code

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

        [HttpPost]
        public async Task<IHttpActionResult> PostClaimForChris(Nullable<int> Consumer_ID, Nullable<int> ActiveProductItems_ID, string capturedClaimFormDataJson, Nullable<DateTime> claimDate, string claimStatus, string claimPaymentFinalised)
        {
            claim newClaim = new claim();
            newClaim.Consumer_ID = Consumer_ID;
            newClaim.ActiveProductItems_ID = ActiveProductItems_ID;
            newClaim.capturedClaimFormDataJson = capturedClaimFormDataJson;
            newClaim.claimDate = claimDate;
            newClaim.claimStatus = claimStatus;
            newClaim.claimPaymentFinalised = claimPaymentFinalised;
            db.claims.Add(newClaim);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.OK);
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

#endregion

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
        [ResponseType(typeof(DTOclaim))]
        public async Task<IHttpActionResult> getClaim(int activeProductItem_ID)
        {
            claim cl = await (from c in db.claims where c.ActiveProductItems_ID == activeProductItem_ID select c).SingleOrDefaultAsync();
            DTOclaim dtoClaim = new DTOclaim(cl);

            if (dtoClaim == null)
            {
                return NotFound();
            }
            return Ok(dtoClaim);

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


        //View Claims to be processed: "In progess" and Payment "false"
        [HttpGet]
        public List<dtoViewClaimApplication> viewClaimsToBeProcessed()
        {
            List<claim> list = (from c in db.claims where (c.claimStatus == "In Progress" && c.claimPaymentFinalised == "false") || (c.claimStatus == "Accepted" && c.claimPaymentFinalised =="false") select c).ToList();
            List<dtoViewClaimApplication> toReturn = new List<dtoViewClaimApplication>();

            foreach (claim c in list)
            {
                toReturn.Add(new dtoViewClaimApplication(c));
            }
            return toReturn;
        }


        [HttpGet]
        public dtoViewClaimApplication getSingleClaimToBeProcessed(int claimID)
        {
            claim cl = (from c in db.claims where c.Claim_ID == claimID select c).SingleOrDefault();
            dtoViewClaimApplication toRet = new dtoViewClaimApplication(cl);
            return toRet;
        }

        //Get a claim's uploadDocumentPath
        [HttpGet]
        public string getClaimUploadedDocsPath(int ClaimID)
        {
            string docPath = (from d in db.claimuploaddocuments where d.Claim_ID == ClaimID select d.claimUploadDocumentPath).SingleOrDefault();
        
            return docPath;
        }

        //Accept/Reject Claim: Claim status
        [HttpPut]
        public async Task<IHttpActionResult> updateClaimStatus(int ClaimID, string status)
        {
            claim toUpdate = (from c in db.claims where c.Claim_ID == ClaimID select c).SingleOrDefault();
            DTOclaim dtoClaim = new DTOclaim(toUpdate);
            dtoClaim.claimStatus = status;
            toUpdate = EntityMapper.updateEntity(toUpdate, dtoClaim);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.OK);

        }


        //Set that the claim has been payed.
        [HttpPut]
        public async Task<IHttpActionResult> updateClaimPaymentStatus(int ClaimID, string paymentStatus)
        {
            claim toUpdate = (from c in db.claims where c.Claim_ID == ClaimID select c).SingleOrDefault();
            DTOclaim dtoClaim = new DTOclaim(toUpdate);
            dtoClaim.claimPaymentFinalised = paymentStatus;
            toUpdate = EntityMapper.updateEntity(toUpdate, dtoClaim);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.OK);

        }

        //Confirm payment of claim...doc upload



        #endregion



        //View claims that are in progress of being processed by the insurance manager
        [HttpPost]
        public List<DTOclaim> getClaimsInProgress(int ConsumerID)
        {
            List<DTOclaim> toReturn = new List<DTOclaim>();

            List<claim> list = (from c in db.claims where c.Consumer_ID==ConsumerID && c.claimStatus=="In Progress" select c).ToList();
            if (!list.Any())
            {
                return null;
            }
            foreach (claim p in list)
            {
                toReturn.Add(new DTOclaim(p));
            }

            return toReturn;
        }

        //View claims that have been settled in the past, will be with a doc download
        [HttpPost]
        public List<DTOclaim> getClaimsThatHaveBeenSettled(int ConsumerID)
        {
            List<DTOclaim> toReturn = new List<DTOclaim>();

            List<claim> list = (from c in db.claims where c.Consumer_ID == ConsumerID && c.claimStatus == "Accepted" && c.claimPaymentFinalised == "true" select c).ToList();
            if (!list.Any())
            {
                return null;
            }
            foreach (claim p in list)
            {
                toReturn.Add(new DTOclaim(p));
            }

            return toReturn;
        }


    }
}
