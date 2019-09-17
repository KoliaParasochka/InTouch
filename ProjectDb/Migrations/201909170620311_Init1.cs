namespace ProjectDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Email", c => c.String());
            AlterColumn("dbo.People", "LastName", c => c.String());
            AlterColumn("dbo.People", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "Name", c => c.Int(nullable: false));
            AlterColumn("dbo.People", "LastName", c => c.Int(nullable: false));
            DropColumn("dbo.People", "Email");
        }
    }
}
