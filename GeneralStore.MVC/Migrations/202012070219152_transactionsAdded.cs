namespace GeneralStore.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transactionsAdded : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Transactions", new[] { "ProductID" });
            CreateIndex("dbo.Transactions", "ProductId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Transactions", new[] { "ProductId" });
            CreateIndex("dbo.Transactions", "ProductID");
        }
    }
}
