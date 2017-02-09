namespace PCMC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLevelfieldtoProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Level", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Level");
        }
    }
}
