namespace PCMC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamSubmissions", "Score", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeamSubmissions", "Score");
        }
    }
}
