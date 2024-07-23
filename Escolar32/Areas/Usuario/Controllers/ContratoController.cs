using Escolar32.Context;
using Escolar32.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Escolar32.Areas.Usuario.Controllers
{
    [Area("Usuario")]
    [Authorize(Roles = "Member,Admin")]
    public class ContratoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAlunoRepository _alunoRepository;

        public ContratoController(UserManager<IdentityUser> userManager,
               AppDbContext context, IAlunoRepository alunoRepository)
        {
            _userManager = userManager;
            _context = context;
            _alunoRepository = alunoRepository;
        }
        public IActionResult Contrato()
        {
            var user = User.Identity.Name;

            var alunos = _alunoRepository.GetAlunoByUsuario(user).ToList();

            if (alunos.Count > 1)
            {
                return RedirectToAction(nameof(ContratoList));
            }

            else
                return View(alunos.First());
        }

        public IActionResult ContratoList()
        {
            var user = User.Identity.Name;

            var alunos = _alunoRepository.GetAlunoByUsuario(user).ToList();

            return View(alunos);
        }
    }
}
