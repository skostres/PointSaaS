/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../scripts/typings/jquery/jquery.d.ts" />

class NotificationsCtrl extends BaseCtrl {

    public notificationList: Array<Notification>;
    public constructor($scope, jQuery, SystemSrv: SystemSrv) {
        super($scope, jQuery);
        this.notificationList = new Array<Notification>();
        this.addNotification("Oh noes", AlertType.Error);
        this.addNotification("Oh yes", AlertType.Success);
        this.addNotification("Oh careful", AlertType.Warning);
        this.addNotification("Oh FYI", AlertType.Info);
    };

    /*
        Will return a css class name for the corresponding AlertType
    */
    public getClassOfNotification(type: AlertType) : string {
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
    }

    public setExpiredNotification(noti: Notification) : void {
        this.jQuery("#" + noti.UUID).fadeOut(500, function () {  noti.expired = true; }.bind(noti));
    }

    public closeMsg(index: number): void {
        this.notificationList = this.notificationList.splice(index, 1);
    }   

    public addNotification(msg: string, alertType: AlertType) {
        var key = SystemSrv.UUID();
        this.notificationList.push(new Notification(msg, alertType, key));
    }
}