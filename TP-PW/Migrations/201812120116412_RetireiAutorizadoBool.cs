namespace TP_PW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RetireiAutorizadoBool : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Autorizado");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Autorizado", c => c.Boolean(nullable: false));
        }
    }
}
