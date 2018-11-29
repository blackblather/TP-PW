namespace TP_PW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTabelaTratamento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tratamentoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdArtigo = c.Int(nullable: false),
                        Hora = c.DateTime(nullable: false),
                        IdUtilizador = c.String(nullable: false, maxLength: 128),
                        DescricaoInicial = c.String(nullable: false, unicode: false, storeType: "text"),
                        Tratamentos = c.String(nullable: false, unicode: false, storeType: "text"),
                        DescricaoFinal = c.String(nullable: false, unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artigos", t => t.IdArtigo, cascadeDelete: true)
                .Index(t => t.IdArtigo);
            
            AddColumn("dbo.Artigos", "Tipo", c => c.Byte(nullable: false));
            AddColumn("dbo.Artigos", "Origem", c => c.String());
            AddColumn("dbo.Artigos", "AnoDescoberto", c => c.DateTime());
            AddColumn("dbo.Artigos", "ZonaDescoberto", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tratamentoes", "IdArtigo", "dbo.Artigos");
            DropIndex("dbo.Tratamentoes", new[] { "IdArtigo" });
            DropColumn("dbo.Artigos", "ZonaDescoberto");
            DropColumn("dbo.Artigos", "AnoDescoberto");
            DropColumn("dbo.Artigos", "Origem");
            DropColumn("dbo.Artigos", "Tipo");
            DropTable("dbo.Tratamentoes");
        }
    }
}
