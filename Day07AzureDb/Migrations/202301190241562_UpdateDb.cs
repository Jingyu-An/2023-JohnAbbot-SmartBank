namespace Day07AzureDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Operations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account_id = c.Int(nullable: false),
                        Deposit_amount = c.Int(nullable: false),
                        Withdrawal_amount = c.Int(nullable: false),
                        Other_account_id = c.Int(nullable: false),
                        Date_operation = c.DateTime(nullable: false),
                        Description = c.String(),
                        Transfer_Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Full_name = c.String(),
                        Phone_number = c.Int(nullable: false),
                        Password = c.String(),
                        Address = c.String(),
                        created_at = c.DateTime(nullable: false),
                        Account_type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Operations");
        }
    }
}
