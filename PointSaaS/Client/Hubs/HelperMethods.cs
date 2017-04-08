using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using PCMC.DTO;
using PCMC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PCMC.Client.Hubs
{
   
    public class HelperMethods
    {
        protected static ModelADO db = new ModelADO();

        private bool IsAdmin(UserDTO usr)
        {
            
            User userFromDB = GetUser(usr);
            if (userFromDB != null && userFromDB.Role == UserRole.Admin)
            {
                return true;
            }
            return false;
        }

        private User GetUser(UserDTO usr)
        {
            User userFromDB = null;
            IQueryable<User> userDB = db.User.Where(c => c.Username == usr.Username && c.Password == usr.Password);
            if (userDB != null && userDB.Count() == 1)
            {
                userFromDB = userDB.First();
            }

            return userFromDB;
        }

        private bool IsParticipant(UserDTO usr)
        {
            IQueryable<User> userDB = db.User.Where(c => c.Username == usr.Username && c.Password == usr.Password);

            if (userDB != null && userDB.Count() == 1)
            {
                User userFromDB = userDB.First();
                if (userFromDB.Role == UserRole.User)
                {
                    return true;
                }
            }
            return false;
        }
    }
}