class Student {
    public constructor(id: number, usr: User, school: School, team: Team) {
        this.ID = id;
        this.User = usr;
        this.SchoolEnrolled = school;
        this.TeamAssigned = team;
    }

    public ID: number;
    public User: User;
    public SchoolEnrolled: School;
    public TeamAssigned: Team;
}