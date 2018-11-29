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

        [Required]
        public tipoPeca Tipo { get; set; }

        public string Origem { get; set; }

        public DateTime? AnoDescoberto { get; set; }

        public string ZonaDescoberto { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArtigosEmprestimo> ArtigosEmprestimos { get; set; }

        public virtual ICollection<Tratamento> Tratamentos { get; set; }


     
    }

    public class ArtigosViewModel
    {
        [Required, Display(Name = "Nome do Artigo")]
        public string Nome { get; set; }

        [Required, Display(Name = "Imagem")]
        [StringLength(2000)]
        public string ImagemURL { get; set; }

        [Required, Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required, Display(Name = "Tipo de Peça")]
        public tipoPeca Tipo { get; set; }

        [Display(Name = "Origem")]
        public string Origem { get; set; }

        [Display(Name = "Ano que foi Descoberto")]
        public DateTime? AnoDescoberto { get; set; }

        [Display(Name = "Zona onde foi descoberto")]
        public string ZonaDescoberto { get; set; }

    }

    public enum tipoPeca : byte
    {
        Desconhecido = 0,
        Pintura = 1,
        Escultura = 2,
        Artefacto = 3,
        Fossil = 4

    }
}
