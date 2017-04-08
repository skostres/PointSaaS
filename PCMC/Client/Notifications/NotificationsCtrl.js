/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/jquery/jquery.d.ts" />
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var NotificationsCtrl = (function (_super) {
    __extends(NotificationsCtrl, _super);
    function NotificationsCtrl($scope, jQuery, SystemSrv) {
        _super.call(this, $scope, jQuery);
        this.notificationList = new Array();
        this.addNotification("Oh noes", AlertType.Error);
        this.addNotification("Oh yes", AlertType.Success);
        this.addNotification("Oh careful", AlertType.Warning);
        this.addNotification("Oh FYI", AlertType.Info);
    }
    ;
    /*
        Will return a css class name for the corresponding AlertType
    */
    NotificationsCtrl.prototype.getClassOfNotification = function (type) {
        switch (type) {
            case AlertType.Info:
                return 'alert-info';
            case AlertType.Success:
                return 'alert-success';
            case AlertType.Warning:
                return 'alert-warning';
            case AlertType.Error:
                return 'alert-danger';
        }
    };
    NotificationsCtrl.prototype.setExpiredNotification = function (noti) {
        this.jQuery("#" + noti.UUID).fadeOut(500, function () { noti.expired = true; }.bind(noti));
    };
    NotificationsCtrl.prototype.closeMsg = function (index) {
        this.notificationList = this.notificationList.splice(index, 1);
    };
    NotificationsCtrl.prototype.addNotification = function (msg, alertType) {
        var key = SystemSrv.UUID();
        this.notificationList.push(new Notification(msg, alertType, key));
    };
    return NotificationsCtrl;
}(BaseCtrl));
//# sourceMappingURL=NotificationsCtrl.js.map