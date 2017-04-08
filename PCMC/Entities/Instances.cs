using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PCMC.Entities
{
    public class Instances
    {
        [Key]
        public int ID { get; set; }
        public User Owner { get; set; }
        public ServerLocations LocationInstalled { get; set; }
        public InstanceTypes InstanceType { get; set; }
        public DateTime DeleteDate { get; set; }
        public string URL { get; set; }

    }
}