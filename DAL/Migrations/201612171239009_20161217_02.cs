namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161217_02 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Category_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.PayWayProducts",
                c => new
                    {
                        PayWay_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PayWay_Id, t.Product_Id })
                .ForeignKey("dbo.PayWays", t => t.PayWay_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.PayWay_Id)
                .Index(t => t.Product_Id);
            
            AddColumn("dbo.Categories", "Contact", c => c.String());
            AddColumn("dbo.Categories", "Tel", c => c.String());
            AddColumn("dbo.Categories", "Phone", c => c.String());
            AddColumn("dbo.Categories", "Email", c => c.String());
            CreateIndex("dbo.CategoryPhotoes", "CategoryId");
            AddForeignKey("dbo.CategoryPhotoes", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PayWayProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.PayWayProducts", "PayWay_Id", "dbo.PayWays");
            DropForeignKey("dbo.ProductCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.ProductCategories", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.CategoryPhotoes", "CategoryId", "dbo.Categories");
            DropIndex("dbo.PayWayProducts", new[] { "Product_Id" });
            DropIndex("dbo.PayWayProducts", new[] { "PayWay_Id" });
            DropIndex("dbo.ProductCategories", new[] { "Category_Id" });
            DropIndex("dbo.ProductCategories", new[] { "Product_Id" });
            DropIndex("dbo.CategoryPhotoes", new[] { "CategoryId" });
            DropColumn("dbo.Categories", "Email");
            DropColumn("dbo.Categories", "Phone");
            DropColumn("dbo.Categories", "Tel");
            DropColumn("dbo.Categories", "Contact");
            DropTable("dbo.PayWayProducts");
            DropTable("dbo.ProductCategories");
        }
    }
}
