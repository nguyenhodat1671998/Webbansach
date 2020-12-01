namespace WebCuaHangSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        RoleID = c.Int(nullable: false),
                        LastLoginDate = c.DateTime(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Point = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsLocked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccountID = c.Int(nullable: false),
                        FoundedDate = c.DateTime(nullable: false),
                        TotalCost = c.Double(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsOrdered = c.Boolean(nullable: false),
                        IsApplied = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.AccountID, cascadeDelete: true)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.BillDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BillID = c.Int(nullable: false),
                        BookID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Bills", t => t.BillID, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: true)
                .Index(t => t.BillID)
                .Index(t => t.BookID);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PublishingCompany = c.String(),
                        PublishingDate = c.DateTime(nullable: false),
                        Size = c.String(),
                        NumberOfPages = c.Int(nullable: false),
                        CoverType = c.String(),
                        BookTypeID = c.Int(nullable: false),
                        AuthorID = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Image = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Discount = c.Int(nullable: false),
                        ReducePrice = c.Double(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Authors", t => t.AuthorID, cascadeDelete: true)
                .ForeignKey("dbo.BookTypes", t => t.BookTypeID, cascadeDelete: true)
                .Index(t => t.BookTypeID)
                .Index(t => t.AuthorID);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BookTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ContactInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        AddressNumber = c.String(),
                        Street = c.String(),
                        District = c.String(),
                        Province = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Activity = c.String(),
                        ActivityDate = c.DateTime(nullable: false),
                        Account_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_id, cascadeDelete: true)
                .Index(t => t.Account_id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Logs", "Account_id", "dbo.Accounts");
            DropForeignKey("dbo.ContactInfoes", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.BillDetails", "BookID", "dbo.Books");
            DropForeignKey("dbo.Books", "BookTypeID", "dbo.BookTypes");
            DropForeignKey("dbo.Books", "AuthorID", "dbo.Authors");
            DropForeignKey("dbo.BillDetails", "BillID", "dbo.Bills");
            DropForeignKey("dbo.Bills", "AccountID", "dbo.Accounts");
            DropIndex("dbo.Logs", new[] { "Account_id" });
            DropIndex("dbo.ContactInfoes", new[] { "AccountId" });
            DropIndex("dbo.Books", new[] { "AuthorID" });
            DropIndex("dbo.Books", new[] { "BookTypeID" });
            DropIndex("dbo.BillDetails", new[] { "BookID" });
            DropIndex("dbo.BillDetails", new[] { "BillID" });
            DropIndex("dbo.Bills", new[] { "AccountID" });
            DropIndex("dbo.Accounts", new[] { "RoleID" });
            DropTable("dbo.Roles");
            DropTable("dbo.Logs");
            DropTable("dbo.ContactInfoes");
            DropTable("dbo.BookTypes");
            DropTable("dbo.Authors");
            DropTable("dbo.Books");
            DropTable("dbo.BillDetails");
            DropTable("dbo.Bills");
            DropTable("dbo.Accounts");
        }
    }
}
