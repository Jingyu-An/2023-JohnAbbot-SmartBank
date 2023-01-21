namespace Day07AzureDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Phone_number", c => c.String());
            AlterColumn("dbo.Users", "Phone_number", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Phone_number", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "Phone_number", c => c.Int(nullable: false));
        }
    }
}
