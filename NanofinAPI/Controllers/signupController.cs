using NanofinAPI.Models;
using NanofinAPI.Models.DTOEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TheNanoFinAPI.MultiChainLib.Controllers;



namespace NanofinAPI.Controllers
{
    public class signupController : ApiController
    {

        database_nanofinEntities db = new database_nanofinEntities();

        //post user - return true if user created. userType - 11 for consumer & 21 for reseller
        [HttpPost]
        [ResponseType(typeof(DTOuser))]
        public async Task<DTOuser> postUser(string fName, string lName, string userName, string email,string contactNum, string userPass, int userType, string IDnumber )
        {
            user tmp = new user();
            tmp.userFirstName = fName;
            tmp.userLastName = lName;
            tmp.userName = userName;
            tmp.userEmail = email;
            tmp.userContactNumber = contactNum;
            tmp.userPassword = encryptPass(userPass);
            tmp.userIsActive = true;
            tmp.userType = userType;
            if(userType == 11) //user is consumer
            {
                tmp.userActivationType = "verified";
            }
            else if (userType == 21) //user is reseller - verified through NF staff
            {
                tmp.userActivationType = "verified";
            }
            else //user is neither consumer or reseller and should be flagged
            {
                tmp.userActivationType = "None";
            }
            tmp.IDnumber = IDnumber;
            tmp.IdDocumentPath = null;
            tmp.IdDocumentLastUpdated = DateTime.Now;

           
            db.users.Add(tmp);
            await db.SaveChangesAsync();

            MUserController tmpBCUser = new MUserController(tmp.User_ID); //create new user
            tmpBCUser = await tmpBCUser.init(); //initializing user will set users blockchain address.
            tmp.blockchainAddress = tmpBCUser.propertyUserAddress();

            return new DTOuser(tmp);
        }

        private string encryptPass(string password)
        {
           return BCrypt.Net.BCrypt.HashPassword(password);
        }

        //post user - return true if user created. userType - 11 for consumer & 21 for reseller
        [HttpPost]
        [ResponseType(typeof(DTOconsumer))]
        public async Task<DTOconsumer> postConsumer(string fName, string lName, string userName, string email, string contactNum, string userPass,  string IDnumber, DateTime DOB, string gender, string maritalStatus, string employmentStatus)
        {
            DTOuser newUser = await postUser(fName, lName, userName, email, contactNum, userPass, 11, IDnumber);

            consumer tmp = new consumer();
            tmp.User_ID = newUser.User_ID;
            tmp.consumerDateOfBirth = DOB;
            tmp.gender = gender;
            tmp.maritalStatus = maritalStatus;
            tmp.employmentStatus = employmentStatus;



            db.consumers.Add(tmp);
            await db.SaveChangesAsync();

            return new DTOconsumer(tmp);
        }

        //post user - return true if user created. userType - 11 for consumer & 21 for reseller
        //[HttpPost]
        //[ResponseType(typeof(DTOreseller))]
        //public async Task<DTOreseller> postReseller(string fName, string lName, string userName, string email, string contactNum, string userPass, int userType, string IDnumber, string cardNumber, string cardExpiration, string cardCVV, string nameOnCard, string bankName, DateTime DOB)
        //{
        //    DTOuser newUser = await postUser(fName, lName, userName, email, contactNum, userPass, 11, IDnumber);

        //    reseller tmp = new reseller();
        //    tmp.User_ID = newUser.User_ID;
        //    tmp.resellerIsValidated = true;
        //    //tmp.cardNumber = cardNumber;
            


        //    db.resellers.Add(tmp);
        //    await db.SaveChangesAsync();


        //    return new DTOreseller(tmp);
        //}


    }
}