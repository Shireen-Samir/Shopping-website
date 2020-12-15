namespace task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Proudects", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Proudects", new[] { "Category_CategoryId" });
            RenameColumn(table: "dbo.Proudects", name: "Category_CategoryId", newName: "CategoryId");
            AlterColumn("dbo.Proudects", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Proudects", "CategoryId");
            AddForeignKey("dbo.Proudects", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
            DropColumn("dbo.Proudects", "categoeyid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Proudects", "categoeyid", c => c.Int(nullable: false));
            DropForeignKey("dbo.Proudects", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Proudects", new[] { "CategoryId" });
            AlterColumn("dbo.Proudects", "CategoryId", c => c.Int());
            RenameColumn(table: "dbo.Proudects", name: "CategoryId", newName: "Category_CategoryId");
            CreateIndex("dbo.Proudects", "Category_CategoryId");
            AddForeignKey("dbo.Proudects", "Category_CategoryId", "dbo.Categories", "CategoryId");
        }
    }
}
