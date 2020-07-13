namespace ARFood.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AR20200711 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComplementoProductos", "DocPartidas_ID", c => c.Int());
            AddColumn("dbo.DocPartidas", "Descripcion", c => c.String());
            AddColumn("dbo.DocPartidasPersonalizars", "Descripcion", c => c.String());
            AddColumn("dbo.Documentos", "Estatus", c => c.String());
            //CreateIndex("dbo.ComplementoProductos", "DocPartidas_ID");
            //AddForeignKey("dbo.ComplementoProductos", "DocPartidas_ID", "dbo.DocPartidas", "ID");
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.ComplementoProductos", "DocPartidas_ID", "dbo.DocPartidas");
            //DropIndex("dbo.ComplementoProductos", new[] { "DocPartidas_ID" });
            DropColumn("dbo.Documentos", "Estatus");
            DropColumn("dbo.DocPartidasPersonalizars", "Descripcion");
            DropColumn("dbo.DocPartidas", "Descripcion");
            DropColumn("dbo.ComplementoProductos", "DocPartidas_ID");
        }
    }
}
