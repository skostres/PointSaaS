class TeamSubmission {

    public constructor(id: number, file: string, project: Project, team: Team, score: number, comment: string) {
        this.ID = id;
        this.RawZipSolution = file;
        this.Project = project;
        this.Team = team;
        this.Score = score;
        this.GraderComment = comment;
    }

    public ID: number;
    public Team: Team;
    public Project: Project;
    public RawZipSolution: string; // This will be binary encoded in base64
    public Score: number;
    public GraderComment: string;
}