namespace UserExcercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTitleToPersonalMessages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonalMessages", "MessageTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PersonalMessages", "MessageTitle");
        }
    }
}
