namespace TP_PW.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ArtigosEmprestimos")]
    public partial class ArtigosEmprestimo
    {
        [Key]
        [Column(Order = 0)]
        public string IdArtigo { get; set; }

        [Key]
        [Column(Order = 1)]
        public string IdEmprestimo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataRetornoArtigo { get; set; }

        public virtual Artigo Artigo { get; set; }

        public virtual Emprestimo Emprestimo { get; set; }
    }
}
