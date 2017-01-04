namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161223_03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ADs", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.ADs", "Clicks", c => c.Int(nullable: false));
            AddColumn("dbo.ADs", "CreateDate", c => c.DateTime());
            AddColumn("dbo.ADs", "LastUpdate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ADs", "LastUpdate");
            DropColumn("dbo.ADs", "CreateDate");
            DropColumn("dbo.ADs", "Clicks");
            DropColumn("dbo.ADs", "Active");
        }
    }
}
