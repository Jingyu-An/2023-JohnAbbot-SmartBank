namespace Day07AzureDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foriegnkey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Accounts");
            DropPrimaryKey("dbo.Operations");
            DropColumn("dbo.Accounts", "Id");
            DropColumn("dbo.Operations", "Id");
            AddColumn("dbo.Accounts", "Account_id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Accounts", "Account_number", c => c.Int(nullable: false));
            AddColumn("dbo.Operations", "Transaction_id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Accounts", "Account_id");
            AddPrimaryKey("dbo.Operations", "Transaction_id");
            CreateIndex("dbo.Accounts", "User_id");
            CreateIndex("dbo.Accounts", "Customer_id");
            CreateIndex("dbo.Operations", "Account_id");
            AddForeignKey("dbo.Accounts", "Customer_id", "dbo.Customers", "Customer_id", cascadeDelete: true);
            AddForeignKey("dbo.Accounts", "User_id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Operations", "Account_id", "dbo.Accounts", "Account_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            AddColumn("dbo.Operations", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Accounts", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Operations", "Account_id", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "User_id", "dbo.Users");
            DropForeignKey("dbo.Accounts", "Customer_id", "dbo.Customers");
            DropIndex("dbo.Operations", new[] { "Account_id" });
            DropIndex("dbo.Accounts", new[] { "Customer_id" });
            DropIndex("dbo.Accounts", new[] { "User_id" });
            DropPrimaryKey("dbo.Operations");
            DropPrimaryKey("dbo.Accounts");
            DropColumn("dbo.Operations", "Transaction_id");
            DropColumn("dbo.Accounts", "Account_number");
            DropColumn("dbo.Accounts", "Account_id");
            AddPrimaryKey("dbo.Operations", "Id");
            AddPrimaryKey("dbo.Accounts", "Id");
        }
    }
}
