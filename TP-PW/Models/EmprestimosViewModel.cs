namespace TP_PW.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class EmprestimosViewModel
    {
        public Emprestimo emprestimo { get; set; }
        public ApplicationUser utilizador { get; set; }
        public ArtigosEmprestimo artigosEmprestimo { get; set; }
        public EstadoEmprestimo estadoEmprestimo { get; set; }
    }
}