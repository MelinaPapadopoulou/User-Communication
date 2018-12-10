namespace UserExcercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsingId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ForumMessages", "Senderid", c => c.String());
            AddColumn("dbo.PersonalMessages", "Receiverid", c => c.String());
            AddColumn("dbo.PersonalMessages", "Senderid", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            DropColumn("dbo.ForumMessages", "Sender");
            DropColumn("dbo.PersonalMessages", "Receiver");
            DropColumn("dbo.PersonalMessages", "Sender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PersonalMessages", "Sender", c => c.String());
            AddColumn("dbo.PersonalMessages", "Receiver", c => c.String());
            AddColumn("dbo.ForumMessages", "Sender", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String());
            DropColumn("dbo.PersonalMessages", "Senderid");
            DropColumn("dbo.PersonalMessages", "Receiverid");
            DropColumn("dbo.ForumMessages", "Senderid");
        }
    }
}
