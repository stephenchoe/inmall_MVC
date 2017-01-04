namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161220_10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "OnlineId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "OnlineId");
        }
    }
}
