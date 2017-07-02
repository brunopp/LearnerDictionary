namespace LearnerDictionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsLearning : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Word", "IsLearning", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Word", "IsLearning");
        }
    }
}
