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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdArtigo { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdEmprestimo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataRetornoArtigo { get; set; }

        public virtual Artigo Artigo { get; set; }

        public virtual Emprestimo Emprestimo { get; set; }
    }
}
