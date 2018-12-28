namespace TP_PW.Models
{

    public class EmprestimosViewModel
    {
        public Emprestimo emprestimo { get; set; }
        public ApplicationUser utilizador { get; set; }
        public ArtigosEmprestimo artigosEmprestimo { get; set; }
        public EstadoEmprestimo estadoEmprestimo { get; set; }
    }
}