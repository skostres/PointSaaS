interface idToTeamSubmission {
    [id: number]: Array<TeamSubmission>;
}

interface idToSchool {
    [id: number]: School;
}

interface idToStudentList {
    [id: number]: Array<Student>;
}


class ReportModel {

    public constructor(lvl: Level, teamList: Array<Team>, submissionList: idToTeamSubmission, schoolList: idToSchool, studentList: idToStudentList) {
        this.lvl = lvl;
        this.teamList = teamList;
        this.submissionList = submissionList;
        this.schoolList = schoolList;
        this.studentList = studentList;
    }

    public lvl: Level; // Report for Level
    public teamList: Array<Team>; // Team List
    public submissionList: idToTeamSubmission;
    public schoolList: idToSchool;
    public studentList: idToStudentList;

}