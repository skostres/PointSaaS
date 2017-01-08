using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PCMC.Entities
{
    public class Project
    {
        public int ID { get; set; }
        public byte[] RawZipFile { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Hidden { get; set; }

    }
}