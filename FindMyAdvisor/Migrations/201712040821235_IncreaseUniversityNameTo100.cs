namespace FindMyAdvisor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreaseUniversityNameTo100 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Universities", new[] { "Name" });
            AlterColumn("dbo.Universities", "Name", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Universities", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Universities", new[] { "Name" });
            AlterColumn("dbo.Universities", "Name", c => c.String(nullable: false, maxLength: 60));
            CreateIndex("dbo.Universities", "Name", unique: true);
        }
    }
}
