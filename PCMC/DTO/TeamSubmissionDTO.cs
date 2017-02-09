using PCMC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCMC.DTO
{
    public class TeamSubmissionDTO
    {
        public TeamSubmissionDTO() { }
        public TeamSubmissionDTO(TeamSubmission sub)
        {
            if (sub != null)
            {
                this.ID = sub.ID;
                this.Team = new TeamDTO(sub.Team);
                this.Project = new ProjectDTO(sub.Project);
                this.RawZipSolution = Convert.ToBase64String(sub.RawZipSolution);
                this.Score = sub.Score;
            }
        }
        public int ID { get; set; }
        public TeamDTO Team { get; set; } // Team who made submission.
        public ProjectDTO Project { get; set; } // The Project Solution being submitted for
        public string RawZipSolution { get; set; } //File being submitted.

        public int Score { get; set; }
    }
}