using Microsoft.AspNetCore.Mvc;

namespace Escolar32.Controllers
{
    public class EscolasController : Controller
    {
        public IActionResult Escolas()
        {
            return View();
        }
    }
}
