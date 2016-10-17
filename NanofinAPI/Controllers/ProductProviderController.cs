using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using NanofinAPI.Models;
using NanofinAPI.Models.DTOEnvironment;
using TheNanoFinAPI.MultiChainLib.Controllers;

namespace NanofinAPI.Controllers
{
    public class ProductProviderController : ApiController
    {

        private database_nanofinEntities db = new database_nanofinEntities();

        #region scripts for: ProductProvider Payment, Update of Expired Products

        //Pay the insurance provider for products that have been accepted:
        [HttpPut]
        public async Task<IHttpActionResult> updateInsuranceProviderPaymentStatus(int ppID)
        {
            //list of what has not been paid to this productProvider:
            List<productproviderpayment> paymentsNotMadeList = (from c in db.productproviderpayments where c.hasBeenPayed == false && c.ProductProvider_ID == ppID select c).ToList();
            //Note isActive and Expiry are not too relevant here: should still pay if a product has expired.
            //never be paid if a refund was made: ie purchase rejected.

            foreach (productproviderpayment p in paymentsNotMadeList)
            {
                activeproductitem activeProdRelatedToPayment = (from a in db.activeproductitems where a.ActiveProductItems_ID == p.ActiveProductItems_ID select a).SingleOrDefault();
                if (activeProdRelatedToPayment.Accepted == true)//this product purchase has gone through so the IM should get paid
                {
                    p.hasBeenPayed = true;
                    db.Entry(p).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                else if (activeProdRelatedToPayment.Accepted == false) //product purchase rejected: Refund so the IM won't get paid
                {
                    p.hasBeenPayed = false;
                    db.Entry(p).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                else // IM has not accepted/rejected the product.
                     //Check if date has passed - if yes pay IM. This assumes the rule: consumer covered by Product Provider unless IM rejects
                {
                    if (hasDateExpired(activeProdRelatedToPayment.activeProductItemEndDate) == true)
                    {
                        activeProdRelatedToPayment.Accepted = true;
                        activeProdRelatedToPayment.isActive = false;
                        db.Entry(activeProdRelatedToPayment).State = EntityState.Modified;
                        await db.SaveChangesAsync();

                        p.hasBeenPayed = true;
                        db.Entry(p).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        p.hasBeenPayed = false;
                        db.Entry(p).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                }

            }

            return StatusCode(HttpStatusCode.OK);
        }

        //check if date has passed. today = false (ie not passed)
        public bool hasDateExpired(DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.Date < DateTime.Now.Date;
            }
            else
            {
                return false;
            }
        }

        //Show list of payments to still make to this PP
        [HttpGet]
        public List<DTOproductproviderpayment> ppPaymentsToStillMake(int productProviderID)
        {
            List<DTOproductproviderpayment> toReturn = new List<DTOproductproviderpayment>();
            List<productproviderpayment> list = (from l in db.productproviderpayments where l.ProductProvider_ID == productProviderID && l.hasBeenPayed == false select l).ToList();

            foreach (productproviderpayment p in list)
            {
                toReturn.Add(new DTOproductproviderpayment(p));
            }
            return toReturn;
        }


        //update the isActive based on Expiries
        [HttpPut]
        public IHttpActionResult updateExpiredProductsIsActiveStatus()
        {
            DateTime dateNow = DateTime.Now;
            activeproductitem[] activProducts = (from c in db.activeproductitems where c.activeProductItemEndDate != null && (DateTime.Compare(c.activeProductItemEndDate.Value, dateNow) < 0) select c).ToArray(); //t1 < t2, endDate is earlier than Now <0
            List<DTOactiveproductitem> dtoActProds = new List<DTOactiveproductitem>();
            foreach (activeproductitem a in activProducts)
            {
                dtoActProds.Add(new DTOactiveproductitem(a)); //copy over
            }

            foreach (DTOactiveproductitem d in dtoActProds)
            {
                d.isActive = false;//set isactive to false
            }

            for (int i = 0; i < activProducts.Length; i++)
            {
                activProducts[i] = EntityMapper.updateEntity(activProducts[i], dtoActProds[i]);
                db.Entry(activProducts[i]).State = EntityState.Modified;
                db.SaveChanges();
            }

            return StatusCode(HttpStatusCode.OK);
        }

        #endregion

    }
}