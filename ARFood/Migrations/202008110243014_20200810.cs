namespace ARFood.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200810 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clientes", "IDUser", c => c.Guid(nullable: false));
            DropColumn("dbo.Documentos", "IDCliente");
            AddColumn("dbo.Documentos", "IDCliente", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Documentos", "IDCliente", c => c.Int(nullable: false));
            DropColumn("dbo.Clientes", "IDUser");
        }
    }
}
