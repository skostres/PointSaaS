﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PCMC.Entities
{
    public class InstanceTypes
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string SysUser { get; set; }
        public string SysPass { get; set; }
    }
}