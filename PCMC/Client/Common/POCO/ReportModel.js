var ReportModel = (function () {
    function ReportModel(lvl, teamList, submissionList, schoolList, studentList) {
        this.lvl = lvl;
        this.teamList = teamList;
        this.submissionList = submissionList;
        this.schoolList = schoolList;
        this.studentList = studentList;
    }
    return ReportModel;
}());
