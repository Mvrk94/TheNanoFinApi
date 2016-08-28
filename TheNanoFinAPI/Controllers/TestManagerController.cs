using NanofinAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TheNanoFinAPI.Models;
using TheNanoFinAPI.Models.DTOEnvironment;

namespace NanoFinAPI_6_07.Controllers.testManager
{
    public class TestManagerController : ApiController
    {
        nanofinEntities db = new nanofinEntities();

        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOactiveproductitem))]
        public async Task<DTOactiveproductitem> Postactiveproductitem(DTOactiveproductitem newDTO)
        {

            activeproductitem newProd = EntityMapper.updateEntity(null, newDTO);
            db.activeproductitems.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOactiveproductitem> Getactiveproductitem()
        {
            List<DTOactiveproductitem> toReturn = new List<DTOactiveproductitem>();
            List<activeproductitem> list = (from c in db.activeproductitems select c).ToList();

            foreach (activeproductitem p in list)
            {
                toReturn.Add(new DTOactiveproductitem(p));
            }

            return toReturn;
        }


        // PUT: api/activeproductitem/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putactiveproductitem(int ID, DTOactiveproductitem editedDTO)
        {
            activeproductitem toUpdate = db.activeproductitems.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOconsumer))]
        public async Task<DTOconsumer> Postconsumer(DTOconsumer newDTO)
        {

            consumer newProd = EntityMapper.updateEntity(null, newDTO);
            db.consumers.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOconsumer> Getconsumer()
        {
            List<DTOconsumer> toReturn = new List<DTOconsumer>();
            List<consumer> list = (from c in db.consumers select c).ToList();

            foreach (consumer p in list)
            {
                toReturn.Add(new DTOconsumer(p));
            }

            return toReturn;
        }


        // PUT: api/consumer/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putconsumer(int ID, DTOconsumer editedDTO)
        {
            consumer toUpdate = db.consumers.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
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
        // GET: api/testManager
        public List<DTOclaim> Getclaim()
        {
            List<DTOclaim> toReturn = new List<DTOclaim>();
            List<claim> list = (from c in db.claims select c).ToList();

            foreach (claim p in list)
            {
                toReturn.Add(new DTOclaim(p));
            }

            return toReturn;
        }


        // PUT: api/claim/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putclaim(int ID, DTOclaim editedDTO)
        {
            claim toUpdate = db.claims.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOclaimtemplate))]
        public async Task<DTOclaimtemplate> Postclaimtemplate(DTOclaimtemplate newDTO)
        {

            claimtemplate newProd = EntityMapper.updateEntity(null, newDTO);
            db.claimtemplates.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOclaimtemplate> Getclaimtemplate()
        {
            List<DTOclaimtemplate> toReturn = new List<DTOclaimtemplate>();
            List<claimtemplate> list = (from c in db.claimtemplates select c).ToList();

            foreach (claimtemplate p in list)
            {
                toReturn.Add(new DTOclaimtemplate(p));
            }

            return toReturn;
        }


        // PUT: api/claimtemplate/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putclaimtemplate(int ID, DTOclaimtemplate editedDTO)
        {
            claimtemplate toUpdate = db.claimtemplates.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOclaimuploaddocument))]
        public async Task<DTOclaimuploaddocument> Postclaimuploaddocument(DTOclaimuploaddocument newDTO)
        {

            claimuploaddocument newProd = EntityMapper.updateEntity(null, newDTO);
            db.claimuploaddocuments.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOclaimuploaddocument> Getclaimuploaddocument()
        {
            List<DTOclaimuploaddocument> toReturn = new List<DTOclaimuploaddocument>();
            List<claimuploaddocument> list = (from c in db.claimuploaddocuments select c).ToList();

            foreach (claimuploaddocument p in list)
            {
                toReturn.Add(new DTOclaimuploaddocument(p));
            }

            return toReturn;
        }


        // PUT: api/claimuploaddocument/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putclaimuploaddocument(int ID, DTOclaimuploaddocument editedDTO)
        {
            claimuploaddocument toUpdate = db.claimuploaddocuments.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOdocument))]
        public async Task<DTOdocument> Postdocument(DTOdocument newDTO)
        {

            document newProd = EntityMapper.updateEntity(null, newDTO);
            db.documents.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOdocument> Getdocument()
        {
            List<DTOdocument> toReturn = new List<DTOdocument>();
            List<document> list = (from c in db.documents select c).ToList();

            foreach (document p in list)
            {
                toReturn.Add(new DTOdocument(p));
            }

            return toReturn;
        }


        // PUT: api/document/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putdocument(int ID, DTOdocument editedDTO)
        {
            document toUpdate = db.documents.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOinsuranceproduct))]
        public async Task<DTOinsuranceproduct> Postinsuranceproduct(DTOinsuranceproduct newDTO)
        {

            insuranceproduct newProd = EntityMapper.updateEntity(null, newDTO);
            db.insuranceproducts.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOinsuranceproduct> Getinsuranceproduct()
        {
            List<DTOinsuranceproduct> toReturn = new List<DTOinsuranceproduct>();
            List<insuranceproduct> list = (from c in db.insuranceproducts select c).ToList();

            foreach (insuranceproduct p in list)
            {
                toReturn.Add(new DTOinsuranceproduct(p));
            }

            return toReturn;
        }


        // PUT: api/insuranceproduct/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putinsuranceproduct(int ID, DTOinsuranceproduct editedDTO)
        {
            insuranceproduct toUpdate = db.insuranceproducts.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOinsurancetype))]
        public async Task<DTOinsurancetype> Postinsurancetype(DTOinsurancetype newDTO)
        {

            insurancetype newProd = EntityMapper.updateEntity(null, newDTO);
            db.insurancetypes.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOinsurancetype> Getinsurancetype()
        {
            List<DTOinsurancetype> toReturn = new List<DTOinsurancetype>();
            List<insurancetype> list = (from c in db.insurancetypes select c).ToList();

            foreach (insurancetype p in list)
            {
                toReturn.Add(new DTOinsurancetype(p));
            }

            return toReturn;
        }


        // PUT: api/insurancetype/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putinsurancetype(int ID, DTOinsurancetype editedDTO)
        {
            insurancetype toUpdate = db.insurancetypes.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOlocation))]
        public async Task<DTOlocation> Postlocation(DTOlocation newDTO)
        {

            location newProd = EntityMapper.updateEntity(null, newDTO);
            db.locations.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOlocation> Getlocation()
        {
            List<DTOlocation> toReturn = new List<DTOlocation>();
            List<location> list = (from c in db.locations select c).ToList();

            foreach (location p in list)
            {
                toReturn.Add(new DTOlocation(p));
            }

            return toReturn;
        }


        // PUT: api/location/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putlocation(int ID, DTOlocation editedDTO)
        {
            location toUpdate = db.locations.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOnotificationlog))]
        public async Task<DTOnotificationlog> Postnotificationlog(DTOnotificationlog newDTO)
        {

            notificationlog newProd = EntityMapper.updateEntity(null, newDTO);
            db.notificationlogs.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOnotificationlog> Getnotificationlog()
        {
            List<DTOnotificationlog> toReturn = new List<DTOnotificationlog>();
            List<notificationlog> list = (from c in db.notificationlogs select c).ToList();

            foreach (notificationlog p in list)
            {
                toReturn.Add(new DTOnotificationlog(p));
            }

            return toReturn;
        }


        // PUT: api/notificationlog/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putnotificationlog(int ID, DTOnotificationlog editedDTO)
        {
            notificationlog toUpdate = db.notificationlogs.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOproduct))]
        public async Task<DTOproduct> Postproduct(DTOproduct newDTO)
        {

            product newProd = EntityMapper.updateEntity(null, newDTO);
            db.products.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOproduct> Getproduct()
        {
            List<DTOproduct> toReturn = new List<DTOproduct>();
            List<product> list = (from c in db.products select c).ToList();

            foreach (product p in list)
            {
                toReturn.Add(new DTOproduct(p));
            }

            return toReturn;
        }


        // PUT: api/product/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putproduct(int ID, DTOproduct editedDTO)
        {
            product toUpdate = db.products.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOproductprovider))]
        public async Task<DTOproductprovider> Postproductprovider(DTOproductprovider newDTO)
        {

            productprovider newProd = EntityMapper.updateEntity(null, newDTO);
            db.productproviders.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOproductprovider> Getproductprovider()
        {
            List<DTOproductprovider> toReturn = new List<DTOproductprovider>();
            List<productprovider> list = (from c in db.productproviders select c).ToList();

            foreach (productprovider p in list)
            {
                toReturn.Add(new DTOproductprovider(p));
            }

            return toReturn;
        }


        // PUT: api/productprovider/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putproductprovider(int ID, DTOproductprovider editedDTO)
        {
            productprovider toUpdate = db.productproviders.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOproductproviderpayment))]
        public async Task<DTOproductproviderpayment> Postproductproviderpayment(DTOproductproviderpayment newDTO)
        {

            productproviderpayment newProd = EntityMapper.updateEntity(null, newDTO);
            db.productproviderpayments.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOproductproviderpayment> Getproductproviderpayment()
        {
            List<DTOproductproviderpayment> toReturn = new List<DTOproductproviderpayment>();
            List<productproviderpayment> list = (from c in db.productproviderpayments select c).ToList();

            foreach (productproviderpayment p in list)
            {
                toReturn.Add(new DTOproductproviderpayment(p));
            }

            return toReturn;
        }


        // PUT: api/productproviderpayment/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putproductproviderpayment(int ID, DTOproductproviderpayment editedDTO)
        {
            productproviderpayment toUpdate = db.productproviderpayments.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOproductredemptionlog))]
        public async Task<DTOproductredemptionlog> Postproductredemptionlog(DTOproductredemptionlog newDTO)
        {

            productredemptionlog newProd = EntityMapper.updateEntity(null, newDTO);
            db.productredemptionlogs.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOproductredemptionlog> Getproductredemptionlog()
        {
            List<DTOproductredemptionlog> toReturn = new List<DTOproductredemptionlog>();
            List<productredemptionlog> list = (from c in db.productredemptionlogs select c).ToList();

            foreach (productredemptionlog p in list)
            {
                toReturn.Add(new DTOproductredemptionlog(p));
            }

            return toReturn;
        }


        // PUT: api/productredemptionlog/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putproductredemptionlog(int ID, DTOproductredemptionlog editedDTO)
        {
            productredemptionlog toUpdate = db.productredemptionlogs.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOproductsalespermonth))]
        public async Task<DTOproductsalespermonth> Postproductsalespermonth(DTOproductsalespermonth newDTO)
        {

            productsalespermonth newProd = EntityMapper.updateEntity(null, newDTO);
            db.productsalespermonths.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOproductsalespermonth> Getproductsalespermonth()
        {
            List<DTOproductsalespermonth> toReturn = new List<DTOproductsalespermonth>();
            List<productsalespermonth> list = (from c in db.productsalespermonths select c).ToList();

            foreach (productsalespermonth p in list)
            {
                toReturn.Add(new DTOproductsalespermonth(p));
            }

            return toReturn;
        }


        // PUT: api/productsalespermonth/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putproductsalespermonth(int ID, DTOproductsalespermonth editedDTO)
        {
            productsalespermonth toUpdate = db.productsalespermonths.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOproducttype))]
        public async Task<DTOproducttype> Postproducttype(DTOproducttype newDTO)
        {

            producttype newProd = EntityMapper.updateEntity(null, newDTO);
            db.producttypes.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOproducttype> Getproducttype()
        {
            List<DTOproducttype> toReturn = new List<DTOproducttype>();
            List<producttype> list = (from c in db.producttypes select c).ToList();

            foreach (producttype p in list)
            {
                toReturn.Add(new DTOproducttype(p));
            }

            return toReturn;
        }


        // PUT: api/producttype/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putproducttype(int ID, DTOproducttype editedDTO)
        {
            producttype toUpdate = db.producttypes.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOprocessapplication))]
        public async Task<DTOprocessapplication> Postprocessapplication(DTOprocessapplication newDTO)
        {

            processapplication newProd = EntityMapper.updateEntity(null, newDTO);
            db.processapplications.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOprocessapplication> Getprocessapplication()
        {
            List<DTOprocessapplication> toReturn = new List<DTOprocessapplication>();
            List<processapplication> list = (from c in db.processapplications select c).ToList();

            foreach (processapplication p in list)
            {
                toReturn.Add(new DTOprocessapplication(p));
            }

            return toReturn;
        }


        // PUT: api/processapplication/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putprocessapplication(int ID, DTOprocessapplication editedDTO)
        {
            processapplication toUpdate = db.processapplications.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOreseller))]
        public async Task<DTOreseller> Postreseller(DTOreseller newDTO)
        {

            reseller newProd = EntityMapper.updateEntity(null, newDTO);
            db.resellers.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOreseller> Getreseller()
        {
            List<DTOreseller> toReturn = new List<DTOreseller>();
            List<reseller> list = (from c in db.resellers select c).ToList();

            foreach (reseller p in list)
            {
                toReturn.Add(new DTOreseller(p));
            }

            return toReturn;
        }


        // PUT: api/reseller/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putreseller(int ID, DTOreseller editedDTO)
        {
            reseller toUpdate = db.resellers.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOsystemadmin))]
        public async Task<DTOsystemadmin> Postsystemadmin(DTOsystemadmin newDTO)
        {

            systemadmin newProd = EntityMapper.updateEntity(null, newDTO);
            db.systemadmins.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOsystemadmin> Getsystemadmin()
        {
            List<DTOsystemadmin> toReturn = new List<DTOsystemadmin>();
            List<systemadmin> list = (from c in db.systemadmins select c).ToList();

            foreach (systemadmin p in list)
            {
                toReturn.Add(new DTOsystemadmin(p));
            }

            return toReturn;
        }


        // PUT: api/systemadmin/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putsystemadmin(int ID, DTOsystemadmin editedDTO)
        {
            systemadmin toUpdate = db.systemadmins.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOtransactiontype))]
        public async Task<DTOtransactiontype> Posttransactiontype(DTOtransactiontype newDTO)
        {

            transactiontype newProd = EntityMapper.updateEntity(null, newDTO);
            db.transactiontypes.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOtransactiontype> Gettransactiontype()
        {
            List<DTOtransactiontype> toReturn = new List<DTOtransactiontype>();
            List<transactiontype> list = (from c in db.transactiontypes select c).ToList();

            foreach (transactiontype p in list)
            {
                toReturn.Add(new DTOtransactiontype(p));
            }

            return toReturn;
        }


        // PUT: api/transactiontype/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttransactiontype(int ID, DTOtransactiontype editedDTO)
        {
            transactiontype toUpdate = db.transactiontypes.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOunittype))]
        public async Task<DTOunittype> Postunittype(DTOunittype newDTO)
        {

            unittype newProd = EntityMapper.updateEntity(null, newDTO);
            db.unittypes.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOunittype> Getunittype()
        {
            List<DTOunittype> toReturn = new List<DTOunittype>();
            List<unittype> list = (from c in db.unittypes select c).ToList();

            foreach (unittype p in list)
            {
                toReturn.Add(new DTOunittype(p));
            }

            return toReturn;
        }


        // PUT: api/unittype/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putunittype(int ID, DTOunittype editedDTO)
        {
            unittype toUpdate = db.unittypes.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOuser))]
        public async Task<DTOuser> Postuser(DTOuser newDTO)
        {

            user newProd = EntityMapper.updateEntity(null, newDTO);
            db.users.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOuser> Getuser()
        {
            List<DTOuser> toReturn = new List<DTOuser>();
            List<user> list = (from c in db.users select c).ToList();

            foreach (user p in list)
            {
                toReturn.Add(new DTOuser(p));
            }

            return toReturn;
        }


        // PUT: api/user/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putuser(int ID, DTOuser editedDTO)
        {
            user toUpdate = db.users.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOusertype))]
        public async Task<DTOusertype> Postusertype(DTOusertype newDTO)
        {

            usertype newProd = EntityMapper.updateEntity(null, newDTO);
            db.usertypes.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOusertype> Getusertype()
        {
            List<DTOusertype> toReturn = new List<DTOusertype>();
            List<usertype> list = (from c in db.usertypes select c).ToList();

            foreach (usertype p in list)
            {
                toReturn.Add(new DTOusertype(p));
            }

            return toReturn;
        }


        // PUT: api/usertype/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putusertype(int ID, DTOusertype editedDTO)
        {
            usertype toUpdate = db.usertypes.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOvalidator))]
        public async Task<DTOvalidator> Postvalidator(DTOvalidator newDTO)
        {

            validator newProd = EntityMapper.updateEntity(null, newDTO);
            db.validators.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOvalidator> Getvalidator()
        {
            List<DTOvalidator> toReturn = new List<DTOvalidator>();
            List<validator> list = (from c in db.validators select c).ToList();

            foreach (validator p in list)
            {
                toReturn.Add(new DTOvalidator(p));
            }

            return toReturn;
        }


        // PUT: api/validator/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putvalidator(int ID, DTOvalidator editedDTO)
        {
            validator toUpdate = db.validators.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOvoucher))]
        public async Task<DTOvoucher> Postvoucher(DTOvoucher newDTO)
        {

            voucher newProd = EntityMapper.updateEntity(null, newDTO);
            db.vouchers.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOvoucher> Getvoucher()
        {
            List<DTOvoucher> toReturn = new List<DTOvoucher>();
            List<voucher> list = (from c in db.vouchers select c).ToList();

            foreach (voucher p in list)
            {
                toReturn.Add(new DTOvoucher(p));
            }

            return toReturn;
        }


        // PUT: api/voucher/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putvoucher(int ID, DTOvoucher editedDTO)
        {
            voucher toUpdate = db.vouchers.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOvouchertransaction))]
        public async Task<DTOvouchertransaction> Postvouchertransaction(DTOvouchertransaction newDTO)
        {

            vouchertransaction newProd = EntityMapper.updateEntity(null, newDTO);
            db.vouchertransactions.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOvouchertransaction> Getvouchertransaction()
        {
            List<DTOvouchertransaction> toReturn = new List<DTOvouchertransaction>();
            List<vouchertransaction> list = (from c in db.vouchertransactions select c).ToList();

            foreach (vouchertransaction p in list)
            {
                toReturn.Add(new DTOvouchertransaction(p));
            }

            return toReturn;
        }


        // PUT: api/vouchertransaction/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putvouchertransaction(int ID, DTOvouchertransaction editedDTO)
        {
            vouchertransaction toUpdate = db.vouchertransactions.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/testManager
        [HttpPost]
        [ResponseType(typeof(DTOvouchertype))]
        public async Task<DTOvouchertype> Postvouchertype(DTOvouchertype newDTO)
        {

            vouchertype newProd = EntityMapper.updateEntity(null, newDTO);
            db.vouchertypes.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        [HttpGet]
        // GET: api/testManager
        public List<DTOvouchertype> Getvouchertype()
        {
            List<DTOvouchertype> toReturn = new List<DTOvouchertype>();
            List<vouchertype> list = (from c in db.vouchertypes select c).ToList();

            foreach (vouchertype p in list)
            {
                toReturn.Add(new DTOvouchertype(p));
            }

            return toReturn;
        }


        // PUT: api/vouchertype/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putvouchertype(int ID, DTOvouchertype editedDTO)
        {
            vouchertype toUpdate = db.vouchertypes.Find(ID);
            toUpdate = EntityMapper.updateEntity(toUpdate, editedDTO);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }




    }
}
