namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161218_02 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Clicks = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagProducts",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Product_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.TagProducts", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TagProducts", new[] { "Product_Id" });
            DropIndex("dbo.TagProducts", new[] { "Tag_Id" });
            DropTable("dbo.TagProducts");
            DropTable("dbo.Tags");
        }
    }
}
