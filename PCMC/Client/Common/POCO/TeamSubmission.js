var TeamSubmission = (function () {
    function TeamSubmission(id, file, project, team, score, comment) {
        this.ID = id;
        this.RawZipSolution = file;
        this.Project = project;
        this.Team = team;
        this.Score = score;
        this.GraderComment = comment;
    }
    return TeamSubmission;
}());
