namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161206_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "DisplayOrder", c => c.Int(nullable: false));
            AddColumn("dbo.Categories", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPhotoes", "Top", c => c.Boolean(nullable: false));
            AddColumn("dbo.Suppliers", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "Valid", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CreateDate");
            DropColumn("dbo.Orders", "Valid");
            DropColumn("dbo.Orders", "CreateDate");
            DropColumn("dbo.Suppliers", "Active");
            DropColumn("dbo.ProductPhotoes", "Top");
            DropColumn("dbo.Products", "Active");
            DropColumn("dbo.Products", "CreateDate");
            DropColumn("dbo.Categories", "Active");
            DropColumn("dbo.Categories", "DisplayOrder");
        }
    }
}
