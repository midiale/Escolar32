using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Escolar32.Models
{
    public class Receita
    {
        public int ReceitaId { get; set; }

        [Display(Name = "Nome da Receita")]
        public string ReceitaNome { get; set;}

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Valor")]
        public decimal ValorReceita { get; set; }

        [Required]
        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [BindProperty, DataType(DataType.Date)]
        public DateTime DataReceita{ get; set; }

        [Display(Name = "Detalhe da Receita")]
        public string ReceitaDetalhe { get; set; }

        [Display(Name = "Receita do Mês")]
        public int MesReceita { get; set; }
        
        [Display(Name = "Receita do Ano")]
        public int AnoReceita { get; set; }

    }
}
