using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCMC.Entities;

namespace PCMC.DTO
{
    public class InstructorDTO
    {
    
        public InstructorDTO(Instructor instructor)
        {
            this.ID = instructor.ID;
            this.Name = instructor.Name;
            this.Email = instructor.Email;
            this.Phone = instructor.Phone;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}