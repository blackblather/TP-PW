namespace TP_PW.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mensagem
    {
        public string Id { get; set; }

        [Column(TypeName = "text")]
        public string Texto { get; set; }

        [Required]
        [StringLength(128)]
        public string IdRemetente { get; set; }

        [Required]
        [StringLength(128)]
        public string IdRecetor { get; set; }
    }
}
