namespace TP_PW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PrimeiroNome", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Apelido", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Aniversario", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Aniversario");
            DropColumn("dbo.AspNetUsers", "Apelido");
            DropColumn("dbo.AspNetUsers", "PrimeiroNome");
        }
    }
}
