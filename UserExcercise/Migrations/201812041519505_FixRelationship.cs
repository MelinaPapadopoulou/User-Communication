namespace UserExcercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ForumMessages", "User_UserId", "dbo.Users");
            DropIndex("dbo.ForumMessages", new[] { "User_UserId" });
            DropColumn("dbo.ForumMessages", "Senderid");
            RenameColumn(table: "dbo.ForumMessages", name: "User_UserId", newName: "Senderid");
            AlterColumn("dbo.ForumMessages", "Senderid", c => c.Int(nullable: false));
            AlterColumn("dbo.ForumMessages", "Senderid", c => c.Int(nullable: false));
            CreateIndex("dbo.ForumMessages", "Senderid");
            AddForeignKey("dbo.ForumMessages", "Senderid", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ForumMessages", "Senderid", "dbo.Users");
            DropIndex("dbo.ForumMessages", new[] { "Senderid" });
            AlterColumn("dbo.ForumMessages", "Senderid", c => c.Int());
            AlterColumn("dbo.ForumMessages", "Senderid", c => c.String());
            RenameColumn(table: "dbo.ForumMessages", name: "Senderid", newName: "User_UserId");
            AddColumn("dbo.ForumMessages", "Senderid", c => c.String());
            CreateIndex("dbo.ForumMessages", "User_UserId");
            AddForeignKey("dbo.ForumMessages", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
