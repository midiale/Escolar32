using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Escolar32.Models
{
    public class Despesa
    {
        public int DespesaId { get; set; }

        [Required]
        [Display(Name = "Nome da Despesa")]
        public string DespesaNome { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Valor")]
        public decimal ValorDespesa { get; set; }        

        [Required]
        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [BindProperty, DataType(DataType.Date)]
        public DateTime DataDespesa { get; set; }

        [Display(Name = "Detalhe da Despesa")]
        public string DespesaDetalhe { get; set; }       
        
        [Display(Name = "Despesas do Mês")]
        public int MesDespesa { get; set; }
        
        [Display(Name = "Despesas do Ano")]
        public int AnoDespesa { get; set; }
    }
}
