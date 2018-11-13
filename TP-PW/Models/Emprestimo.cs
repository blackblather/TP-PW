using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP_PW.Models
{
    public class emprestimo
    {
        [Key]
        public string Id { get; set; } // NVARCHAR(128)
        //[ForeignKey()]
        public string IdU { get; set; } // NVARCHAR(128)
        public DateTime DataEmprestimo { get; set; } // DATE
    }
}