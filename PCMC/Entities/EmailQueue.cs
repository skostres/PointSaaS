using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PCMC.Entities
{
    public class EmailQueue
    {
        [Key]
        public int ID { get; set; }
        public User Owner { get; set; }
        public Instances Instance { get; set; }
        public DateTime FutureTime { get; set; }
        public bool IsReady { get; set; }
        public EmailTemplates Template { get; set; }

    }
}