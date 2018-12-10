namespace UserExcercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ForumMessages",
                c => new
                    {
                        ForumMessageId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ForumMessageId);
            
            CreateTable(
                "dbo.PersonalMessages",
                c => new
                    {
                        PersonalMessageId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.PersonalMessageId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.PersonalMessages");
            DropTable("dbo.ForumMessages");
        }
    }
}
