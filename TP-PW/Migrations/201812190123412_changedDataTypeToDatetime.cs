namespace TP_PW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedDataTypeToDatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Mensagens", "HoraEnvio", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Mensagens", "HoraEnvio", c => c.Time(nullable: false, precision: 0));
        }
    }
}
