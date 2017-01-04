namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20161217_01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategoryPhotoes", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.ProductCategories", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.PayWayProducts", "PayWay_Id", "dbo.PayWays");
            DropForeignKey("dbo.PayWayProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "SupplierId", "dbo.Suppliers");
            DropIndex("dbo.CategoryPhotoes", new[] { "CategoryId" });
            DropIndex("dbo.Products", new[] { "SupplierId" });
            DropIndex("dbo.ProductCategories", new[] { "Product_Id" });
            DropIndex("dbo.ProductCategories", new[] { "Category_Id" });
            DropIndex("dbo.PayWayProducts", new[] { "PayWay_Id" });
            DropIndex("dbo.PayWayProducts", new[] { "Product_Id" });

            DropTable("dbo.Categories");

            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Parent = c.Int(nullable: false),
                        Name = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Prefix = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Products", "SupplierId");
          
            DropTable("dbo.Suppliers");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.PayWayProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PayWayProducts",
                c => new
                    {
                        PayWay_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PayWay_Id, t.Product_Id });
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Category_Id });
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Contact = c.String(),
                        Tel = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Parent = c.Int(nullable: false),
                        Name = c.String(),
                        Prefix = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "SupplierId", c => c.Int(nullable: false));
            DropTable("dbo.Categories");
            CreateIndex("dbo.PayWayProducts", "Product_Id");
            CreateIndex("dbo.PayWayProducts", "PayWay_Id");
            CreateIndex("dbo.ProductCategories", "Category_Id");
            CreateIndex("dbo.ProductCategories", "Product_Id");
            CreateIndex("dbo.Products", "SupplierId");
            CreateIndex("dbo.CategoryPhotoes", "CategoryId");
            AddForeignKey("dbo.Products", "SupplierId", "dbo.Suppliers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PayWayProducts", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PayWayProducts", "PayWay_Id", "dbo.PayWays", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductCategories", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductCategories", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CategoryPhotoes", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
