namespace LearnerDictionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WordAttempt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WordId = c.Int(nullable: false),
                        CreatedUtc = c.DateTime(nullable: false),
                        Recognize = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Word", t => t.WordId, cascadeDelete: true)
                .Index(t => t.WordId);
            
            CreateTable(
                "dbo.Word",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Language = c.String(),
                        Definition = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WordAttempt", "WordId", "dbo.Word");
            DropIndex("dbo.WordAttempt", new[] { "WordId" });
            DropTable("dbo.Word");
            DropTable("dbo.WordAttempt");
        }
    }
}
