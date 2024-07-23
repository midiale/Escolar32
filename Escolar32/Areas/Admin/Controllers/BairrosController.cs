using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Escolar32.Context;
using Escolar32.Models;
using Microsoft.AspNetCore.Authorization;

namespace Escolar32.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BairrosController : Controller
    {
        private readonly AppDbContext _context;

        public BairrosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Bairro
        public async Task<IActionResult> Index()
        {
              return View(await _context.Bairros.ToListAsync());
        }

        // GET: Admin/Bairro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bairros == null)
            {
                return NotFound();
            }

            var bairro = await _context.Bairros
                .FirstOrDefaultAsync(m => m.BairroId == id);
            if (bairro == null)
            {
                return NotFound();
            }

            return View(bairro);
        }

        // GET: Admin/Bairro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Bairro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BairroId,BairroNome")] Bairro bairro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bairro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bairro);
        }

        // GET: Admin/Bairro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bairros == null)
            {
                return NotFound();
            }

            var bairro = await _context.Bairros.FindAsync(id);
            if (bairro == null)
            {
                return NotFound();
            }
            return View(bairro);
        }

        // POST: Admin/Bairro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BairroId,BairroNome")] Bairro bairro)
        {
            if (id != bairro.BairroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bairro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BairroExists(bairro.BairroId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bairro);
        }

        // GET: Admin/Bairro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bairros == null)
            {
                return NotFound();
            }

            var bairro = await _context.Bairros
                .FirstOrDefaultAsync(m => m.BairroId == id);
            if (bairro == null)
            {
                return NotFound();
            }

            return View(bairro);
        }

        // POST: Admin/Bairro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bairros == null)
            {
                return Problem("Entity set 'AppDbContext.Bairros'  is null.");
            }
            var bairro = await _context.Bairros.FindAsync(id);
            if (bairro != null)
            {
                _context.Bairros.Remove(bairro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BairroExists(int id)
        {
          return _context.Bairros.Any(e => e.BairroId == id);
        }
    }
}
