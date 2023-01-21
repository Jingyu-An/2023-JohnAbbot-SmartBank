namespace Day07AzureDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class balance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Account_balance", c => c.String());
            DropColumn("dbo.Accounts", "Account_number");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "Account_number", c => c.Int(nullable: false));
            DropColumn("dbo.Accounts", "Account_balance");
        }
    }
}
