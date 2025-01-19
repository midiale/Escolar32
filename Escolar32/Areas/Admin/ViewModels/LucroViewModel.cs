using Escolar32.Models;

namespace Escolar32.Areas.Admin.ViewModels
{
    public class LucroViewModel
    {
        public IEnumerable<Aluno> Alunos { get; set; }
        public decimal[] TotalMeses { get; set; }
        public decimal[] TotalReceitas { get; set; }
        public decimal[] TotalDespesas { get; set; }
        public decimal[] TotalLucroPorMes { get; set; }
        public decimal TotalLucro { get; set; }
        public int? AnosDespesa { get; set; }

    }
}
