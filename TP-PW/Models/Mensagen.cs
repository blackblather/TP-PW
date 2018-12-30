using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace TP_PW.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mensagen
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Mensagem { get; set; }

        [Required]
        [StringLength(128)]
        public string Assunto { get; set; }


        [Required]
        [StringLength(128)]
        public string IdRemetente { get; set; }

        [Required]
        [StringLength(128)]
        public string IdRecetor { get; set; }

        [Required]
        public DateTime HoraEnvio { get; set; }
    }

    public class MensagenViewModel
    {

        public int Id { get; set; }

        [Required]
        public string Mensagem { get; set; }

        [Required]
        public string Assunto { get; set; }

        [Required]
        public string Recetor { get; set; }

        public string Remetente { get; set; }

        [Required]
        public DateTime HoraEnvio { get; set; }

    }

   
}
