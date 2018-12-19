namespace TP_PW.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EstadoEmprestimo")]
    public partial class EstadoEmprestimo
    {
        public int Id { get; set; }

        public string Estado { get; set; }

        public virtual ICollection<Emprestimo> Emprestimos { get; set; }
    }
}
