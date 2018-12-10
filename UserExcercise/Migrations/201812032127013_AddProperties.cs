namespace UserExcercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ForumMessages", "Sender", c => c.String());
            AddColumn("dbo.ForumMessages", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.ForumMessages", "MessageText", c => c.String());
            AddColumn("dbo.PersonalMessages", "Receiver", c => c.String());
            AddColumn("dbo.PersonalMessages", "Sender", c => c.String());
            AddColumn("dbo.PersonalMessages", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.PersonalMessages", "MessageText", c => c.String());
            AddColumn("dbo.Users", "Username", c => c.String());
            AddColumn("dbo.Users", "UsersPrivilege", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "UsersPrivilege");
            DropColumn("dbo.Users", "Username");
            DropColumn("dbo.PersonalMessages", "MessageText");
            DropColumn("dbo.PersonalMessages", "DateCreated");
            DropColumn("dbo.PersonalMessages", "Sender");
            DropColumn("dbo.PersonalMessages", "Receiver");
            DropColumn("dbo.ForumMessages", "MessageText");
            DropColumn("dbo.ForumMessages", "DateCreated");
            DropColumn("dbo.ForumMessages", "Sender");
        }
    }
}
