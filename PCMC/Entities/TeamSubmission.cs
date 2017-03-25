using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PCMC.Entities
{
    public class TeamSubmission
    {
        public int ID { get; set; }
        public Team Team { get; set; } // Team who made submission.
        public Project Project { get; set; } // The Project Solution being submitted for
        public byte[] RawZipSolution { get; set; } //File being submitted.

        public int Score { get; set; }
        public string GraderComment { get; set; }
    }
}