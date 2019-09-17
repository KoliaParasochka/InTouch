namespace ProjectDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Messages",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Text = c.Int(nullable: false),
            //            PersonId = c.Int(),
            //            SecondPersonId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.People", t => t.PersonId)
            //    .Index(t => t.PersonId);
            
            //CreateTable(
            //    "dbo.People",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            LastName = c.Int(nullable: false),
            //            Name = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "PersonId", "dbo.People");
            DropIndex("dbo.Messages", new[] { "PersonId" });
            DropTable("dbo.People");
            DropTable("dbo.Messages");
        }
    }
}
