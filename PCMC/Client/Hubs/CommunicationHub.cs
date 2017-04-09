using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using PCMC.DTO;
using PCMC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PCMC.Models;

namespace PCMC.Client.Hubs
{
    public class CommunicationHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<CommunicationHub>();
        protected static ModelADO db = new ModelADO();


        /*
         * This hub action will serve to identify a connection and assign it to groups.
         * Groups are used to inform authorized users of certain data changes, such as project submissions and grade assignments.
         */
        public void Subscribe(UserDTO usr)
        {
            IQueryable<User> userDB = db.User.Where(c => c.Username == usr.Username && c.Password == usr.Password);

            if (userDB != null && userDB.Count() == 1)
            {
                User userFromDB = userDB.First();
                switch (userFromDB.Role)
                {
                    case UserRole.Admin:
                        // Admin's hear all
                        Groups.Add(Context.ConnectionId, "Admins");
                        break;
                    case UserRole.User:
                        Groups.Add(Context.ConnectionId, "Users");
                        break;
                }
            }
            // Do nothing otherwise
        }

       public void PollInstanceList(UserDTO usr)
        {
            User userDB = HelperMethods.GetUser(usr);
            if (HelperMethods.IsAdmin(usr))
            {
                Clients.Caller.updateInstanceList(db.Instances.Include("InstanceType").Include("Owner").Include("LocationInstalled").ToList());
            } else if (userDB != null && userDB.Role == UserRole.User) 
            {
                Clients.Caller.updateInstanceList(db.Instances.Include("InstanceType").Include("Owner").Include("LocationInstalled").Where(p => p.Owner.ID == userDB.ID).ToList());
            }
        }

        public void PollServerLocations(UserDTO usr)
        {
            User userDB = HelperMethods.GetUser(usr);
            if (userDB != null)
            {
                Clients.Caller.updateServerLocationList(db.ServerLocations.ToList());
            }
        }

        public void PollInstanceTypes(UserDTO usr)
        {
            User userDB = HelperMethods.GetUser(usr);
            if (userDB != null)
            {
                Clients.Caller.updateInstanceTypeList(db.InstanceTypes.ToList());
            }
        }

        public void RequestInstance(UserDTO usr, AddInstanceModel model)
        {
            User userDB = HelperMethods.GetUser(usr);
            if (userDB != null)
            {
                Instances ins = new Instances { InstanceType = db.InstanceTypes.Find(model.InstanceType),
                    LocationInstalled = db.ServerLocations.Find(model.ServerLocation), Owner = db.User.Find(userDB.ID), URL = model.URLExtension, DeleteDate = DateTime.Now};
                ins = db.Instances.Add(ins);
                db.SaveChanges();

                Clients.Caller.triggerUpdate();
                Clients.Caller.notifyChange("Added a instance!", MsgTypeDTO.SUCCESS);
                db.EmailQueue.Add(new EmailQueue { Instance = db.Instances.Find(ins.ID), IsReady = true, Template = db.EmailTemplates.Find(ins.InstanceType.ID), Owner = db.User.Find(userDB.ID),FutureTime=DateTime.Now });
                string batch_create = @"C:\PointSaaS\Images\" + ins.InstanceType.ID + @"\CREATE.cmd";
                db.SaveChanges();
                System.Diagnostics.Process.Start(batch_create, ""+ins.ID+" "+ins.URL);
                
            }
            else
            {
                Clients.Caller.notifyChange("Error", MsgTypeDTO.ERROR);
            }
            
        }

    }
}