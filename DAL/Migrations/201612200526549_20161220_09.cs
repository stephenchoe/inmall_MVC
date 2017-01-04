namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161220_09 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Kind", c => c.String());
            DropColumn("dbo.Products", "OnlineId");
            DropColumn("dbo.Products", "Stock");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Stock", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "OnlineId", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Kind");
        }
    }
}
