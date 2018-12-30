namespace TP_PW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMensagensAssunto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mensagens", "Assunto", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mensagens", "Assunto");
        }
    }
}
