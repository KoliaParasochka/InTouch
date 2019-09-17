namespace ProjectDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "Text", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Text", c => c.Int(nullable: false));
        }
    }
}
