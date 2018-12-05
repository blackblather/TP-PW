namespace TP_PW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoolAutorizado_E_TUFK : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tratamentoes", name: "IdArtigo", newName: "ArtigoId");
            RenameIndex(table: "dbo.Tratamentoes", name: "IX_IdArtigo", newName: "IX_ArtigoId");
            AddColumn("dbo.Tratamentoes", "UtilizadorId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Autorizado", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Tratamentoes", "UtilizadorId");
            AddForeignKey("dbo.Tratamentoes", "UtilizadorId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Tratamentoes", "IdUtilizador");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tratamentoes", "IdUtilizador", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Tratamentoes", "UtilizadorId", "dbo.AspNetUsers");
            DropIndex("dbo.Tratamentoes", new[] { "UtilizadorId" });
            DropColumn("dbo.AspNetUsers", "Autorizado");
            DropColumn("dbo.Tratamentoes", "UtilizadorId");
            RenameIndex(table: "dbo.Tratamentoes", name: "IX_ArtigoId", newName: "IX_IdArtigo");
            RenameColumn(table: "dbo.Tratamentoes", name: "ArtigoId", newName: "IdArtigo");
        }
    }
}
