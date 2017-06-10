namespace LearnerDictionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeLanguageRelationNotNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Word", "LanguageId", "dbo.Language");
            DropIndex("dbo.Word", new[] { "LanguageId" });
            AlterColumn("dbo.Word", "LanguageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Word", "LanguageId");
            AddForeignKey("dbo.Word", "LanguageId", "dbo.Language", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Word", "LanguageId", "dbo.Language");
            DropIndex("dbo.Word", new[] { "LanguageId" });
            AlterColumn("dbo.Word", "LanguageId", c => c.Int());
            CreateIndex("dbo.Word", "LanguageId");
            AddForeignKey("dbo.Word", "LanguageId", "dbo.Language", "Id");
        }
    }
}
