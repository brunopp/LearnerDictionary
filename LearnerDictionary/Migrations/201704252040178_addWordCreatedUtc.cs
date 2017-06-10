namespace LearnerDictionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addWordCreatedUtc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Word", "CreatedUtc", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Word", "CreatedUtc");
        }
    }
}
