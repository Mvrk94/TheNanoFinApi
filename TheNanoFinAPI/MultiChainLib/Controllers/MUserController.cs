using MultiChainLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TheNanoFinAPI.Models;
using TheNanoFinAPI.Models.DTOEnvironment;
using NanoFinAPI_6_07.Controllers.testManager;
using System.Data.Entity;

namespace TheNanoFinAPI.MultiChainLib.Controllers
{
    public class MUserController
    {

        private  nanofinEntities db = new nanofinEntities();
        private MultiChainClient client;
        private String userAddress;
        private int user_ID;

        public MUserController(int user_ID)
        {
            client = new MultiChainClient("188.166.170.248", 6492, false, "multichainrpc", "AYBR44NDe7VdSWXHJCrR2i2xhCcnByorzHy6f6vaczTd", "NanoFinBlockChain");
            this.user_ID = user_ID;
        }

        public MUserController(int user_ID, MultiChainClient cli)
        {
            client = cli;
            this.user_ID = user_ID;
        }


        public async Task<MUserController> init()
        {
            userAddress = await getUserAddress();
            return this;
        }
        //returns users associated address. if user has no address -> give user address -> return new address.
        public async Task<string> getUserAddress()
        {
            if (db.users.Where(list => list.User_ID == user_ID).Any())
            {
                return db.users.Where(list => list.User_ID == user_ID).Select(list => list.blockchainAddress).FirstOrDefault();
            }
            else
            {
                //get new address, get user for which address does not exist, add address to user record.
                var newAddress = await client.GetNewAddressAsync();
                newAddress.AssertOk();
                var userToEdit = db.users.Single(o => o.User_ID == user_ID);
                userToEdit.blockchainAddress = newAddress.Result;
                db.Entry(userToEdit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return newAddress.Result;
            }
        }
        
        //true if user has specified permission
        public async Task<Boolean> hasPermission(BlockchainPermissions permissionType)
        {
            var listPermissions = await client.ListPermissions(permissionType);
            listPermissions.AssertOk();
            foreach (var permission in listPermissions.Result)
            {
                if(permission.Address.Equals(userAddress) && permission.Type.Equals(permissionType.ToString().ToLower()) ) {
                    return true;
                }
            }
            return false;
        }
        //true if user has all specified permissions
        public async Task<Boolean> hasPermissions(params BlockchainPermissions[] paramPermissions)
        {
            for(int i = 0; i < paramPermissions.Length; i++)
            {
                if (await hasPermission(paramPermissions[i]) == false)
                {
                    return false;
                }
            }
            return true;
        }
        //grant user all specified permissions
        public async void grantPermissions(params BlockchainPermissions[] paramPermissions)
        {
            for (int i = 0; i < paramPermissions.Length; i++)
            {
                if (await hasPermission(paramPermissions[i]) == false)
                {
                    var perms = await client.GrantAsync(new List<string>() { userAddress }, paramPermissions[i]);
                    perms.AssertOk();
                }       
            }
            
        }

        //remove all double quotation marks from string.
        public static String removeQuotes(String input)
        {
            return input.Replace("\"", String.Empty);
        }

        public String propertyUserAddress() { return userAddress; }
        public int propertyUserID() { return user_ID; }


    }
}