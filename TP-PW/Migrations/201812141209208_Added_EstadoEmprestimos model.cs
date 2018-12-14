namespace TP_PW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_EstadoEmprestimosmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EstadoEmprestimo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Emprestimos", "IdEstado");
            AddForeignKey("dbo.Emprestimos", "IdEstado", "dbo.EstadoEmprestimo", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emprestimos", "IdEstado", "dbo.EstadoEmprestimo");
            DropIndex("dbo.Emprestimos", new[] { "IdEstado" });
            DropTable("dbo.EstadoEmprestimo");
        }
    }
}
