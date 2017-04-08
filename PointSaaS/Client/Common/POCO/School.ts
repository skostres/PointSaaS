class School {

    public constructor(id: number, name: string) {
        this.ID = id;
        this.Name = name;
    }

    public ID: number;
    public Name: string;
    // Won't really be used...
    public Instructor: Instructor;
    public Teams: Array<Team>;

}
