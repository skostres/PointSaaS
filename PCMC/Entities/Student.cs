using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PCMC.Entities
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        public User User { get; set; }
        public School SchoolEnrolled { get; set; }
        public Team TeamAssigned { get; set; }
    }
}