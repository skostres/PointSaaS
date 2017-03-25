/*
 *  A Project class used to retain information sent from the server
 */
var Project = (function () {
    function Project(ID, JudgeZip, ParZip, Name, MaxScore, Desc, IsHidden, Level) {
        this.ID = ID;
        this.RawZipFileJudges = JudgeZip;
        this.RawZipFileParticipants = ParZip;
        this.Name = Name;
        this.MaxScore = MaxScore;
        this.Description = Desc;
        this.Hidden = IsHidden;
        this.Level = Level;
    }
    return Project;
}());
