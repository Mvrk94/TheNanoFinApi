
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NanofinAPI.Models;
using NanofinAPI.Models.DTOEnvironment;

namespace NanoFinAPI.Controllers
{
    public class UserHandlerController : ApiController
    {
        nanofinEntities db = new nanofinEntities();

        #region UserType CRUD

        //GET List of usertypes
        [HttpGet]
        public List<DTOusertype> getUserTypesList()
        {
            List<DTOusertype> list = new List<DTOusertype>();

            foreach (usertype us in db.usertypes)
            {
                list.Add(new DTOusertype(us));
            }

            return list;

        }

        //GET usertype by id
        [ResponseType(typeof(DTOusertype))]
        [HttpGet]
        public async Task<IHttpActionResult> getUserType(int id)
        {
            DTOusertype toReturn = new DTOusertype(await db.usertypes.FindAsync(id));
            if (toReturn == null)
            {
                return NotFound();
            }
            return CreatedAtRoute("DefaultApi", new { id = toReturn.UserType_ID }, toReturn);

        }


        //PUT userType
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> putUser(int id, DTOusertype dtousertype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dtousertype.UserType_ID)
            {
                return BadRequest();
            }

            var putUsertype = db.usertypes.Single(e => e.UserType_ID == id);
            db.Entry(EntityMapper.updateEntity(putUsertype, dtousertype)).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(id))
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

        //DELETE userType
        [ResponseType(typeof(DTOuser))]
        [HttpDelete]
        public async Task<IHttpActionResult> deleteUserType(int id)
        {
            var userTypeToDel = db.usertypes.SingleOrDefault(x => x.UserType_ID == id);

            if (userTypeToDel == null)
            {
                return NotFound();
            }

            DTOusertype temp = new DTOusertype(userTypeToDel);

            db.usertypes.Remove(userTypeToDel);
            await db.SaveChangesAsync();

            return Ok(temp);
        }

