using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Escolar32.Context;
using Escolar32.Models;
using Microsoft.AspNetCore.Authorization;

namespace Escolar32.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EscolasController : Controller
    {
        private readonly AppDbContext _context;

        public EscolasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Escolas
        public async Task<IActionResult> Index()
        {
              return View(await _context.Escolas.ToListAsync());
        }

        
        // GET: Admin/Escolas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Escolas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EscolaId,EscolaNome,Endereco")] Escola escola)
        {
            if (ModelState.IsValid)
            {
                _context.Add(escola);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(escola);
        }

        // GET: Admin/Escolas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Escolas == null)
            {
                return NotFound();
            }

            var escola = await _context.Escolas.FindAsync(id);
            if (escola == null)
            {
                return NotFound();
            }
            return View(escola);
        }

        // POST: Admin/Escolas/Edit/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EscolaId,EscolaNome,Endereco")] Escola escola)
        {
            if (id != escola.EscolaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(escola);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EscolaExists(escola.EscolaId))
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
            return View(escola);
        }

        // GET: Admin/Escolas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Escolas == null)
            {
                return NotFound();
            }

            var escola = await _context.Escolas.Include(e => e.Alunos)
                .FirstOrDefaultAsync(m => m.EscolaId == id);
            if (escola == null)
            {
                return NotFound();
            }

            return View(escola);
        }

        // POST: Admin/Escolas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Escolas == null)
            {
                return Problem("Entity set 'AppDbContext.Escolas' is null.");
            }

            var escola = await _context.Escolas.FindAsync(id);

            if (escola == null)
            {
                return NotFound();
            }

            // Defina o EscolaId como null para todos os alunos associados a esta escola
            var alunosDaEscola = _context.Alunos.Where(a => a.EscolaId == id);
            foreach (var aluno in alunosDaEscola)
            {
                aluno.EscolaId = null;
            }

            _context.Escolas.Remove(escola);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool EscolaExists(int id)
        {
          return _context.Escolas.Any(e => e.EscolaId == id);
        }
    }
}
