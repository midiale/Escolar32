using Escolar32.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Escolar32.ViewModels
{
    public class CadastroViewModel
    {
        public Aluno Aluno { get; set; }

        public Escola Escola { get; set; }

        public Bairro Bairro { get; set; }

        public List<SelectListItem> ComboEscolas { get; set; }

        public List<SelectListItem> ComboSeries { get; set; }

    }
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Informe o e-mail")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Informe a Senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

    }

    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A senha deverá conter 8 dígitos com números, letras maiúsculas, minúsculas e caracteres especiais", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação NÃO conferem")]
        public string ConfirmPassword { get; set; }
        public string ReturnUrl { get; set; }
    }   

    public class ResetPasswordViewModel
    {
        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A senha deverá conter 8 dígitos com números, letras maiúsculas, minúsculas e caracteres especiais", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação NÃO conferem")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        [Display(Name = "Email")]
        public string UserName { get; set; }
    }
}
