using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;

namespace Escolar32.Controllers
{

    public class InicialController : Controller
    {

        readonly string novoaluno = "novoaluno";
        readonly string bemvindo = "bemvindo";

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string usuario, string senha)
        {
            if (usuario == "novoaluno" && senha == "bemvindo")
            {
                return RedirectToAction("Register", "Account");
            }
            else
            {
                ViewBag.ErrorMessage = "Usuaio/Senha incorretos";
                return View();
            }
        }

    }

}

