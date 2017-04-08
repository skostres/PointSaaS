using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PCMC.Entities
{
    public class GlobalSettings
    {
        [Key]
        
        public string ParamName { get; set; }
        public string ParamValue { get; set; }
    }
}