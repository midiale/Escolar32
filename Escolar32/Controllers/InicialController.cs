using Microsoft.AspNetCore.Mvc;

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
            if(!ModelState.IsValid)
                return View();
                if (usuario == novoaluno && senha == bemvindo)
                {
                return RedirectToAction("Register", "Account");
                    
                }
            
                else
                {
                    return View();
                }
        }
    }
}
