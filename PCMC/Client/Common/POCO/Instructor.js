/*
 *  An Instructor class used to retain information sent from the server
 */
var Instructor = (function () {
    function Instructor(id, name, email, phone) {
        this.ID = id;
        this.Name = name;
        this.Email = email;
        this.Phone = phone;
    }
    return Instructor;
}());
