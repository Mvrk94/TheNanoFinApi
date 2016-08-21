using NanofinAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TheNanoFinAPI.Models.DTOEnvironment
{ 
    public class DTOUsersGetNoPass
    {
        public int User_ID { get; set; }
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string userContactNumber { get; set; }
        public Nullable<bool> userIsActive { get; set; }
        public Nullable<int> userTypeID { get; set; }
        public string userActivationType { get; set; }

        public string userPassword { get; set; }
        public string IDnumber { get; set; }

        public DTOUsersGetNoPass() { }

        public DTOUsersGetNoPass(user entityObject)
        {
            this.User_ID = entityObject.User_ID;
            this.userFirstName = entityObject.userFirstName;
            this.userLastName = entityObject.userLastName;
            this.userName = entityObject.userName;
            this.userEmail = entityObject.userEmail;
            this.userContactNumber = entityObject.userContactNumber;
            this.userIsActive = entityObject.userIsActive;
            this.userTypeID = entityObject.userType;
            this.userActivationType = entityObject.userActivationType;
            this.IDnumber = entityObject.IDnumber;
            this.userPassword = entityObject.userPassword;
        }

    }


    public class SessionObj
    {
      //  public string name;
        public string type;
     //   public string userName;

        public SessionObj(user nanofinUser)
        {
        //    name = nanofinUser.userFirstName;
            type = nanofinUser.usertype1.UserTypeDescription;
         //   userName = nanofinUser.userName;
        }
    }
}