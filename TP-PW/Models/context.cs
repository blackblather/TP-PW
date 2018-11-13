using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TP_PW.Models
{
    public class context : DbContext
    {
        public DbSet<espolio> Espolios { get; set; }
        public DbSet<emprestimo> Emprestimos { get; set; }
        public DbSet<mensagem> Mensagens { get; set; }
       // public DbSet<artigosemprestimos> EspolioEmprestimos { get; set; }
        public DbSet<estadoemprestimo> EstadoEmprestimos { get; set; }

       
    }
}
