// Class will be used to store current user credentials
var Session = (function () {
    function Session() {
        this.currentUser = new User();
    }
    Session.prototype.create = function (userId, LastName, FirstName, Password, Username, userRole) {
        this.currentUser.set(userId, LastName, FirstName, Password, Username, userRole);
    };
    Session.prototype.destroy = function () {
        this.currentUser.destroy();
    };
    return Session;
}());
