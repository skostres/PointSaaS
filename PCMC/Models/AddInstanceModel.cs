using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCMC.Entities;

namespace PCMC.Models
{
    public class AddInstanceModel
    {
        public int ServerLocation { get; set; }
        public string URLExtension { get; set; }
        public int InstanceType { get; set; }
        public bool IsValidExtension { get; set; }
        public ServerLocations[] ServerLocationOptions { get; set; }
        public InstanceTypes[] InstanceTypeOptions { get; set; }

        /* public ServerLocation: ServerLocation;
         public IsValidExtension:boolean;

    public ServerLocationOptions: Array<ServerLocation>;
    public InstanceTypeOptions: Array<InstanceType>;
     */
    }
}