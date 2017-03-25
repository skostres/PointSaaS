using PCMC.DTO;
using PCMC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCMC.Models
{
    public class ReportModel
    {
        public ReportModel() { }

        public ReportModel(Level lvl)
        {
            this.lvl = lvl;
            teamList = new List<TeamDTO>();
            submissionList = new Dictionary<int, List<TeamSubmissionDTO>>();
            schoolList = new Dictionary<int, SchoolDTO>();
            studentList = new Dictionary<int, List<StudentDTO>>();
        }

        public Level lvl { get; set; }                                          // Report for Level
        public List<TeamDTO> teamList { get; set; }                                // Team List

        public Dictionary<Int32, List<TeamSubmissionDTO>> submissionList { get; set; }   // Team.ID => submission
        public Dictionary<Int32, SchoolDTO> schoolList { get; set; }               // Team.ID => School
        public Dictionary<Int32, List<StudentDTO>> studentList { get; set; }       // Team.ID => List<Student>
    }
}