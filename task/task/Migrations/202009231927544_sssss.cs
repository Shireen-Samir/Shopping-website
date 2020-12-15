namespace task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sssss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Categoryimage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Categoryimage");
        }
    }
}
