using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP_PW.Models
{
    public class espolio
    {
        [Key]
        public string Id { get; set; } // NVARCHAR(128)
        public string Nome { get; set; } // NVARCHAR(MAX)
        public string Descricao { get; set; } // TEXT
    }
}