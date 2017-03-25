var ReportRecord = (function () {
    function ReportRecord(team, school, students, submit) {
        this.team = team;
        this.school = school;
        this.students = students;
        this.submissions = submit;
        this.score = 0;
        if (submit != null) {
            // Count score
            submit.forEach(function (value) {
                // Only add to score if greater than 0
                if (value.Score > 0) {
                    this.score += value.Score;
                }
            }.bind(this));
        }
    }
    return ReportRecord;
}());
//# sourceMappingURL=ReportRecord.js.map