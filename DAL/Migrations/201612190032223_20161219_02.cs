namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161219_02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Description_Title", c => c.String());
            AddColumn("dbo.Categories", "Description_Content", c => c.String());
            AddColumn("dbo.Products", "Description_Title", c => c.String());
            AddColumn("dbo.Products", "Description_Content", c => c.String());
            DropColumn("dbo.Categories", "Description");
            DropColumn("dbo.Products", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Description", c => c.String());
            AddColumn("dbo.Categories", "Description", c => c.String());
            DropColumn("dbo.Products", "Description_Content");
            DropColumn("dbo.Products", "Description_Title");
            DropColumn("dbo.Categories", "Description_Content");
            DropColumn("dbo.Categories", "Description_Title");
        }
    }
}
