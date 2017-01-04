namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161223_04 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ADs", "DisplayOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ADs", "DisplayOrder");
        }
    }
}
