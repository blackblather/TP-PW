namespace TP_PW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Artigos", "AnoDescoberto", c => c.Int());
            AlterColumn("dbo.Mensagens", "Mensagem", c => c.String(nullable: false, unicode: false, storeType: "text"));
            AlterColumn("dbo.Mensagens", "HoraEnvio", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Mensagens", "HoraEnvio", c => c.Time(nullable: false, precision: 0));
            AlterColumn("dbo.Mensagens", "Mensagem", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("dbo.Artigos", "AnoDescoberto", c => c.DateTime());
        }
    }
}
