using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TP_PW.Models
{
    public class artigosemprestimos
    {
        [Key, ForeignKey("espolio"), Column(Order = 0)]
        public string IdEspolio { get; set; } //NVARCHAR(128)
        [Key, ForeignKey("emprestimo"), Column(Order = 1)]
        public string IdEmprestimo { get; set; } //NVARCHAR(128)
        public DateTime DataEntregue { get; set; } //DATE

        [JsonIgnore]
        public espolio espolio { get; set; }
        [JsonIgnore]
        public emprestimo emprestimo { get; set; }


    }
}