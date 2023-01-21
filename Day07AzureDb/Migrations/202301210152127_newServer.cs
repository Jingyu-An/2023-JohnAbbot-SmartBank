namespace Day07AzureDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newServer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Account_id = c.Int(nullable: false, identity: true),
                        Account_number = c.Int(nullable: false),
                        Bank_branch_address = c.String(),
                        Phone_number_branch = c.String(),
                        User_id = c.Int(nullable: false),
                        Customer_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Account_id)
                .ForeignKey("dbo.Customers", t => t.Customer_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .Index(t => t.User_id)
                .Index(t => t.Customer_id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Customer_id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Full_name = c.String(),
                        Phone_number = c.Int(nullable: false),
                        Password = c.String(),
                        Address = c.String(),
                        Created_at = c.DateTime(nullable: false),
                        Account_type = c.String(),
                    })
                .PrimaryKey(t => t.Customer_id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        User_id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Full_name = c.String(),
                        Phone_number = c.Int(nullable: false),
                        Password = c.String(),
                        Address = c.String(),
                        Created_at = c.DateTime(nullable: false),
                        Account_type = c.String(),
                    })
                .PrimaryKey(t => t.User_id);
            
            CreateTable(
                "dbo.Operations",
                c => new
                    {
                        Transaction_id = c.Int(nullable: false, identity: true),
                        Deposit_amount = c.Int(nullable: false),
                        Withdrawal_amount = c.Int(nullable: false),
                        Other_account_id = c.Int(nullable: false),
                        Date_operation = c.DateTime(nullable: false),
                        Description = c.String(),
                        Transfer_Type = c.Int(nullable: false),
                        Account_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Transaction_id)
                .ForeignKey("dbo.Accounts", t => t.Account_id, cascadeDelete: true)
                .Index(t => t.Account_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Operations", "Account_id", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "User_id", "dbo.Users");
            DropForeignKey("dbo.Accounts", "Customer_id", "dbo.Customers");
            DropIndex("dbo.Operations", new[] { "Account_id" });
            DropIndex("dbo.Accounts", new[] { "Customer_id" });
            DropIndex("dbo.Accounts", new[] { "User_id" });
            DropTable("dbo.Operations");
            DropTable("dbo.Users");
            DropTable("dbo.Customers");
            DropTable("dbo.Accounts");
        }
    }
}