        //POST userType
        [ResponseType(typeof(usertype))]
        [HttpPost]
        public async Task<IHttpActionResult> postUserType(DTOusertype dtoUsertype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.usertypes.Add(EntityMapper.updateEntity(null, dtoUsertype));
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dtoUsertype.UserType_ID }, dtoUsertype);
        }


        #endregion

        #region User CRUD
        //GET a list of users (Without passwords)
        [HttpGet]
        [Authorize]
        public List<DTOUsersGetNoPass> getUsersList()
        {
            List<DTOUsersGetNoPass> list = new List<DTOUsersGetNoPass>();

            foreach (user us in db.users)
            {
                list.Add(new DTOUsersGetNoPass(us));
            }

            return list;

        }

        //GET a user by id
        [ResponseType(typeof(DTOuser))]
        [HttpGet]
        public async Task<IHttpActionResult> getUser(int id)
        {
            DTOuser toReturn = new DTOuser(await db.users.FindAsync(id));
            if (toReturn == null)
            {
                return NotFound();
            }
            return CreatedAtRoute("DefaultApi", new { id = toReturn.User_ID }, toReturn);

        }

        // PUT: update a user
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> putUser(int id, DTOuser dtouser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dtouser.User_ID)
            {
                return BadRequest();
            }

            var putUser = db.users.Single(e => e.User_ID == id);
            db.Entry(EntityMapper.updateEntity(putUser, dtouser)).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(id))
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

        // POST: add a user
        [ResponseType(typeof(user))]
        [HttpPost]
        public async Task<IHttpActionResult> postUser(DTOuser dtoUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.users.Add(EntityMapper.updateEntity(null, dtoUser));
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dtoUser.User_ID }, dtoUser);
        }

        // DELETE: delete a user
        [ResponseType(typeof(DTOuser))]
        [HttpDelete]
        public async Task<IHttpActionResult> deleteUser(int id)
        {
            var userToDel = db.users.SingleOrDefault(x => x.User_ID == id);

            if (userToDel == null)
            {
                return NotFound();
            }

            DTOuser temp = new DTOuser(userToDel);

            db.users.Remove(userToDel);
            await db.SaveChangesAsync();

            return Ok(temp);
        }

        #endregion

        #region Reseller CRU
        //GET a list of resellers
        [HttpGet]
        public List<DTOreseller> getResellersList()
        {
            List<DTOreseller> list = new List<DTOreseller>();

            foreach (reseller res in db.resellers)
            {
                list.Add(new DTOreseller(res));
            }

            return list;

        }

        //GET a reseller by id
        [ResponseType(typeof(DTOreseller))]
        [HttpGet]
        public async Task<IHttpActionResult> getReseller(int id)
        {
            DTOreseller toReturn = new DTOreseller(await db.resellers.FindAsync(id));
            if (toReturn == null)
            {
                return NotFound();
            }
            return CreatedAtRoute("DefaultApi", new { id = toReturn.Reseller_ID }, toReturn);

        }

        //PUT a reseller
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> putReseller(int id, DTOreseller dtoreseller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dtoreseller.Reseller_ID)
            {
                return BadRequest();
            }

            var putUser = db.resellers.Single(e => e.Reseller_ID == id);
            db.Entry(EntityMapper.updateEntity(putUser, dtoreseller)).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(id))
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


        //POST a reseller
        [HttpPost]
        public async Task<IHttpActionResult> postReseller(DTOreseller dtoReseller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.resellers.Add(EntityMapper.updateEntity(null, dtoReseller));
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dtoReseller.Reseller_ID }, dtoReseller);
        }
        #endregion

        #region Consumer CRU

        //GET a list of consumers
        [HttpGet]
        public List<DTOconsumer> getConsumerList()
        {
            List<DTOconsumer> list = new List<DTOconsumer>();

            foreach (consumer cons in db.consumers)
            {
                list.Add(new DTOconsumer(cons));
            }

            return list;

        }

        //GET a consumer by id
        [ResponseType(typeof(DTOconsumer))]
        [HttpGet]
        public async Task<IHttpActionResult> getConsumer(int id)
        {
            DTOconsumer toReturn = new DTOconsumer(await db.consumers.FindAsync(id));
            if (toReturn == null)
            {
                return NotFound();
            }
            return CreatedAtRoute("DefaultApi", new { id = toReturn.Consumer_ID }, toReturn);

        }

        //PUT a consumer
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> put(int id, DTOconsumer dtoconsumer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dtoconsumer.Consumer_ID)
            {
                return BadRequest();
            }

            var putUser = db.consumers.Single(e => e.Consumer_ID == id);
            db.Entry(EntityMapper.updateEntity(putUser, dtoconsumer)).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(id))
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

        //POST a consumer
        public async Task<IHttpActionResult> postConsumer(DTOconsumer dtoConsumer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.consumers.Add(EntityMapper.updateEntity(null, dtoConsumer));
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dtoConsumer.Consumer_ID }, dtoConsumer);
        }

        #endregion

        #region Validator CRU

        //GET validatorlist
        [HttpGet]
        public List<DTOvalidator> getValidatorList()
        {
            List<DTOvalidator> list = new List<DTOvalidator>();

            foreach (validator v in db.validators)
            {
                list.Add(new DTOvalidator(v));
            }

            return list;

        }

        //GET validtator by id
        [ResponseType(typeof(DTOvalidator))]
        [HttpGet]
        public async Task<IHttpActionResult> getValidator(int id)
        {
            DTOvalidator toReturn = new DTOvalidator(await db.validators.FindAsync(id));
            if (toReturn == null)
            {
                return NotFound();
            }
            return CreatedAtRoute("DefaultApi", new { id = toReturn.Validator_ID }, toReturn);

        }

        //PUT validator
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> putValidator(int id, DTOvalidator dtovalidator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dtovalidator.Validator_ID)
            {
                return BadRequest();
            }

            var putValidator = db.validators.Single(e => e.Validator_ID == id);
            db.Entry(EntityMapper.updateEntity(putValidator, dtovalidator)).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(id))
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

        //POST
        [ResponseType(typeof(validator))]
        [HttpPost]
        public async Task<IHttpActionResult> postValidator(DTOvalidator dtovalidator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.validators.Add(EntityMapper.updateEntity(null, dtovalidator));
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dtovalidator.Validator_ID }, dtovalidator);
        }

        #endregion

        #region productProvider CRU
        //GET productprovider list
        [HttpGet]
        public List<DTOproductprovider> getProductProviderList()
        {
            List<DTOproductprovider> list = new List<DTOproductprovider>();

            foreach (productprovider p in db.productproviders)
            {
                list.Add(new DTOproductprovider(p));
            }

            return list;

        }

        //Get productProvider by id
        [ResponseType(typeof(DTOproductprovider))]
        [HttpGet]
        public async Task<IHttpActionResult> getProductProvider(int id)
        {
           DTOproductprovider toReturn = new DTOproductprovider(await db.productproviders.FindAsync(id));
            if (toReturn == null)
            {
                return NotFound();
            }
            return CreatedAtRoute("DefaultApi", new { id = toReturn.ProductProvider_ID }, toReturn);

        }

        //PUT
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> putProductProvider(int id, DTOproductprovider dtoproductProvider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dtoproductProvider.ProductProvider_ID)
            {
                return BadRequest();
            }

            var putProductProvider = db.productproviders.Single(e => e.ProductProvider_ID == id);
            db.Entry(EntityMapper.updateEntity(putProductProvider, dtoproductProvider)).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(id))
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

        //POST

        [ResponseType(typeof(productprovider))]
        [HttpPost]
        public async Task<IHttpActionResult> postProductProvider(DTOproductprovider dtoprodprovider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.productproviders.Add(EntityMapper.updateEntity(null, dtoprodprovider));
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dtoprodprovider.ProductProvider_ID }, dtoprodprovider);
        }
        #endregion

        #region systemAdmin CRU
        //GET List of systemAdmin
        [HttpGet]
        public List<DTOsystemadmin> getSystemAdminList()
        {
            List<DTOsystemadmin> list = new List<DTOsystemadmin>();

            foreach (systemadmin sa in db.systemadmins)
            {
                list.Add(new DTOsystemadmin(sa));
            }

            return list;

        }
        

        [HttpGet]
        [ResponseType(typeof(SessionObj))]
        public async Task<SessionObj> getUserSession(int userID)
        {
            user toReturn = await db.users.FindAsync(userID);

            if (toReturn == null)
            {
                return null;
            }

            return new SessionObj(toReturn);
        }

        //GET systemAdmin by id
        [ResponseType(typeof(DTOsystemadmin))]
        [HttpGet]
        public async Task<IHttpActionResult> getSystemAdmin(int id)
        {
            DTOsystemadmin toReturn = new DTOsystemadmin(await db.systemadmins.FindAsync(id));
            if (toReturn == null)
            {
                return NotFound();
            }
            return CreatedAtRoute("DefaultApi", new { id = toReturn.SystemAdmin_ID }, toReturn);

        }


        //PUT systemAdmin
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> putSystemAdmin(int id, DTOsystemadmin dtosysAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dtosysAdmin.SystemAdmin_ID)
            {
                return BadRequest();
            }

            var putSysadmin = db.systemadmins.Single(e => e.SystemAdmin_ID == id);
            db.Entry(EntityMapper.updateEntity(putSysadmin, dtosysAdmin)).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(id))
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


        //POST systemAdmin
        [ResponseType(typeof(systemadmin))]
        [HttpPost]
        public async Task<IHttpActionResult> postSystemAdmin(DTOsystemadmin dtoSysadmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.systemadmins.Add(EntityMapper.updateEntity(null, dtoSysadmin));
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dtoSysadmin.SystemAdmin_ID }, dtoSysadmin);
        }
        #endregion


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userExists(int id)
        {
            return db.users.Count(e => e.User_ID == id) > 0;
        }




    }
}
