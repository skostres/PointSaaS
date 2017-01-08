using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace PCMC.Entities
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Level lvl { get; set; }
    }
}