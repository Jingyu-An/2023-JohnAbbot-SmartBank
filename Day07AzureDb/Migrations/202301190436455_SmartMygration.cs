namespace Day07AzureDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmartMygration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Customer_id = c.Int(nullable: false),
                        User_id = c.Int(nullable: false),
                        Bank_branch_address = c.String(),
                        Phone_number_branch = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
            DropTable("dbo.Accounts");
        }
    }
}
