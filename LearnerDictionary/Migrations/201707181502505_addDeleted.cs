namespace LearnerDictionary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Word", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Word", "Deleted");
        }
    }
}
