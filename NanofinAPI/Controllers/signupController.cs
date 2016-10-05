using NanofinAPI.Models;
using NanofinAPI.Models.DTOEnvironment;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public async Task<DTOuser> postUser(string fName, string lName, string userName, string email,string contactNum, string userPass, int userType, string IDnumber ) 
        {
            user tmp = new user();
            tmp.userFirstName = fName;
            tmp.userLastName = lName;
            //email is taken return empty user object before changes to db made
            if (await isUsernameTaken(userName))
            {
                tmp.userFirstName = null;
                tmp.userLastName = null;
                tmp.userName = "-1";
                return new DTOuser(tmp);
            }else{ tmp.userName = userName; }
            
            if (await isEmailTaken(email))
            {
                tmp.userFirstName = null;
                tmp.userLastName = null;
                tmp.userEmail = "-1";
                return new DTOuser(tmp);
            }else{ tmp.userEmail = email; }

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


        public async Task<IHttpActionResult> postConsumer(string fName, string lName, string userName, string email, string contactNum, string userPass, string IDnumber, DateTime DOB, string gender, string maritalStatus, string employmentStatus)
        {
            DTOuser newUser = await postUser(fName, lName, userName, email, contactNum, userPass, 11, IDnumber);

            if (newUser.userFirstName == null && newUser.userLastName == null)
            {
                if(newUser.userName == "-1")
                {
                    return BadRequest("Username taken");
                }else if (newUser.userEmail == "-1")
                {
                    return BadRequest("email taken");
                }
                else
                {
                    return BadRequest("Invalid user");
                }
            }

            consumer tmp = new consumer();
            tmp.User_ID = newUser.User_ID;
            tmp.consumerDateOfBirth = DOB;
            tmp.gender = gender;
            tmp.maritalStatus = maritalStatus;
            tmp.employmentStatus = employmentStatus;



            db.consumers.Add(tmp);
            await db.SaveChangesAsync();

            return Ok();
        }

        //post user - return true if user created. userType - 11 for consumer & 21 for reseller
        public async Task<IHttpActionResult> postReseller(string fName, string lName, string userName, string email, string contactNum, string userPass, string IDnumber, string cardNumber, string cardExpiration, string cardCVV, string nameOnCard, string bankName, DateTime DOB)
        {
            DTOuser newUser = await postUser(fName, lName, userName, email, contactNum, userPass, 21, IDnumber);

            if (newUser.userFirstName == null && newUser.userLastName == null)
            {
                if (newUser.userName == "-1")
                {
                    return BadRequest("Username taken");
                }
                else if (newUser.userEmail == "-1")
                {
                    return BadRequest("email taken");
                }
                else
                {
                    return BadRequest("Invalid user");
                }
            }

            reseller tmp = new reseller();
            tmp.Reseller_ID = newUser.User_ID;
            tmp.User_ID = newUser.User_ID;
            tmp.resellerIsValidated = true;
            tmp.cardNumber = cardNumber;
            tmp.cardExpirationMonth_Year = cardExpiration;
            tmp.cardCVV = cardCVV;
            tmp.nameOnCard = nameOnCard;
            tmp.resellerBankName = bankName;
            tmp.resellerDateOfBirth = DOB;
            tmp.isSharingLocation = "false";

            tmp.resellerBankBranchName = "";
            tmp.resellerBankAccountNumber = "";
            tmp.resellerBankBranchCode = "";

            db.resellers.Add(tmp);
            db.SaveChanges();

            return Ok();
        }

        public async Task<bool> isEmailTaken(string email)
        {
            return await db.users.AnyAsync(l => l.userEmail == email);
        }

        public async Task<bool> isUsernameTaken(string userName)
        {
            return await db.users.AnyAsync(l => l.userName == userName); ;
        }


    }
}