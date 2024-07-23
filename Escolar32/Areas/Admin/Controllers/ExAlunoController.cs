using Escolar32.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Escolar32.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ExAlunoController : Controller
    {
        private readonly IAlunoRepository _alunoRepository;

        public ExAlunoController(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public IActionResult List()
        {
            var exalunos = _alunoRepository.ExAlunos;
            return View(exalunos);
        }
    }
}
