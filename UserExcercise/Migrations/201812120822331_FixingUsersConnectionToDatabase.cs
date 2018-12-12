namespace UserExcercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingUsersConnectionToDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonalMessages", "IsMessageShownToReciever", c => c.Boolean(nullable: false));
            AddColumn("dbo.PersonalMessages", "IsMessageShownToSender", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PersonalMessages", "IsMessageShownToSender");
            DropColumn("dbo.PersonalMessages", "IsMessageShownToReciever");
        }
    }
}
