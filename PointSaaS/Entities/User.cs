using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PCMC.Entities
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public UserRole Role { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Username { get; set; }
        
    }
}