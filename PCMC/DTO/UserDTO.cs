using PCMC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCMC.DTO
{
    public class UserDTO
    {
        public UserDTO (User usr)
        {
            this.ID = usr.ID;
            this.LastName = usr.LastName;
            this.FirstName = usr.FirstName;
            this.Email = usr.Email;
            this.Role = (UserRoleDTO) usr.Role;
        }
        public UserDTO() { }

        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public UserRoleDTO Role { get; set; }
    }
}