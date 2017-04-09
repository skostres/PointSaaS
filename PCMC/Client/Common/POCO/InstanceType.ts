class InstanceType {

    public constructor(ID: number, Name: string, SysUser: string, SysPass: string) {
        this.ID = ID;
        this.Name = Name;
        this.SysPass = SysPass;
        this.SysUser = SysUser;
    }

    public ID : number;
    public Name: string;
    public SysUser: string;
    public SysPass: string;
}