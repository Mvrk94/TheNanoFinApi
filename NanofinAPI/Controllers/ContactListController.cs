using NanofinAPI.Models;
using NanofinAPI.Models.DTOEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace NanofinAPI.Controllers
{
    public class ContactListController : ApiController
    {

        database_nanofinEntities db = new database_nanofinEntities();
        //search for beneficiary based on mobile number
        [HttpGet]
        public DTOuser getUserFromContactNumberAndUserNameOrEmail(string userNameOrEmail,string contactNum)
        {
            //check that a matching userName/Email AND with matching contactNumber is registered, AS A CONSUMER (usertypeID 11) - not resellers.
            if(db.users.Any(u => (u.userName == userNameOrEmail || u.userEmail == userNameOrEmail)&& u.userContactNumber==contactNum && u.userType==11))
            {
                user usr = (from c in db.users where( c.userName == userNameOrEmail||c.userEmail==userNameOrEmail) && c.userContactNumber==contactNum select c).FirstOrDefault();
                DTOuser toReturn = new DTOuser(usr);
                return toReturn;
            }
            //returns null if user not registered/incorrect details
            return null;
   
        }

        //input (user: email/cellphone number/ user name) return user ID; return -1 if user details passed are not in User table
        public int getUserDetailsToUserID(String input)
        {
            if (db.users.Any(list => list.userName == input || list.userEmail == input || list.userContactNumber == input))
            {
                return db.users.Where(list => list.userName == input || list.userEmail == input || list.userContactNumber == input).Select(list => list.User_ID).FirstOrDefault();
            }
            else
                return -1;
        }

        //Add a contact: POST : need userID and the contactUserID
        [HttpPost]
        [ResponseType(typeof(DTOcontactlist))]
        public async Task<DTOcontactlist> PostContact(DTOcontactlist newDTO)
        {
            contactlist newContact = EntityMapper.updateEntity(null, newDTO);
            db.contactlists.Add(newContact);
            await db.SaveChangesAsync();
            return newDTO;
        }

        //Delete a contact
        
        [ResponseType(typeof(DTOcontactlist))]
        [HttpDelete]
        public async Task<IHttpActionResult> deleteContact(int UserID, int ContactUserID)
        {
            var contactToDel = (from c in db.contactlists where c.UserID == UserID && c.ContactsUserID==ContactUserID select c).SingleOrDefault();

            if (contactToDel == null)
            {
                return NotFound();
            }

            DTOcontactlist temp = new DTOcontactlist(contactToDel);

            db.contactlists.Remove(contactToDel);
            await db.SaveChangesAsync();

            return Ok(temp);
        }

        //Get a user's contactList
        [HttpGet]
        public List<DTOcontactlist> getUsersContactList_IDsOnly(int UserID)
        {
            List<DTOcontactlist> dtoContactList = new List<DTOcontactlist>();
            List<contactlist> list = (from l in db.contactlists where l.UserID == UserID select l).ToList();
            if (list == null)
            {
                return null;
            }

            foreach (contactlist l in list)
            {
                dtoContactList.Add(new DTOcontactlist(l));
            }
            return dtoContactList;
        }

        public List<DTOuser> getUsersContactListWithDetails(int UserID)
        {
            List<DTOuser> dtoContactDetailsList = new List<DTOuser>();
            List<contactlist> list = (from l in db.contactlists where l.UserID == UserID select l).ToList();
            foreach (contactlist l in list)
            {
                user entityUser = (from u in db.users where u.User_ID == l.ContactsUserID select u).SingleOrDefault();
                dtoContactDetailsList.Add(new DTOuser(entityUser));
            }

            return dtoContactDetailsList;
        }

        //Check if a contact already exists for this user..true/false? -been added to postContact method logic
        [HttpGet]
        public bool contactAlreadyExists(Nullable<int> UserID, Nullable<int> contactID)
        {           
            if(db.contactlists.Any(c=> c.UserID ==UserID && c.ContactsUserID == contactID))
            {
                return true;
            }
            return false;
        }
    }
}
