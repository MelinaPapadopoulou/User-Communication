namespace UserExcercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingUsernameAndPersonalMessages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PersonalMessages", "Senderid", "dbo.Users");
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String());
            CreateIndex("dbo.PersonalMessages", "Recieverid");
            AddForeignKey("dbo.PersonalMessages", "Recieverid", "dbo.Users", "UserId");
            AddForeignKey("dbo.PersonalMessages", "Senderid", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonalMessages", "Senderid", "dbo.Users");
            DropForeignKey("dbo.PersonalMessages", "Recieverid", "dbo.Users");
            DropIndex("dbo.PersonalMessages", new[] { "Recieverid" });
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 20));
            AddForeignKey("dbo.PersonalMessages", "Senderid", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
