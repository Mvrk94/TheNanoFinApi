using NanofinAPI.Models;
using System;
using System.Collections.Generic;
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
        [ResponseType(typeof(DTOactiveproductitem))]
        public async Task<DTOactiveproductitem> Postactiveproductitem(DTOactiveproductitem newDTO)
        {
            activeproductitem newProd = EntityMapper.updateEntity(null, newDTO);
            db.activeproductitems.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


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


        // POST: api/testManager
        [ResponseType(typeof(DTOconsumer))]
        public async Task<DTOconsumer> Postconsumer(DTOconsumer newDTO)
        {

            consumer newProd = EntityMapper.updateEntity(null, newDTO);
            db.consumers.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }



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
        // POST: api/testManager
        [ResponseType(typeof(DTOinsuranceproduct))]
        public async Task<DTOinsuranceproduct> Postinsuranceproduct(DTOinsuranceproduct newDTO)
        {

            insuranceproduct newProd = EntityMapper.updateEntity(null, newDTO);
            db.insuranceproducts.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


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
        // POST: api/testManager
        [ResponseType(typeof(DTOinsurancetype))]
        public async Task<DTOinsurancetype> Postinsurancetype(DTOinsurancetype newDTO)
        {

            insurancetype newProd = EntityMapper.updateEntity(null, newDTO);
            db.insurancetypes.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


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
        // POST: api/testManager
        [ResponseType(typeof(DTOnotificationlog))]
        public async Task<DTOnotificationlog> Postnotificationlog(DTOnotificationlog newDTO)
        {

            notificationlog newProd = EntityMapper.updateEntity(null, newDTO);
            db.notificationlogs.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


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
        // POST: api/testManager
        [ResponseType(typeof(DTOproduct))]
        public async Task<DTOproduct> Postproduct(DTOproduct newDTO)
        {

            product newProd = EntityMapper.updateEntity(null, newDTO);
            db.products.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


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
        // POST: api/testManager
        [ResponseType(typeof(DTOproductprovider))]
        public async Task<DTOproductprovider> Postproductprovider(DTOproductprovider newDTO)
        {

            productprovider newProd = EntityMapper.updateEntity(null, newDTO);
            db.productproviders.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


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
        // POST: api/testManager
        [ResponseType(typeof(DTOproducttype))]
        public async Task<DTOproducttype> Postproducttype(DTOproducttype newDTO)
        {

            producttype newProd = EntityMapper.updateEntity(null, newDTO);
            db.producttypes.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


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
        // POST: api/testManager
        [ResponseType(typeof(DTOreseller))]
        public async Task<DTOreseller> Postreseller(DTOreseller newDTO)
        {

            reseller newProd = EntityMapper.updateEntity(null, newDTO);
            db.resellers.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


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
        // POST: api/testManager
        [ResponseType(typeof(DTOsystemadmin))]
        public async Task<DTOsystemadmin> Postsystemadmin(DTOsystemadmin newDTO)
        {

            systemadmin newProd = EntityMapper.updateEntity(null, newDTO);
            db.systemadmins.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


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
        // POST: api/testManager
        [ResponseType(typeof(DTOtransactiontype))]
        public async Task<DTOtransactiontype> Posttransactiontype(DTOtransactiontype newDTO)
        {

            transactiontype newProd = EntityMapper.updateEntity(null, newDTO);
            db.transactiontypes.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


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
        // POST: api/testManager
        [ResponseType(typeof(DTOtransactiontype))]
        public async Task<DTOtransactiontype> Posttrasnsactiontype(DTOtransactiontype newDTO)
        {

            transactiontype newProd = EntityMapper.updateEntity(null, newDTO);
            db.transactiontypes.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


        // GET: api/testManager
        public List<DTOtransactiontype> Gettrasnsactiontype()
        {
            List<DTOtransactiontype> toReturn = new List<DTOtransactiontype>();
            List<transactiontype> list = (from c in db.transactiontypes select c).ToList();

            foreach (transactiontype p in list)
            {
                toReturn.Add(new DTOtransactiontype(p));
            }

            return toReturn;
        }
        // POST: api/testManager
        [ResponseType(typeof(DTOuser))]
        public async Task<DTOuser> Postuser(DTOuser newDTO)
        {

            user newProd = EntityMapper.updateEntity(null, newDTO);
            db.users.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


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
        // POST: api/testManager
        [ResponseType(typeof(DTOusertype))]
        public async Task<DTOusertype> Postusertype(DTOusertype newDTO)
        {

            usertype newProd = EntityMapper.updateEntity(null, newDTO);
            db.usertypes.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


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
        // POST: api/testManager
        [ResponseType(typeof(DTOvalidator))]
        public async Task<DTOvalidator> Postvalidator(DTOvalidator newDTO)
        {

            validator newProd = EntityMapper.updateEntity(null, newDTO);
            db.validators.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


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
        // POST: api/testManager
        [ResponseType(typeof(DTOvoucher))]
        public async Task<DTOvoucher> Postvoucher(DTOvoucher newDTO)
        {

            voucher newProd = EntityMapper.updateEntity(null, newDTO);
            db.vouchers.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


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


        // GET: api/testManager
        public List<DTOproductredemptionlog> RedemptionLog()
        {
            List<DTOproductredemptionlog> toReturn = new List<DTOproductredemptionlog>();
            List<productredemptionlog> list = (from c in db.productredemptionlogs select c).ToList();

            foreach (productredemptionlog p in list)
            {
                toReturn.Add(new DTOproductredemptionlog(p));
            }

            return toReturn;
        }


        // POST: api/testManager
        [ResponseType(typeof(DTOvouchertransaction))]
        public async Task<DTOvouchertransaction> Postvouchertransaction(DTOvouchertransaction newDTO)
        {

            vouchertransaction newProd = EntityMapper.updateEntity(null, newDTO);
            db.vouchertransactions.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


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
        // POST: api/testManager
        [ResponseType(typeof(DTOvouchertype))]
        public async Task<DTOvouchertype> Postvouchertype(DTOvouchertype newDTO)
        {

            vouchertype newProd = EntityMapper.updateEntity(null, newDTO);
            db.vouchertypes.Add(newProd);
            await db.SaveChangesAsync();

            return newDTO;
        }


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



    }
}
