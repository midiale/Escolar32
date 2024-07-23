using Escolar32.Context;
using Escolar32.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Escolar32.Areas.Admin.Services;

namespace Escolar32.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReceitasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly RelatorioReceitaService _relatorioReceitaService;

        public ReceitasController(AppDbContext context, RelatorioReceitaService relatorioReceitaService)
        {
            _context = context;
            _relatorioReceitaService = relatorioReceitaService;

        }
        
        // GET: ReceitasController
        public async Task<IActionResult> Receitas()
        {
            return View(await _context.Receitas.ToListAsync());
        }        

        //Relatorio de Receitas por período
        public async Task<IActionResult> ReceitaFiltro(DateTime? minDate,
            DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _relatorioReceitaService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        // GET: Admin/Receita/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Receita/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReceitaId,ReceitaNome,ValorReceita,DataReceita,ReceitaDetalhe,ReceitaMes,ReceitaAno")] Receita receita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receita);
                receita.MesReceita = receita.DataReceita.Month;
                receita.AnoReceita = receita.DataReceita.Year;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Receitas));
            }
            return View(receita);
        }

        // GET: ReceitasController/Details/5        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Receitas == null)
            {
                return NotFound();
            }

            var receita = await _context.Receitas
                .FirstOrDefaultAsync(m => m.ReceitaId == id);
            if (receita == null)
            {
                return NotFound();
            }

            return View(receita);
        }

        // GET: Admin/Receita/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Receitas == null)
            {
                return NotFound();
            }

            var receita = await _context.Receitas.FindAsync(id);
            if (receita == null)
            {
                return NotFound();
            }
            return View(receita);
        }

        // POST: Admin/Receita/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReceitaId,ReceitaNome,ValorReceita,DataReceita,ReceitaDetalhe,MesReceita,AnoReceita")] Receita receita)
        {
            if (id != receita.ReceitaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receita);
                    receita.MesReceita = receita.DataReceita.Month;
                    receita.AnoReceita = receita.DataReceita.Year;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceitaExists(receita.ReceitaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Receitas));
            }
            return View(receita);
        }

        // GET: Admin/Receita/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Receitas == null)
            {
                return NotFound();
            }

            var receita = await _context.Receitas
                .FirstOrDefaultAsync(m => m.ReceitaId == id);
            if (receita == null)
            {
                return NotFound();
            }

            return View(receita);
        }

        // POST: Admin/Receita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Receitas == null)
            {
                return Problem("Entity set 'AppDbContext.Receitas' is null.");
            }
            var receita = await _context.Receitas.FindAsync(id);
            if (receita != null)
            {
                _context.Receitas.Remove(receita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Receitas));
        }

        private bool ReceitaExists(int id)
        {
            return _context.Receitas.Any(e => e.ReceitaId == id);
        }

    }
}
