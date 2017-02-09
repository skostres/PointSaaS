using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCMC.Entities;

namespace PCMC.DTO
{
    /**
     * A Data Transfer Object of a Project used between the Client and Browser
     */
    public class ProjectDTO
    {

        /// <summary>
        /// CONSTRUCTORS
        /// </summary>

        public ProjectDTO() { }
        public ProjectDTO(int ID, Byte[] JudgeZip, Byte[] ParZip, string Name, int MaxScore, string Desc, bool IsHidden)
        {
            this.ID = ID;
            this.RawZipFileJudges = BitConverter.ToString(JudgeZip).Replace("-", "");
            this.RawZipFileParticipants = BitConverter.ToString(ParZip).Replace("-", "");
            this.Name = Name;
            this.MaxScore = MaxScore;
            this.Description = Desc;
            this.Hidden = IsHidden;
        }

        /**
         * Entity to DTO object
         */
        public ProjectDTO(Project proj)
        {
            if (proj != null)
            {
                this.ID = proj.ID;
                this.RawZipFileJudges = Convert.ToBase64String(proj.RawZipFileJudges);
                this.RawZipFileParticipants = Convert.ToBase64String(proj.RawZipFileParticipants);
                this.Name = proj.Name;
                this.Description = proj.Description;
                this.MaxScore = proj.MaxScore;
                this.Hidden = proj.Hidden;
                this.Level = proj.Level;
            }
        }



        public int ID { get; set; }
        public string RawZipFileJudges { get; set; }
        public string RawZipFileParticipants { get; set; }
        public string Name { get; set; }
        public int MaxScore { get; set; }
        public string Description { get; set; }
        public bool Hidden { get; set; }    // Project's visibility for participants

        public Level Level { get; set; }    // Level of this project: Introduction / Advanced

        public Project toProjectType()
        {
            return new Project
            {
                ID = this.ID,
                Description = this.Description,
                Hidden = this.Hidden,
                Name = this.Name,
                MaxScore = this.MaxScore,
                RawZipFileJudges = Convert.FromBase64String(this.RawZipFileJudges),
                RawZipFileParticipants = Convert.FromBase64String(this.RawZipFileParticipants),
                Level = this.Level
            };
        }
    }
}