namespace UserExcercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingAlmostEverything : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PersonalMessages", "User_UserId", "dbo.Users");
            DropIndex("dbo.PersonalMessages", new[] { "User_UserId" });
            DropColumn("dbo.PersonalMessages", "Senderid");
            RenameColumn(table: "dbo.PersonalMessages", name: "User_UserId", newName: "Senderid");
            AlterColumn("dbo.PersonalMessages", "Recieverid", c => c.Int(nullable: false));
            AlterColumn("dbo.PersonalMessages", "Senderid", c => c.Int(nullable: false));
            AlterColumn("dbo.PersonalMessages", "Senderid", c => c.Int(nullable: false));
            CreateIndex("dbo.PersonalMessages", "Senderid");
            AddForeignKey("dbo.PersonalMessages", "Senderid", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonalMessages", "Senderid", "dbo.Users");
            DropIndex("dbo.PersonalMessages", new[] { "Senderid" });
            AlterColumn("dbo.PersonalMessages", "Senderid", c => c.Int());
            AlterColumn("dbo.PersonalMessages", "Senderid", c => c.String());
            AlterColumn("dbo.PersonalMessages", "Recieverid", c => c.String());
            RenameColumn(table: "dbo.PersonalMessages", name: "Senderid", newName: "User_UserId");
            AddColumn("dbo.PersonalMessages", "Senderid", c => c.String());
            CreateIndex("dbo.PersonalMessages", "User_UserId");
            AddForeignKey("dbo.PersonalMessages", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
