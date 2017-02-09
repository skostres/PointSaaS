using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PCMC.Entities
{
    /**
     * Class entity model for projects. Entity Framework will generate a schema using code first approach.
     */
    public class Project
    { 
        public int ID { get; set; }                 //DB key for the project.
        public byte[] RawZipFileJudges { get; set; }//The zip file for judge instructions
        public byte[] RawZipFileParticipants { get; set; }//The zip file of participant instructions
        public string Name { get; set; }            // A Friendly name for the project .. Project#1 - LinkedLists
        public string Description { get; set; }     // A short description of the project ... testing knowledge of LinkedLists..
        public int MaxScore { get; set; }           // The maximum score participants may obtain (In general a score is bounded [-1,MaxScore] where -1 represents ungraded
        public bool Hidden { get; set; }            // Whether this project is hidden from participants
        public Level Level { get; set; }            // The level associated with the project
    }
}