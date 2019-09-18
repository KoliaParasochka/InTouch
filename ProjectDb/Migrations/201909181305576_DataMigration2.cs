namespace ProjectDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Path", c => c.String());
            AddColumn("dbo.Messages", "FileName", c => c.String());
            AddColumn("dbo.Messages", "IsImg", c => c.Boolean(nullable: false));
            DropColumn("dbo.Messages", "ImgPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "ImgPath", c => c.String());
            DropColumn("dbo.Messages", "IsImg");
            DropColumn("dbo.Messages", "FileName");
            DropColumn("dbo.Messages", "Path");
        }
    }
}
