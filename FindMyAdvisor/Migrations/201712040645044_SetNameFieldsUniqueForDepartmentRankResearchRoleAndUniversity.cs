namespace FindMyAdvisor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetNameFieldsUniqueForDepartmentRankResearchRoleAndUniversity : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Departments", "Name", unique: true);
            CreateIndex("dbo.Universities", "Name", unique: true);
            CreateIndex("dbo.Ranks", "Name", unique: true);
            CreateIndex("dbo.Researches", "Research_Interest", unique: true);
            CreateIndex("dbo.Roles", "RoleName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Roles", new[] { "RoleName" });
            DropIndex("dbo.Researches", new[] { "Research_Interest" });
            DropIndex("dbo.Ranks", new[] { "Name" });
            DropIndex("dbo.Universities", new[] { "Name" });
            DropIndex("dbo.Departments", new[] { "Name" });
        }
    }
}
