namespace PCMC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamSubmissionAddedComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamSubmissions", "GraderComment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeamSubmissions", "GraderComment");
        }
    }
}
