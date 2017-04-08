class ReportRecord {

    public constructor(team: Team, school: School, students: Array<Student>, submit: Array<TeamSubmission>) {
        this.team = team;
        this.school = school;
        this.students = students;
        this.submissions = submit;
        this.score = 0;

        if (submit != null) {
            // Count score
            submit.forEach(function (value: TeamSubmission) {
                // Only add to score if greater than 0
                if (value.Score > 0) {
                    this.score += value.Score;
                }
            }.bind(this));
        }
    }

    public team: Team;
    public school: School;
    public students: Array<Student>;
    public submissions: Array<TeamSubmission>;
    public score: number;

    
}