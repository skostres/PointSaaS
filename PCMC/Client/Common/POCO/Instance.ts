class Instance {

    public constructor(ID: number, Owner: User, LocationInstalled: ServerLocation, DeleteDate: any, URL: string) {
        this.ID = ID;
        this.Owner = Owner;
        this.LocationInstalled = LocationInstalled;
        this.DeleteDate = DeleteDate;
        this.URL = URL;
    }

    public ID: number;
    public Owner: User;
    public LocationInstalled: ServerLocation;
    public InstanceType: InstanceType;
    public DeleteDate: any;
    public URL:string;


}