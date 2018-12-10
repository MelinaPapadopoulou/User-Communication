namespace UserExcercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingDeleteUser : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ForumMessages", new[] { "Senderid" });
            CreateIndex("dbo.ForumMessages", "SenderID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ForumMessages", new[] { "SenderID" });
            CreateIndex("dbo.ForumMessages", "Senderid");
        }
    }
}
