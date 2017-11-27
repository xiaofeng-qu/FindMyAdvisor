namespace FindMyAdvisor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Professors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        Join_Date = c.DateTime(nullable: false),
                        Homepage = c.String(maxLength: 50),
                        Photo_Link = c.String(maxLength: 50),
                        UniversityId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        BachelorId = c.Int(),
                        MasterId = c.Int(),
                        PhdId = c.Int(),
                        Likes = c.Int(),
                        Rank_Id = c.Int(nullable: false),
                        Research_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Universities", t => t.BachelorId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Universities", t => t.MasterId)
                .ForeignKey("dbo.Universities", t => t.PhdId)
                .ForeignKey("dbo.Ranks", t => t.Rank_Id, cascadeDelete: true)
                .ForeignKey("dbo.Researches", t => t.Research_Id, cascadeDelete: true)
                .ForeignKey("dbo.Universities", t => t.UniversityId, cascadeDelete: true)
                .Index(t => t.UniversityId)
                .Index(t => t.DepartmentId)
                .Index(t => t.BachelorId)
                .Index(t => t.MasterId)
                .Index(t => t.PhdId)
                .Index(t => t.Rank_Id)
                .Index(t => t.Research_Id);
            
            CreateTable(
                "dbo.Universities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Researches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Research_Interest = c.String(nullable: false, maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 32),
                        Role = c.String(maxLength: 16),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfessors",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Professor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Professor_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Professors", t => t.Professor_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Professor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfessors", "Professor_Id", "dbo.Professors");
            DropForeignKey("dbo.UserProfessors", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Professors", "UniversityId", "dbo.Universities");
            DropForeignKey("dbo.Professors", "Research_Id", "dbo.Researches");
            DropForeignKey("dbo.Professors", "Rank_Id", "dbo.Ranks");
            DropForeignKey("dbo.Professors", "PhdId", "dbo.Universities");
            DropForeignKey("dbo.Professors", "MasterId", "dbo.Universities");
            DropForeignKey("dbo.Professors", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Professors", "BachelorId", "dbo.Universities");
            DropIndex("dbo.UserProfessors", new[] { "Professor_Id" });
            DropIndex("dbo.UserProfessors", new[] { "User_Id" });
            DropIndex("dbo.Professors", new[] { "Research_Id" });
            DropIndex("dbo.Professors", new[] { "Rank_Id" });
            DropIndex("dbo.Professors", new[] { "PhdId" });
            DropIndex("dbo.Professors", new[] { "MasterId" });
            DropIndex("dbo.Professors", new[] { "BachelorId" });
            DropIndex("dbo.Professors", new[] { "DepartmentId" });
            DropIndex("dbo.Professors", new[] { "UniversityId" });
            DropTable("dbo.UserProfessors");
            DropTable("dbo.Users");
            DropTable("dbo.Researches");
            DropTable("dbo.Ranks");
            DropTable("dbo.Universities");
            DropTable("dbo.Professors");
            DropTable("dbo.Departments");
        }
    }
}
