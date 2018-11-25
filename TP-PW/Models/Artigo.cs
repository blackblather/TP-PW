namespace TP_PW.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Artigos")]
    public partial class Artigo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Artigo()
        {
            ArtigosEmprestimos = new HashSet<ArtigosEmprestimo>();
        }

        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Column(TypeName = "text")]
        public string Descricao { get; set; }

        [Required]
        [StringLength(2000)]
        public string ImagemURL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArtigosEmprestimo> ArtigosEmprestimos { get; set; }
    }
}
