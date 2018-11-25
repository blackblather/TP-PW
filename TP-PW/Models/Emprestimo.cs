namespace TP_PW.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Emprestimos")]
    public partial class Emprestimo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Emprestimo()
        {
            ArtigosEmprestimos = new HashSet<ArtigosEmprestimo>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string IdUtilizador { get; set; }

        public int IdEstado { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataEmprestimo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArtigosEmprestimo> ArtigosEmprestimos { get; set; }
    }
}
