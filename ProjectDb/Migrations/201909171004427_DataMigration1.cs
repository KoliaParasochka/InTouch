namespace ProjectDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "ImgPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "ImgPath");
        }
    }
}
