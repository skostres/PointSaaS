using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCMC.Entities
{
    public class School
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Instructor Instructor { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}