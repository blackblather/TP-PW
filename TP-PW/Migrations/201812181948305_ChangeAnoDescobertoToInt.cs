namespace TP_PW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAnoDescobertoToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Artigos", "AnoDescoberto", c => c.String());
            AlterColumn("dbo.Artigos", "AnoDescoberto", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Artigos", "AnoDescoberto", c => c.String());
            AlterColumn("dbo.Artigos", "AnoDescoberto", c => c.DateTime());
        }
    }
}
