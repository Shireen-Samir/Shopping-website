namespace task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Proudects", "Proudctimage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Proudects", "Proudctimage");
        }
    }
}
