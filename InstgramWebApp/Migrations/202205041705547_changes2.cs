namespace InstgramWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Bio", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Bio", c => c.String());
        }
    }
}
