class User {
    public constructor() {
        this.FirstName = null;
        this.LastName = null;
        this.ID = null;
        this.Username = null;
        this.Password = null;
        this.UserRole = null;
        this.setRoleStr();
    }
    public ID: number;
    public LastName: string;
    public FirstName: string;
    public Password: string;
    public Username: string;
    public UserRole: Role;
    ////////////////////////
    public roleStr: string;

    private setRoleStr() {
        var str; 
        switch (this.UserRole) {
            case Role.Admin: str = "Admin"; break;
            case Role.Judge: str = "Judge"; break;
            case Role.Participant: str = "Participant"; break;
            default: str = "Unknown Role";
        }

        this.roleStr = str;
    }

    public destroy() {
        this.FirstName = null;
        this.LastName = null;
        this.ID = null;
        this.Username = null;
        this.Password = null;
        this.UserRole = null;
        this.roleStr = null;
    }
    public set(userId: number, LastName: string, FirstName: string, Password: string, Username: string, userRole: Role) {
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.ID = userId;
        this.Username = Username;
        this.Password = Password;
        this.UserRole = userRole;
        this.setRoleStr();
    }
}