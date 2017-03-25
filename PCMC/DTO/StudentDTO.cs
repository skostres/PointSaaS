using PCMC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCMC.DTO
{
    public class StudentDTO
    {
        public StudentDTO() {}
        public StudentDTO(Student stu)
        {
            this.ID = stu.ID;
            this.User = new UserDTO(stu.User);
            this.SchoolEnrolled = new SchoolDTO(stu.SchoolEnrolled);
            this.TeamAssigned = new TeamDTO(stu.TeamAssigned);
        }

        public int ID { get; set; }
        public UserDTO User { get; set; }
        public SchoolDTO SchoolEnrolled { get; set; }
        public TeamDTO TeamAssigned { get; set; }
    }
}