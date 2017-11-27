namespace FindMyAdvisor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAddDisplayName_AddUniqueConstrains : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "DisplayName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Users", "Email", unique: true);
            CreateIndex("dbo.Users", "DisplayName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "DisplayName" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropColumn("dbo.Users", "DisplayName");
        }
    }
}
