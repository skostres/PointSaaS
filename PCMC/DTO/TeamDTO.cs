using PCMC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCMC.DTO
{
    public class TeamDTO
    {
        public TeamDTO() { }
        public TeamDTO (Team team)
        {
            this.ID = team.ID;
            this.Name = team.Name;
            this.lvl = team.lvl;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public Level lvl { get; set; }
    }
}