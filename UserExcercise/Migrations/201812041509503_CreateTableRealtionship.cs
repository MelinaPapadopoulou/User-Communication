namespace UserExcercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableRealtionship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ForumMessages", "User_UserId", c => c.Int());
            AddColumn("dbo.PersonalMessages", "Recieverid", c => c.String());
            AddColumn("dbo.PersonalMessages", "User_UserId", c => c.Int());
            CreateIndex("dbo.ForumMessages", "User_UserId");
            CreateIndex("dbo.PersonalMessages", "User_UserId");
            AddForeignKey("dbo.ForumMessages", "User_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.PersonalMessages", "User_UserId", "dbo.Users", "UserId");
            DropColumn("dbo.PersonalMessages", "Receiverid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PersonalMessages", "Receiverid", c => c.String());
            DropForeignKey("dbo.PersonalMessages", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.ForumMessages", "User_UserId", "dbo.Users");
            DropIndex("dbo.PersonalMessages", new[] { "User_UserId" });
            DropIndex("dbo.ForumMessages", new[] { "User_UserId" });
            DropColumn("dbo.PersonalMessages", "User_UserId");
            DropColumn("dbo.PersonalMessages", "Recieverid");
            DropColumn("dbo.ForumMessages", "User_UserId");
        }
    }
}
