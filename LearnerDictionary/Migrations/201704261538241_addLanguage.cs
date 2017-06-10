namespace LearnerDictionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLanguage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Word", "LanguageId", c => c.Int());
            CreateIndex("dbo.Word", "Value", unique: true);
            CreateIndex("dbo.Word", "LanguageId");
            AddForeignKey("dbo.Word", "LanguageId", "dbo.Language", "Id");
            DropColumn("dbo.Word", "Language");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Word", "Language", c => c.String());
            DropForeignKey("dbo.Word", "LanguageId", "dbo.Language");
            DropIndex("dbo.Word", new[] { "LanguageId" });
            DropIndex("dbo.Word", new[] { "Value" });
            DropColumn("dbo.Word", "LanguageId");
            DropTable("dbo.Language");
        }
    }
}
