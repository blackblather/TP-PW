namespace TP_PW.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Emprestimos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Emprestimos()
        {
            ArtigosEmprestimos = new HashSet<ArtigosEmprestimos>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(128)]
        public string IdUtilizador { get; set; }

        [Required]
        [StringLength(128)]
        public string IdEstado { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataEmprestimo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArtigosEmprestimos> ArtigosEmprestimos { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual EstadoEmprestimo EstadoEmprestimo { get; set; }
    }
}
