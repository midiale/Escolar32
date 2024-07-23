using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Escolar32.Models
{
    [Table("Bairros")]
    public class Bairro
    {
        public int BairroId { get; set; }

        [Required(ErrorMessage = "Informe o nome do Bairro")]
        [Display(Name = "Bairro")]
        public string BairroNome { get; set; }
        
    }
}
