using Escolar32.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Escolar32.Models
{

    [Table("Alunos")]
    public class Aluno: IValidatableObject
    {
        public int AlunoId { get; set; }

        [StringLength(50, MinimumLength = 10, ErrorMessage = "Mínimo 10 caracteres")]
        [Required(ErrorMessage = "Informe o nome do Aluno")]
        [Display(Name = "Nome do Aluno")]
        public string Nome { get; set; }

        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "Informe a data de Nascimento")]
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [BindProperty, DataType(DataType.Date)]

        public DateTime DataNasc { get; set; }

        [StringLength(50, MinimumLength = 10, ErrorMessage = "O tamanho deve ser entre 10 e 50 caracteres")]
        [Required(ErrorMessage = "Informe o nome da mãe")]
        [Display(Name = "Nome da mãe")]
        public string Mae { get; set; }

        [StringLength(50, MinimumLength = 10, ErrorMessage = "O tamanho deve ser entre 10 e 50 caracteres")]
        [Display(Name = "Nome do pai")]
        public string Pai { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }

        [Required(ErrorMessage = "N° não informado")]
        [Display(Name = "N°")]
        public string NumeroCasa { get; set; }

        [StringLength(15, MinimumLength = 2)]
        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe um  telefone")]
        [Display(Name = "Telefone")]
        public string Telefone1 { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone2 { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone3 { get; set; }

        [Required(ErrorMessage = "Preencha S ou N")]
        [Display(Name = "Já utilizou Van Escolar?")]
        public bool VanAnterior { get; set; }

        [Display(Name = "Qual ?")]
        public string QualEscolar { get; set; }

        //[Required(ErrorMessage = "Informe a escola")]
        [Display(Name = "Escola")]
        public int? EscolaId { get; set; }
        
        public virtual Escola Escola { get; set; }

        [StringLength(10, ErrorMessage = "O tamanho máximo é 10 caracteres")]
        [Required(ErrorMessage = "Informe a série")]
        [Display(Name = "Série")]
        public string Serie { get; set; }

        [Required(ErrorMessage = "Informe o Período")]
        public string Periodo { get; set; }

        [StringLength(50, MinimumLength = 10, ErrorMessage = "O tamanho deve ser entre 10 e 50 caracteres")]
        [Required(ErrorMessage = "Informe o nome Responsável Financeiro")]
        [Display(Name = "Responsável financeiro")]
        public string RespFinan { get; set; }

        [StringLength(20, ErrorMessage = "O tamanho máximo é 20 caracteres")]
        [Required(ErrorMessage = "Informe o número do RG")]
        [Display(Name = "RG")]
        public string Rg { get; set; }

        [ValidarCPF(ErrorMessage = "CPF Inválido")]
        [StringLength(14, ErrorMessage = "O tamanho máximo é 14 caracteres")]
        [Required(ErrorMessage = "Informe o CPF")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [StringLength(30, MinimumLength = 05, ErrorMessage = "O tamanho deve ser entre 05 e 30 caracteres")]
        [Required(ErrorMessage = "Informe sua Profissão")]
        [Display(Name = "Profissão")]
        public string Profissao { get; set; }

        [Required(ErrorMessage = "Preencha S ou N")]
        [Display(Name = "Firma em Cartório?")]
        public bool FirmaRec { get; set; }

        [StringLength(20, ErrorMessage = "O tamanho máximo é 20 caracteres")]
        [Display(Name = "Qual?")]
        public string Cartorio { get; set; }

        [Display(Name = "Primeiro Pagamento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [BindProperty, DataType(DataType.Date)]
        public DateTime InicioPgto { get; set; }

        [Display(Name = "Último Pagamento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [BindProperty, DataType(DataType.Date)]
        public DateTime FimPgto { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Valor Parcela")]
        [Range(0, double.MaxValue, ErrorMessage = "O valor da parcela deve ser maior que 0")]
        public decimal ValorParcela { get; set; }
        
        [Display(Name = "Qt. Parcelas")]
        [Range(0, double.MaxValue, ErrorMessage = "A quantidade das parcela deve ser maior que 1")]
        public int QtdeParcelas { get; set; }
         
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Total do Contrato")]
        public decimal TotalContrato { get; set; }

        [Display(Name = "Inicio do Transporte")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [BindProperty, DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Fim do Transporte")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [BindProperty, DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [Display(Name = "Data do Cadastro")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Ex Aluno")]
        public bool ExAluno { get; set; }
        public bool Jan { get; set; }
        
        public bool Fev { get; set; }

        public bool Mar { get; set; }

        public bool Abr { get; set; }

        public bool Mai { get; set; }

        public bool Jun { get; set; }

        public bool Jul { get; set; }

        public bool Ago { get; set; }

        public bool Set { get; set; }

        public bool Out { get; set; }

        public bool Nov { get; set; }

        public bool Dez { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DataInicio.AddDays(30) < DataCadastro)
            {
                yield return new ValidationResult("A data de início do transporte não pode ser anterior à data de cadastro.", new[] { nameof(DataInicio) });
            }
            if (DataFim < DataInicio)
            {
                yield return new ValidationResult("A data do fim do transporte não pode ser anterior à data de inicio do transporte.", new[] { nameof(DataInicio) });
            }
        }        
    }   
}
