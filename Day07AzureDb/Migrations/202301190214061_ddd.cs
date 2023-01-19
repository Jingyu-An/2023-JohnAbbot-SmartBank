namespace Day07AzureDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddd : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
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
    }
}
