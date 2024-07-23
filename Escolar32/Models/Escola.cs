using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Escolar32.Models
{
    [Table("Escolas")]
    public class Escola
    {
        public int EscolaId { get; set; }
        
        [Required(ErrorMessage = "Informe o nome da Escola")]
        [Display(Name = "Escola")]
        public string EscolaNome { get; set; }

        [Required(ErrorMessage = "Informe o Endereço")]
        [Display(Name = "Endereço Rua, N°, Complemento")]
        public string Endereco { get; set; }

        [ForeignKey("EscolaId")]
        public ICollection<Aluno> Alunos { get; set; }
    }
}
