namespace LearnerDictionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addWordExample : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WordExample",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WordId = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Word", t => t.WordId, cascadeDelete: true)
                .Index(t => t.WordId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WordExample", "WordId", "dbo.Word");
            DropIndex("dbo.WordExample", new[] { "WordId" });
            DropTable("dbo.WordExample");
        }
    }
}
