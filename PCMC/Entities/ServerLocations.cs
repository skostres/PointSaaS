using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace PCMC.Entities
{
    public class ServerLocations
    {
        [Key]
        public int ID { get; set; }
        public string LocationName { get; set; }
        public string ServerIP { get; set; }
        public string ServerPort { get; set; }
        public string ServerURL { get; set; }
    }
}