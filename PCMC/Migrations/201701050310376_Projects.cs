namespace PCMC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Projects : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Students", new[] { "user_ID" });
            CreateTable(
                "dbo.JudgeTeamMaps",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Judge_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Judge_ID)
                .Index(t => t.Judge_ID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RawZipFile = c.Binary(),
                        Name = c.String(),
                        Description = c.String(),
                        Hidden = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TeamSubmissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RawZipSolution = c.Binary(),
                        Project_ID = c.Int(),
                        Team_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.Project_ID)
                .ForeignKey("dbo.Teams", t => t.Team_ID)
                .Index(t => t.Project_ID)
                .Index(t => t.Team_ID);
            
            AddColumn("dbo.Teams", "JudgeTeamMap_ID", c => c.Int());
            CreateIndex("dbo.Teams", "JudgeTeamMap_ID");
            CreateIndex("dbo.Students", "User_ID");
            AddForeignKey("dbo.Teams", "JudgeTeamMap_ID", "dbo.JudgeTeamMaps", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamSubmissions", "Team_ID", "dbo.Teams");
            DropForeignKey("dbo.TeamSubmissions", "Project_ID", "dbo.Projects");
            DropForeignKey("dbo.Teams", "JudgeTeamMap_ID", "dbo.JudgeTeamMaps");
            DropForeignKey("dbo.JudgeTeamMaps", "Judge_ID", "dbo.Users");
            DropIndex("dbo.TeamSubmissions", new[] { "Team_ID" });
            DropIndex("dbo.TeamSubmissions", new[] { "Project_ID" });
            DropIndex("dbo.Students", new[] { "User_ID" });
            DropIndex("dbo.Teams", new[] { "JudgeTeamMap_ID" });
            DropIndex("dbo.JudgeTeamMaps", new[] { "Judge_ID" });
            DropColumn("dbo.Teams", "JudgeTeamMap_ID");
            DropTable("dbo.TeamSubmissions");
            DropTable("dbo.Projects");
            DropTable("dbo.JudgeTeamMaps");
            CreateIndex("dbo.Students", "user_ID");
        }
    }
}
