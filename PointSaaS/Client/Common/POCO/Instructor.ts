/*
 *  An Instructor class used to retain information sent from the server
 */
class Instructor {

    public constructor(id: number, name: string, email: string, phone: string) {
        this.ID = id;
        this.Name = name;
        this.Email = email;
        this.Phone = phone;
    }

    public ID:number;
    public Name:string;
    public Email:string;
    public Phone:string;

}