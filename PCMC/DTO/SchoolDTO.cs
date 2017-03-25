using PCMC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCMC.DTO
{
    public class SchoolDTO
    {
        public SchoolDTO() {}
        public SchoolDTO(School other)
        {
            this.ID = other.ID;
            this.Name = other.Name;
            this.Instructor = new InstructorDTO(other.Instructor);
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public InstructorDTO Instructor { get; set; }
    }
}