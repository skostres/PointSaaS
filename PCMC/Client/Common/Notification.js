var AlertType;
(function (AlertType) {
    AlertType[AlertType["Error"] = 0] = "Error";
    AlertType[AlertType["Success"] = 1] = "Success";
    AlertType[AlertType["Info"] = 2] = "Info";
    AlertType[AlertType["Warning"] = 3] = "Warning";
})(AlertType || (AlertType = {}));
// Class will be used for pop-up alerts within the app
var Notification = (function () {
    function Notification(msg, alert, UUID) {
        this.type = alert;
        this.message = msg;
        this.expired = false;
        this.UUID = UUID;
    }
    return Notification;
}());
//# sourceMappingURL=Notification.js.map