namespace TP_PW.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class EmprestimosUsersViewModel
    {
        public Emprestimo emprestimo { get; set; }
        public ApplicationUser utilizador { get; set; }
        
        public EmprestimosUsersViewModel(Emprestimo emp, ApplicationUser usr)
        {
            emprestimo = emp;
            utilizador = usr;
        }
    }
}