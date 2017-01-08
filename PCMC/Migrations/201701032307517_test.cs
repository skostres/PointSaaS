namespace PCMC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
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
                "dbo.Schools",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Instructors", t => t.ID)
                .Index(t => t.ID);
            
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
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SchoolEnrolled_ID = c.Int(),
                        TeamAssigned_ID = c.Int(),
                        user_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Schools", t => t.SchoolEnrolled_ID)
                .ForeignKey("dbo.Teams", t => t.TeamAssigned_ID)
                .ForeignKey("dbo.Users", t => t.user_ID)
                .Index(t => t.SchoolEnrolled_ID)
                .Index(t => t.TeamAssigned_ID)
                .Index(t => t.user_ID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "user_ID", "dbo.Users");
            DropForeignKey("dbo.Students", "TeamAssigned_ID", "dbo.Teams");
            DropForeignKey("dbo.Students", "SchoolEnrolled_ID", "dbo.Schools");
            DropForeignKey("dbo.Teams", "School_ID", "dbo.Schools");
            DropForeignKey("dbo.Schools", "ID", "dbo.Instructors");
            DropIndex("dbo.Users", new[] { "Username" });
            DropIndex("dbo.Students", new[] { "user_ID" });
            DropIndex("dbo.Students", new[] { "TeamAssigned_ID" });
            DropIndex("dbo.Students", new[] { "SchoolEnrolled_ID" });
            DropIndex("dbo.Teams", new[] { "School_ID" });
            DropIndex("dbo.Schools", new[] { "ID" });
            DropTable("dbo.Users");
            DropTable("dbo.Students");
            DropTable("dbo.Teams");
            DropTable("dbo.Schools");
            DropTable("dbo.Instructors");
        }
    }
}
