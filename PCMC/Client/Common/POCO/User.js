var User = (function () {
    function User() {
        this.FirstName = null;
        this.LastName = null;
        this.ID = null;
        this.Email = null;
        this.Username = null;
        this.Password = null;
        this.UserRole = null;
        this.setRoleStr();
    }
    User.prototype.setRoleStr = function () {
        var str;
        switch (this.UserRole) {
            case Role.Admin:
                str = "Admin";
                break;
            case Role.User:
                str = "User";
                break;
            default: str = "Unknown Role";
        }
        this.roleStr = str;
    };
    User.prototype.destroy = function () {
        this.FirstName = null;
        this.LastName = null;
        this.ID = null;
        this.Email = null;
        this.Username = null;
        this.Password = null;
        this.UserRole = null;
        this.roleStr = null;
    };
    User.prototype.set = function (userId, LastName, FirstName, Email, Password, Username, userRole) {
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Email = Email;
        this.ID = userId;
        this.Username = Username;
        this.Password = Password;
        this.UserRole = userRole;
        this.setRoleStr();
    };
    return User;
}());
//# sourceMappingURL=User.js.map