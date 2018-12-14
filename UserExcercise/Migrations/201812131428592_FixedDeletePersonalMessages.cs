namespace UserExcercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedDeletePersonalMessages : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ForumMessages", "MessageText", c => c.String(nullable: false));
            AlterColumn("dbo.PersonalMessages", "MessageText", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PersonalMessages", "MessageText", c => c.String());
            AlterColumn("dbo.ForumMessages", "MessageText", c => c.String());
        }
    }
}
