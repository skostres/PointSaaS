using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PCMC.Entities
{
    public class JudgeTeamMap
    {
        [Key]
        public int ID { get; set; }
        public User Judge { get; set; }
        public Team Team { get; set; }
    }
}