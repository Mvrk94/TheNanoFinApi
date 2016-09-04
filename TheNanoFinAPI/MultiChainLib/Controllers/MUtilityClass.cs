using MultiChainLib;
using MultiChainLib.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TheNanoFinAPI.Models;

namespace TheNanoFinAPI.MultiChainLib.Controllers
{
    public class MUtilityClass
    {
        private static nanofinEntities db = new nanofinEntities();

        //returns users associated address. if user has no address -> give user address -> return new address.
        public static async Task<string> getAddress(int userID, MultiChainClient client)
        {
            if (db.users.Where(list => list.User_ID == userID).Any())
            {
                return db.users.Where(list => list.User_ID == userID).Select(list => list.blockchainAddress).FirstOrDefault();
            }
            else
            {
                //get new address, get user for which address does not exist, add address to user record.
                var newAddress = await client.GetNewAddressAsync();
                newAddress.AssertOk();
                var userToEdit = db.users.Single(o => o.User_ID == userID);
                userToEdit.blockchainAddress = newAddress.Result;
                db.Entry(userToEdit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return newAddress.Result;
            }
        }

    }
}