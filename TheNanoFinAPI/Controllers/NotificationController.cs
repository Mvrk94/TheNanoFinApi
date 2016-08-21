using NanofinAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;
using TheNanoFinAPI.Models;

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

    }
}
