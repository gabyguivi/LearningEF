namespace TestExam.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestsDone",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TestExamId = c.Int(nullable: false),
                        Gradle = c.Double(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TestsExam", t => t.TestExamId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TestExamId);
            
            CreateTable(
                "dbo.TestsExam",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        PassScore = c.String(),
                        TotalScore = c.String(),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        HasMany = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        TestExamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TestsExam", t => t.TestExamId, cascadeDelete: true)
                .Index(t => t.TestExamId);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        QuestionId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        Correct = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestsDone", "UserId", "dbo.Users");
            DropForeignKey("dbo.TestsDone", "TestExamId", "dbo.TestsExam");
            DropForeignKey("dbo.Questions", "TestExamId", "dbo.TestsExam");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropIndex("dbo.Questions", new[] { "TestExamId" });
            DropIndex("dbo.TestsDone", new[] { "TestExamId" });
            DropIndex("dbo.TestsDone", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Answers");
            DropTable("dbo.Questions");
            DropTable("dbo.TestsExam");
            DropTable("dbo.TestsDone");
        }
    }
}
