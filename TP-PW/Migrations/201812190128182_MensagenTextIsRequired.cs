namespace TP_PW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MensagenTextIsRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Mensagens", "Mensagem", c => c.String(nullable: false, unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Mensagens", "Mensagem", c => c.String(unicode: false, storeType: "text"));
        }
    }
}
