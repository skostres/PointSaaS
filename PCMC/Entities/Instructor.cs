using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCMC.Entities
{
    public class Instructor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}