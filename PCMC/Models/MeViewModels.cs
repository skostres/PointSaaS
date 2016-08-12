using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PCMC.Models
{
    // Models returned by MeController actions.
    public class GetViewModel
    {
        public string Hometown { get; set; }
    }
}