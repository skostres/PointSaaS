using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace PCMC.Entities
{
    public class EmailTemplates
    {
        [Key]
        public int ID { get; set; }
        public string TemplateFileName { get; set; }
        public string EmailSubject { get; set; }
        public InstanceTypes InstanceType { get; set; }
    }
}