using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP_PW.Models
{
    public partial class Tratamento
    {

        public int Id { get; set; }

        [Required]
        public int IdArtigo { get; set; }

        [Required]
        public DateTime Hora { get; set; }

        [Required]
        [StringLength(128)]
        public string IdUtilizador { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string DescricaoInicial { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Tratamentos { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string DescricaoFinal { get; set; }

        public virtual Artigo Artigo { get; set; }

    }
}