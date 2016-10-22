using NanofinAPI.Models;
using NanofinAPI.Models.DTOEnvironment;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NanofinAPI.Controllers
{
    public class ConsumerProfilesController : ApiController
    {
        database_nanofinEntities db = new database_nanofinEntities();

        public List<consumerprofiledata> getConsumerProfileData()
        {
            return db.consumerprofiledatas.ToList();
        }

        public List<consumerpreferencesreport> getPreferencesReports()
        {
            return db.consumerpreferencesreports.ToList();
        }

        //Update method consumer table: Additional Sign up Info
        //Accept/Reject Claim: Claim status
        [HttpPut]
        public async Task<IHttpActionResult> putAdditionalSignUpInfo(int userID, string consumerAddress, string homeOwnerType, Nullable<int> numDependants, string topProductsInterestedIn, Nullable<decimal> grossMonthly, Nullable<decimal> nettMonthly, Nullable<decimal> totalExpenses  )
        {
            consumer toUpdate = (from c in db.consumers where c.User_ID == userID select c).SingleOrDefault();
            DTOconsumer dtoConsumer = new DTOconsumer(toUpdate);
            dtoConsumer.consumerAddress = consumerAddress;
            dtoConsumer.homeOwnerType = homeOwnerType;
            dtoConsumer.numDependant = numDependants;
            dtoConsumer.topProductCategoriesInterestedIn = topProductsInterestedIn;
            dtoConsumer.grossMonthlyIncome = grossMonthly;
            dtoConsumer.nettMonthlyIncome = nettMonthly;
            dtoConsumer.totalMonthlyExpenses = totalExpenses;
            toUpdate = EntityMapper.updateEntity(toUpdate, dtoConsumer);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.OK);

        }

        [HttpPost]
        public bool getIsHomeOwnerTypeNull(int userID)
        {
            consumer cons= (from c in db.consumers where c.User_ID == userID select c).SingleOrDefault();
            if (cons.homeOwnerType == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Update method consumer table: Additional Sign up Info
        //Accept/Reject Claim: Claim status
        [HttpPut]
        public async Task<IHttpActionResult> consumerUpdateEntireProfile(int userID, string userFirstName, string userLastName, string UserName, string userEmail, string userContactNum, DateTime consumerDateOfBirth, string consumerAddress,string maritalStatus, string homeOwnerType, string employmentStatus, Nullable<int> numDependants, string topProductsInterestedIn, Nullable<decimal> grossMonthly, Nullable<decimal> nettMonthly, Nullable<decimal> totalExpenses)
        {
            //user Table:
            user toUpdateUser = (from u in db.users where u.User_ID == userID select u).SingleOrDefault();
            DTOuser dtoUser = new DTOuser(toUpdateUser);
            toUpdateUser.userFirstName = userFirstName;
            toUpdateUser.userLastName = userLastName;
            toUpdateUser.userName = UserName;
            toUpdateUser.userEmail = userEmail;
            toUpdateUser.userContactNumber = userContactNum;
            toUpdateUser = EntityMapper.updateEntity(toUpdateUser, dtoUser);
            db.Entry(toUpdateUser).State = EntityState.Modified;
            await db.SaveChangesAsync();

            //consumer Table:
            consumer toUpdate = (from c in db.consumers where c.User_ID == userID select c).SingleOrDefault();
            DTOconsumer dtoConsumer = new DTOconsumer(toUpdate);
            dtoConsumer.consumerDateOfBirth = consumerDateOfBirth;
            dtoConsumer.consumerAddress = consumerAddress;
            dtoConsumer.maritalStatus = maritalStatus;
            dtoConsumer.homeOwnerType = homeOwnerType;
            dtoConsumer.numDependant = numDependants;
            dtoConsumer.topProductCategoriesInterestedIn = topProductsInterestedIn;
            dtoConsumer.grossMonthlyIncome = grossMonthly;
            dtoConsumer.nettMonthlyIncome = nettMonthly;
            dtoConsumer.totalMonthlyExpenses = totalExpenses;
            toUpdate = EntityMapper.updateEntity(toUpdate, dtoConsumer);
            db.Entry(toUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.OK);

        }

        [HttpPut]
        public async Task<IHttpActionResult> consumerUpdateJustBasicProfile(int userID, string userFirstName, string userLastName, string UserName, string userEmail, string userContactNum, DateTime consumerDateOfBirth, string consumerAddress, string maritalStatus)
        {
            //user Table:
            user toUpdateUser = (from u in db.users where u.User_ID == userID select u).SingleOrDefault();
            DTOuser dtoUser = new DTOuser(toUpdateUser);
            toUpdateUser.userFirstName = userFirstName;
            toUpdateUser.userLastName = userLastName;
            toUpdateUser.userName = UserName;
            toUpdateUser.userEmail = userEmail;
            toUpdateUser.userContactNumber = userContactNum;
            toUpdateUser = EntityMapper.updateEntity(toUpdateUser, dtoUser);
            db.Entry(toUpdateUser).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.OK);
        }

        //deactivate user: to reactivate...they must just login again?
        [HttpPut]
        public async Task<IHttpActionResult> consumerDeactivateProfile(int userID)
        {
            //user Table:
            user toUpdateUser = (from u in db.users where u.User_ID == userID select u).SingleOrDefault();
            DTOuser dtoUser = new DTOuser(toUpdateUser);
            toUpdateUser.userIsActive = false;
           
            toUpdateUser = EntityMapper.updateEntity(toUpdateUser, dtoUser);
            db.Entry(toUpdateUser).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.OK);
        }



    }
}
