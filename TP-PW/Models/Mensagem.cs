using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP_PW.Models
{
    public class mensagem
    {
        [Key]
        public string Id { get; set; } //NVARCHAR(128)
        public string Mensagem { get; set; } //text
        //[ForeignKey()]
        public string IdR { get; set; } //NVARCHAR(128)
        //[ForeignKey()]
        public string IdD { get; set; } //NVARCHAR(128)
    }
}