namespace GeneralStore.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customerTransactions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Transactions", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Transactions", new[] { "ProductID" });
            DropIndex("dbo.Transactions", new[] { "CustomerId" });
            DropTable("dbo.Transactions");
        }
    }
}
