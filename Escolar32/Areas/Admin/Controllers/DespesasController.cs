using Escolar32.Areas.Admin.Services;
using Escolar32.Context;
using Escolar32.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escolar32.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DespesasController : Controller
    {

        private readonly AppDbContext _context;
        private readonly RelatorioDespesaService _relatorioDespesaService;

        public DespesasController(AppDbContext context, RelatorioDespesaService relatorioDespesaService)
        {
            _context = context;
            _relatorioDespesaService = relatorioDespesaService;

        }

        // GET: DespesasController
        public async Task<IActionResult> Despesas()
        {
            return View(await _context.Despesas.ToListAsync());
        }

        //Relatório de Despesas por período
        public async Task<IActionResult> DespesaFiltro(DateTime? minDate,
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

            var result = await _relatorioDespesaService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }        

        // GET: Admin/Despesa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Despesa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DespesaId,DespesaNome,ValorDespesa,DataDespesa,DespesaDetalhe,TotalDespesaMes,TotalDespesaAno")] Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(despesa);
                despesa.MesDespesa = despesa.DataDespesa.Month;
                despesa.AnoDespesa = despesa.DataDespesa.Year;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Despesas));
            }
            return View(despesa);
        }

        // GET: DespesasController/Details/5        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Despesas == null)
            {
                return NotFound();
            }

            var despesa = await _context.Despesas
                .FirstOrDefaultAsync(m => m.DespesaId == id);
            if (despesa == null)
            {
                return NotFound();
            }

            return View(despesa);
        }

        // GET: Admin/Despesa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Despesas == null)
            {
                return NotFound();
            }

            var despesa = await _context.Despesas.FindAsync(id);
            if (despesa == null)
            {
                return NotFound();
            }
            return View(despesa);
        }

        // POST: Admin/Despesa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DespesaId,DespesaNome,ValorDespesa,DataDespesa,DespesaDetalhe,TotalDespesaMes,TotalDespesaAno")] Despesa despesa)
        {
            if (id != despesa.DespesaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(despesa);
                    despesa.MesDespesa = despesa.DataDespesa.Month;
                    despesa.AnoDespesa = despesa.DataDespesa.Year;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DespesaExists(despesa.DespesaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Despesas));
            }
            return View(despesa);
        }

        // GET: Admin/Despesa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Despesas == null)
            {
                return NotFound();
            }

            var despesa = await _context.Despesas
                .FirstOrDefaultAsync(m => m.DespesaId == id);
            if (despesa == null)
            {
                return NotFound();
            }

            return View(despesa);
        }

        // POST: Admin/Despesa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Despesas == null)
            {
                return Problem("Entity set 'AppDbContext.Despesas' is null.");
            }
            var despesa = await _context.Despesas.FindAsync(id);
            if (despesa != null)
            {
                _context.Despesas.Remove(despesa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Despesas));
        }

        private bool DespesaExists(int id)
        {
            return _context.Despesas.Any(e => e.DespesaId == id);
        }
    }
}
