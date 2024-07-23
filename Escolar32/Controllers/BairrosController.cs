using Escolar32.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escolar32.Controllers
{

    public class BairrosController : Controller
    {
        private readonly AppDbContext _context;

        public BairrosController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Bairros()
        {
            return View(await _context.Bairros.ToListAsync());
        }
    }
}
