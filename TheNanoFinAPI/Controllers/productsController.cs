using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TheNanoFinAPI.Models;
using TheNanoFinAPI.Models.DTOEnvironment;

namespace NanoFinAPI.Controllers
{
    public class productsController : ApiController
    {
        private nanofinEntities db = new nanofinEntities();

        // GET: api/products
        public List<DTOproduct> Getproducts()
        {

            List<DTOproduct> list = new List<DTOproduct>();

            foreach (product prod in db.products)
            {
                list.Add(new DTOproduct(prod));
            }

            return list;
        }

        // GET: api/products/5
        [ResponseType(typeof(DTOproduct))]
        public async Task<IHttpActionResult> Getproduct(int id)
        {
            DTOproduct toReturn = new DTOproduct(await db.products.FindAsync(id));
            if (toReturn == null)
            {
                return NotFound();
            }
            return CreatedAtRoute("DefaultApi", new { id = toReturn.Product_ID }, toReturn); ;
        }

        // PUT: api/products/5  Edit product
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putproduct(int id, DTOproduct productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productDTO.Product_ID)
            {
                return BadRequest();
            }

            var putProd = db.products.Single(e => e.Product_ID == id);
            db.Entry(EntityMapper.updateEntity(putProd, productDTO)).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productExists(id))
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

        // POST: api/products Add
        [ResponseType(typeof(product))]
        public async Task<IHttpActionResult> Postproduct(DTOproduct productT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            product p = EntityMapper.updateEntity(null, productT);
            db.products.Add(p);
            db.SaveChanges();
            productT.Product_ID = p.Product_ID;
            return CreatedAtRoute("DefaultApi", new { id = productT.Product_ID}, productT);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool productExists(int id)
        {
            return db.products.Count(e => e.Product_ID == id) > 0;
        }


        [HttpGet]
        public List<InsuranceProductMobileDTO> getInsuranceProductsMob()
        {
            List<InsuranceProductMobileDTO> toreturn = new List<InsuranceProductMobileDTO>();

            List<insuranceproduct> list = (from c in db.insuranceproducts orderby c.InsuranceType_ID select c).ToList();

            foreach(insuranceproduct p in list)
                toreturn.Add(new InsuranceProductMobileDTO(p));

            return toreturn;
        }
    }
}