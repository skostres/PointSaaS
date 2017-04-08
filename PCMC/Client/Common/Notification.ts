enum AlertType {
    Error,
    Success,
    Info,
    Warning
}

// Class will be used for pop-up alerts within the app
class Notification {
    public UUID: string;
    public message: string;
    public type: AlertType;
    public expired: boolean;

    public constructor(msg: string, alert: AlertType, UUID: string) {
        this.type = alert;
        this.message = msg;
        this.expired = false;
        this.UUID = UUID;
    }
}