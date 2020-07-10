namespace ARFood.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200708 : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.Documentos");
            AddColumn("dbo.IngredientesxRecetas", "AddorRemove", c => c.Boolean(nullable: false));
            AddColumn("dbo.IngredientesxRecetas", "Precio", c => c.Int(nullable: false));
            AddColumn("dbo.Documentos", "ID", c => c.Guid(nullable: false));
            AddColumn("dbo.DocPartidas", "IDDoc", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Documentos", "ID");
        }
        
        public override void Down()
        {
            //DropPrimaryKey("dbo.Documentos");
            //AlterColumn("dbo.Documentos", "ID", c => c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.DocPartidas", "IDDoc", c => c.Int(nullable: false));
            DropColumn("dbo.IngredientesxRecetas", "Precio");
            DropColumn("dbo.IngredientesxRecetas", "AddorRemove");
            AddPrimaryKey("dbo.Documentos", "ID");
        }
    }
}
