namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161222_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Clicks", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "LastUpdate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "LastUpdate");
            DropColumn("dbo.Products", "Clicks");
        }
    }
}
