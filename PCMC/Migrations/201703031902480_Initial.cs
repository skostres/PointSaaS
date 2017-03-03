namespace PCMC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Instructors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.JudgeTeamMaps",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Judge_ID = c.Int(),
                        Team_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Judge_ID)
                .ForeignKey("dbo.Teams", t => t.Team_ID)
                .Index(t => t.Judge_ID)
                .Index(t => t.Team_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Password = c.String(),
                        Username = c.String(maxLength: 450),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Username, unique: true);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        lvl = c.Int(nullable: false),
                        School_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Schools", t => t.School_ID)
                .Index(t => t.School_ID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RawZipFileJudges = c.Binary(),
                        RawZipFileParticipants = c.Binary(),
                        Name = c.String(),
                        Description = c.String(),
                        MaxScore = c.Int(nullable: false),
                        Hidden = c.Boolean(nullable: false),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Instructor_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Instructors", t => t.Instructor_ID)
                .Index(t => t.Instructor_ID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SchoolEnrolled_ID = c.Int(),
                        TeamAssigned_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Schools", t => t.SchoolEnrolled_ID)
                .ForeignKey("dbo.Teams", t => t.TeamAssigned_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.SchoolEnrolled_ID)
                .Index(t => t.TeamAssigned_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.TeamSubmissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RawZipSolution = c.Binary(),
                        Score = c.Int(nullable: false),
                        Project_ID = c.Int(),
                        Team_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.Project_ID)
                .ForeignKey("dbo.Teams", t => t.Team_ID)
                .Index(t => t.Project_ID)
                .Index(t => t.Team_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamSubmissions", "Team_ID", "dbo.Teams");
            DropForeignKey("dbo.TeamSubmissions", "Project_ID", "dbo.Projects");
            DropForeignKey("dbo.Students", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Students", "TeamAssigned_ID", "dbo.Teams");
            DropForeignKey("dbo.Students", "SchoolEnrolled_ID", "dbo.Schools");
            DropForeignKey("dbo.Teams", "School_ID", "dbo.Schools");
            DropForeignKey("dbo.Schools", "Instructor_ID", "dbo.Instructors");
            DropForeignKey("dbo.JudgeTeamMaps", "Team_ID", "dbo.Teams");
            DropForeignKey("dbo.JudgeTeamMaps", "Judge_ID", "dbo.Users");
            DropIndex("dbo.TeamSubmissions", new[] { "Team_ID" });
            DropIndex("dbo.TeamSubmissions", new[] { "Project_ID" });
            DropIndex("dbo.Students", new[] { "User_ID" });
            DropIndex("dbo.Students", new[] { "TeamAssigned_ID" });
            DropIndex("dbo.Students", new[] { "SchoolEnrolled_ID" });
            DropIndex("dbo.Schools", new[] { "Instructor_ID" });
            DropIndex("dbo.Teams", new[] { "School_ID" });
            DropIndex("dbo.Users", new[] { "Username" });
            DropIndex("dbo.JudgeTeamMaps", new[] { "Team_ID" });
            DropIndex("dbo.JudgeTeamMaps", new[] { "Judge_ID" });
            DropTable("dbo.TeamSubmissions");
            DropTable("dbo.Students");
            DropTable("dbo.Schools");
            DropTable("dbo.Projects");
            DropTable("dbo.Teams");
            DropTable("dbo.Users");
            DropTable("dbo.JudgeTeamMaps");
            DropTable("dbo.Instructors");
        }
    }
}
