namespace UserExcercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingUsernameAndPersonalMessages1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PersonalMessages", new[] { "Recieverid" });
            DropIndex("dbo.PersonalMessages", new[] { "Senderid" });
            CreateIndex("dbo.PersonalMessages", "RecieverID");
            CreateIndex("dbo.PersonalMessages", "SenderID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PersonalMessages", new[] { "SenderID" });
            DropIndex("dbo.PersonalMessages", new[] { "RecieverID" });
            CreateIndex("dbo.PersonalMessages", "Senderid");
            CreateIndex("dbo.PersonalMessages", "Recieverid");
        }
    }
}
