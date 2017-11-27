namespace FindMyAdvisor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyProfessorTable_Research_Rank_Id : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Professors", name: "Rank_Id", newName: "RankId");
            RenameColumn(table: "dbo.Professors", name: "Research_Id", newName: "ResearchId");
            RenameIndex(table: "dbo.Professors", name: "IX_Rank_Id", newName: "IX_RankId");
            RenameIndex(table: "dbo.Professors", name: "IX_Research_Id", newName: "IX_ResearchId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Professors", name: "IX_ResearchId", newName: "IX_Research_Id");
            RenameIndex(table: "dbo.Professors", name: "IX_RankId", newName: "IX_Rank_Id");
            RenameColumn(table: "dbo.Professors", name: "ResearchId", newName: "Research_Id");
            RenameColumn(table: "dbo.Professors", name: "RankId", newName: "Rank_Id");
        }
    }
}
