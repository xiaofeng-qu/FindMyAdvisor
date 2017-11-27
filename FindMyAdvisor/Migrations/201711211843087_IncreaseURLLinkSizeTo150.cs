namespace FindMyAdvisor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreaseURLLinkSizeTo150 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Professors", "Homepage", c => c.String(maxLength: 150));
            AlterColumn("dbo.Professors", "Photo_Link", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Professors", "Photo_Link", c => c.String(maxLength: 50));
            AlterColumn("dbo.Professors", "Homepage", c => c.String(maxLength: 50));
        }
    }
}
