/*
 *  A Project class used to retain information sent from the server
 */
class Project {
    public constructor(ID: number, JudgeZip: string, ParZip: string, Name: string, MaxScore: number, Desc: string, IsHidden: boolean
        , Level: Level) {
        this.ID = ID;
        this.RawZipFileJudges = JudgeZip;
        this.RawZipFileParticipants = ParZip;
        this.Name = Name;
        this.MaxScore = MaxScore;
        this.Description = Desc;
        this.Hidden = IsHidden;
        this.Level = Level;
    }

    public ID: number;
    public RawZipFileJudges: string;
    public RawZipFileParticipants: string;
    public Name: string;
    public MaxScore: number;
    public Description: string;
    public Hidden: boolean;
    public Level: Level;
}
