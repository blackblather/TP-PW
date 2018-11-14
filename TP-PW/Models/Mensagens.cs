namespace TP_PW.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mensagens
    {
        public string Id { get; set; }

        [Column(TypeName = "text")]
        public string Mensagem { get; set; }

        [Required]
        [StringLength(128)]
        public string IdRemetente { get; set; }

        [Required]
        [StringLength(128)]
        public string IdRecetor { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual AspNetUsers AspNetUsers1 { get; set; }
    }
}
