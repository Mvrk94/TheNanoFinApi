using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;
using TheNanoFinAPI.Models;
using TheNanoFinAPI.Models.DTOEnvironment;

namespace NanoFinAPI.Controllers
{
    public class insuranceManagerController : productsController
    {
        private nanofinEntities db = new nanofinEntities();

        // GET: api/insuranceManager
        public InsuranceProviderSession Session(int ProductProviderID)
        {
            if (ProductProviderID == -1) ProductProviderID = 11;

            InsuranceProviderSession toReturn = new InsuranceProviderSession();
            productprovider pd = db.productproviders.Find(ProductProviderID);

            toReturn.userID = pd.User_ID;
            toReturn.userType = int.Parse(pd.user.userType.ToString());
            toReturn.providerID = pd.ProductProvider_ID;
            toReturn.userName = pd.user.userFirstName.ToString();

            return toReturn;
        }

        // GET: api/insuranceManager
        public List<InsuranceManagerViewProducts> Getinsuranceproducts(int ProductProviderID)
        {
            List<InsuranceManagerViewProducts> toReturn = new List<InsuranceManagerViewProducts>();
            List<insuranceproduct> list = (from c in db.insuranceproducts where c.ProductProvider_ID == ProductProviderID select c).ToList();

            foreach (insuranceproduct p in list)
            {
                toReturn.Add(new InsuranceManagerViewProducts(p));
            }

            return toReturn;
        }
        [HttpGet]
        [ResponseType(typeof(Boolean))]
        public Boolean isPolicyNumberUnique(String policyNo)
        {
            return db.activeproductitems.Count(c => c.activeProductItemPolicyNum == policyNo) > 0 ? false : true;
        }

        // GET: api/insuranceManager/5
        [ResponseType(typeof(ViewUnprocessedApplications))]
        public ViewUnprocessedApplications getUnprocessedApplications(int ProductProviderID)
        {
            

            List<activeproductitem> activeProd =  (from c  in db.activeproductitems where c.product.ProductProvider_ID == ProductProviderID && c.activeProductItemPolicyNum =="" && c.isActive == true select c).ToList();
            ViewUnprocessedApplications toreturn = new ViewUnprocessedApplications(activeProd);

            return toreturn;
        }

        [ResponseType(typeof(Boolean))]
        public async Task<IHttpActionResult> ProcessInsuranceProduct(ProcessedInsuranceProd prod)
        {
            activeproductitem temp =await db.activeproductitems.SingleAsync(c => c.product.ProductProvider_ID == prod.providerID && c.ActiveProductItems_ID == prod.activeproductID);
            Boolean toreturn = false;

            if (temp != null)
            {
                temp.isActive = true;
                temp.activeProductItemPolicyNum = prod.policyNo;
                await db.SaveChangesAsync();
                toreturn = true;
            }
            return Ok(toreturn);
        }  



        // GET: api/insuranceManager/5
        [ResponseType(typeof(DTOinsuranceproduct))]
        public async Task<IHttpActionResult> Getinsuranceproduct(int ProductProviderID, int InsuranceProduct_ID)
        {
            insuranceproduct prod = await db.insuranceproducts.SingleAsync(c => c.ProductProvider_ID == ProductProviderID && c.InsuranceProduct_ID == InsuranceProduct_ID);
            DTOinsuranceproduct insuranceproduct = new DTOinsuranceproduct(prod);

            if (insuranceproduct == null)
            {
                return NotFound();
            }

            return Ok(insuranceproduct);
        }

        // PUT: api/insuranceManager/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putinsuranceproduct(int InsuranceProduct_ID, DTOinsuranceproduct insuranceproduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (InsuranceProduct_ID != insuranceproduct.InsuranceProduct_ID)
            {
                return BadRequest();
            }

            insuranceproduct toUpdate = db.insuranceproducts.Single(c => c.InsuranceProduct_ID == InsuranceProduct_ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, insuranceproduct);
            db.Entry(toUpdate).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!insuranceproductExists(InsuranceProduct_ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/insuranceManager
        [ResponseType(typeof(DTOinsuranceproduct))]
        public async Task<IHttpActionResult> Postinsuranceproduct(DTOinsuranceproduct insuranceproduct)
        {
            //insure json conversion is correct
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            insuranceproduct newProd = EntityMapper.updateEntity(null, insuranceproduct);
            db.insuranceproducts.Add(newProd);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = newProd.InsuranceProduct_ID }, insuranceproduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool insuranceproductExists(int id)
        {
            return db.insuranceproducts.Count(e => e.InsuranceProduct_ID == id) > 0;
        }
    }
}