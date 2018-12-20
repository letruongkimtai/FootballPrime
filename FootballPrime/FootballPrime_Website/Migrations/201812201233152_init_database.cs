namespace FootballPrime_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdmID = c.Int(nullable: false, identity: true),
                        AdmName = c.Int(nullable: false),
                        AdmPwd = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdmID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CtmID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        CtmName = c.String(nullable: false),
                        Tel = c.Int(nullable: false),
                        Address = c.String(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.CtmID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        PrID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.OrderID, t.PrID })
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.PrID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.PrID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        Odate = c.DateTime(nullable: false),
                        PaymentMethod = c.String(),
                        PaymentCheck = c.Boolean(nullable: false),
                        DeliveryStatus = c.Boolean(nullable: false),
                        CtmID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customers", t => t.CtmID, cascadeDelete: true)
                .Index(t => t.CtmID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        PrID = c.Int(nullable: false, identity: true),
                        PrName = c.String(),
                        Pic = c.String(),
                        Describe = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PrID)
                .ForeignKey("dbo.ProductTypes", t => t.PrTypeID, cascadeDelete: true)
                .Index(t => t.PrTypeID);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        PrTypeID = c.Int(nullable: false, identity: true),
                        PrTypeName = c.String(),
                    })
                .PrimaryKey(t => t.PrTypeID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Date = c.DateTime(nullable: false),
                        Author = c.String(),
                        Img = c.String(),
                        Content = c.String(),
                        PostTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.PostTypes", t => t.PostTypeID, cascadeDelete: true)
                .Index(t => t.PostTypeID);
            
            CreateTable(
                "dbo.PostTypes",
                c => new
                    {
                        PostTypeID = c.Int(nullable: false, identity: true),
                        PostTypeName = c.String(),
                    })
                .PrimaryKey(t => t.PostTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "PostTypeID", "dbo.PostTypes");
            DropForeignKey("dbo.OrderDetails", "PrID", "dbo.Products");
            DropForeignKey("dbo.Products", "PrTypeID", "dbo.ProductTypes");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CtmID", "dbo.Customers");
            DropIndex("dbo.Posts", new[] { "PostTypeID" });
            DropIndex("dbo.Products", new[] { "PrTypeID" });
            DropIndex("dbo.Orders", new[] { "CtmID" });
            DropIndex("dbo.OrderDetails", new[] { "PrID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropTable("dbo.PostTypes");
            DropTable("dbo.Posts");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Customers");
            DropTable("dbo.Admins");
        }
    }
}
