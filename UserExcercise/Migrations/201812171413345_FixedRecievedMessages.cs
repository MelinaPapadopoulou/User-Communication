namespace UserExcercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedRecievedMessages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonalMessages", "IsMessageRead", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PersonalMessages", "IsMessageRead");
        }
    }
}
