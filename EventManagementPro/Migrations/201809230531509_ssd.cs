namespace EventManagementPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ssd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        CategoryStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        EventName = c.String(),
                    })
                .PrimaryKey(t => t.EventID);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FatherName = c.String(),
                        MotehrName = c.String(),
                        Age = c.String(),
                        Gender = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                        ParmanentAddress = c.String(),
                        PresentAddress = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Feedback = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        SBookId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.SBookId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.SProducts", t => t.SBookId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.SBookId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        ShippedDate = c.DateTime(),
                        CustomerName = c.String(nullable: false, maxLength: 50),
                        ShippingAddress = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        TransactionId = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.SProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(nullable: false, maxLength: 500),
                        Category = c.String(nullable: false, maxLength: 30),
                        PictureFile = c.String(),
                        Picture = c.String(),
                        Stocklevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockIns",
                c => new
                    {
                        StockInId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        quantity = c.Int(nullable: false),
                        SBookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockInId)
                .ForeignKey("dbo.SProducts", t => t.SBookId, cascadeDelete: true)
                .Index(t => t.SBookId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        AvilableProduct = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        BookStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurId = c.Int(nullable: false, identity: true),
                        PurDate = c.DateTime(nullable: false),
                        PurFrom = c.String(),
                        PurNo = c.Int(nullable: false),
                        PurchaseProductName = c.String(),
                        Quantity = c.Int(nullable: false),
                        Rate = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.PurId);
            
            CreateTable(
                "dbo.RequestProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        url = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subscribers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.VenueBookings",
                c => new
                    {
                        VenueBookingID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        EventName = c.String(),
                        EventID = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        HallName = c.String(),
                        NumberofGuest = c.Int(nullable: false),
                        Event_EventID = c.Int(),
                    })
                .PrimaryKey(t => t.VenueBookingID)
                .ForeignKey("dbo.Events", t => t.Event_EventID)
                .Index(t => t.Event_EventID);
            
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Capasity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(nullable: false, maxLength: 500),
                        PictureFile = c.String(nullable: false, maxLength: 30),
                        Picture = c.String(),
                        EventName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VenueBookings", "Event_EventID", "dbo.Events");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.OrderDetails", "SBookId", "dbo.SProducts");
            DropForeignKey("dbo.StockIns", "SBookId", "dbo.SProducts");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropIndex("dbo.VenueBookings", new[] { "Event_EventID" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.StockIns", new[] { "SBookId" });
            DropIndex("dbo.OrderDetails", new[] { "SBookId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropTable("dbo.Venues");
            DropTable("dbo.VenueBookings");
            DropTable("dbo.Subscribers");
            DropTable("dbo.RequestProducts");
            DropTable("dbo.Purchases");
            DropTable("dbo.Products");
            DropTable("dbo.StockIns");
            DropTable("dbo.SProducts");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Messages");
            DropTable("dbo.Members");
            DropTable("dbo.Events");
            DropTable("dbo.Categories");
        }
    }
}
