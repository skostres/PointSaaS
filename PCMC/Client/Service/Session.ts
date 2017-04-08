// Class will be used to store current user credentials
class Session {
    public currentUser: User = new User();
    public constructor() { }
    public create(userId: number, LastName: string, FirstName: string, Password: string, Email: string, Username: string, userRole: Role) {
        this.currentUser.set(userId, LastName, FirstName, Email, Password, Username, userRole);
    }
    public destroy() {
        this.currentUser.destroy()
    }
}