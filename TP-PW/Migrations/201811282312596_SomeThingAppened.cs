namespace TP_PW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeThingAppened : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DataNascimento", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "Aniversario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Aniversario", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "DataNascimento");
        }
    }
}
