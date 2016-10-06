using NanofinAPI.Models;
using NanofinAPI.Models.DTOEnvironment;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;

namespace NanofinAPI.Controllers
{
    public class NotificationController : ApiController
    {
        private nanofinEntities db = new nanofinEntities();

        

        [HttpPost]
        public IHttpActionResult SendSMS(string toPhoneNum, string message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string correctFormatNum = getCorrectPhoneNumFormat(toPhoneNum);

           
            sendEmailViaWebApi(correctFormatNum,message);

            return  Ok();
        }


        //NB: phone num format must be: "27thenthenumber" -the getCorrectPhoneNum method below handles this
        private void sendEmailViaWebApi(string toPhoneNum, string message)
        {

                string subject = "nanofin";
                string body = message;
                string FromMail = "margauxfourie@gmail.com";
            string emailTo = toPhoneNum + "@2way.co.za";

                MailMessage mail = new MailMessage();
                SmtpClient client = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(FromMail);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;

                client.Port = 587; 
                
                client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("margauxfourie@gmail.com", "agcptlmlrxxwevhn");
            client.Send(mail);

        }

        //still to fix
        public string getCorrectPhoneNumFormat(string phoneNum)
        {
            string numWithoutZero;
            if (phoneNum[0] == '0')
            {
                //delete 0 and add 27
                numWithoutZero = phoneNum.Substring(1);
                return "27" + numWithoutZero;
            }
            else if (phoneNum[0] == '2' && phoneNum[1] == '7')
            {
                return phoneNum;
            }
            else
            {
                return "Invalid phone number format";
            }
           

        }

        //Get User phone number from ID
        public string getPhoneNumFromUserID(int userID)
        {
           
                 return db.users.Where(u => u.User_ID == userID).Select(u => u.userContactNumber).FirstOrDefault();
        }

        [HttpGet]
        private string generateOTP()//builds up a random OTP of length 6
        {
            int length = 6;

            string numbers = "0123456789";
            Random randNum = new Random();
            string strrandom = string.Empty;
            int noofnumbers = length;
            for (int i = 0; i < noofnumbers; i++)
            {
                int temp = randNum.Next(0, numbers.Length);
                strrandom += temp;
            }

            return strrandom;
        }


        //method to call when transaction takes place: Sends OTP to specified userID, resendcounter should be 0 initially
        [HttpPut]
        public IHttpActionResult sendUserOTPAndSaveOTP(int UserID, bool isResend)
        {
            string userPhoneNum = getPhoneNumFromUserID(UserID);
            string correctPhoneNum = getCorrectPhoneNumFormat(userPhoneNum);
            string newOTP = generateOTP();

            otpview toUpdate = (from c in db.otpviews where c.User_ID == UserID select c).SingleOrDefault();
            DTOotpview dtoOtpView = new DTOotpview(toUpdate);

           

            if (isResend)
            {
                dtoOtpView.otpRetryCount += 1; //increment the retrycount
                if(dtoOtpView.otpRetryCount<3) //still a valid attempt
                {
                    sendEmailViaWebApi(correctPhoneNum, "Hello from Nanofin! Your OTP for your transaction is: " + newOTP);
                    dtoOtpView.otpCode = newOTP;
                    //dtoOtpView.otpRetryCount has been set above
                    dtoOtpView.otpExpirationTime = DateTime.Now.AddMinutes(3);
                    dtoOtpView.otpNextAllowedTime = null; //remains null as long as the user isn't blocked
                    dtoOtpView.otpRecordCreated = DateTime.Now;

                    toUpdate = EntityMapper.updateEntity(toUpdate, dtoOtpView);
                    db.Entry(toUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    return Content(HttpStatusCode.OK, "OTP Resent Successfully");

                }
                if (dtoOtpView.otpRetryCount == 3)//too many attempts: user can request new OTP after a defined time=>blocked
                {
                    dtoOtpView.otpCode = null;
                    dtoOtpView.otpRetryCount = 3;
                    dtoOtpView.otpExpirationTime = null;
                    dtoOtpView.otpNextAllowedTime = DateTime.Now.AddMinutes(5);
                    dtoOtpView.otpRecordCreated = DateTime.Now;

                    toUpdate = EntityMapper.updateEntity(toUpdate, dtoOtpView);
                    db.Entry(toUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    return Content(HttpStatusCode.Forbidden, "User blocked, OTP not Resent");
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            else //not a resend: first time being sent
            {
                Nullable<DateTime> nowTime = DateTime.Now;
                //check if user is blocked: User is not blocked timeNow>next allowed time
                if (dtoOtpView.otpNextAllowedTime == null || (Nullable.Compare(nowTime, dtoOtpView.otpNextAllowedTime) > 0))
                {
                    sendEmailViaWebApi(correctPhoneNum, "Hello from Nanofin! Your OTP for your transaction is: " + newOTP);
                    dtoOtpView.otpCode = newOTP;
                    dtoOtpView.otpRetryCount = 0;
                    dtoOtpView.otpExpirationTime = DateTime.Now.AddMinutes(3);
                    dtoOtpView.otpNextAllowedTime = null; //remains null as long as the user isn't blocked
                    dtoOtpView.otpRecordCreated = DateTime.Now;

                    toUpdate = EntityMapper.updateEntity(toUpdate, dtoOtpView);
                    db.Entry(toUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    return Content(HttpStatusCode.OK,"OTP Sent sucessfully first time");
                }
                else //user is still blocked
                {
                    return Content(HttpStatusCode.Forbidden, "User is still blocked");
                    
                }

            }

        }

        [HttpGet]
        public string checkEnteredOTP(int UserID, string enteredOTP)
        {
            //cases: 1. expired OTP, 2. Valid OTP, 3. Invalid OTP
           
            otpview otpView = (from c in db.otpviews where c.User_ID == UserID select c).SingleOrDefault();
            //co check this user's current OTP expiration time:
            Nullable<DateTime> expiryDate = otpView.otpExpirationTime;
            Nullable<DateTime> nowTime = DateTime.Now;


            if (Nullable.Compare(nowTime, expiryDate) > 0) //now is later than expiry: so OTP has expired!
            {
                return "OTP Expired";//, please retry the transaction"; //access denied?
            }
            else //OTP has not expired
            {
                //check if OTP is valid:
                if (enteredOTP.Equals(otpView.otpCode))
                {
                    //need to update the database so that the expiry becomes now
                    otpView.otpExpirationTime = DateTime.Now;
                    db.Entry(otpView).State = EntityState.Modified;
                    db.SaveChanges();
                    return "OTP Valid";

                  

                }
                else
                {   //generate new otp??
                    sendUserOTPAndSaveOTP(UserID, true);//resend-has 2 paths
                    if (otpView.otpRetryCount < 3)
                    {
                        return "OTP Invalid";
                    }
                    else
                    {
                        return "User Blocked";
                    }
                            
                   
                }

           }

        }

      

    }
}
