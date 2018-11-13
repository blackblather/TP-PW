using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP_PW.Models
{
    public class estadoemprestimo
    {
        [Key]
        public string Id { get; set; } //NVARCHAR(128)
        public string Estado { get; set; } //NVARCHAR(MAX)

    }
}