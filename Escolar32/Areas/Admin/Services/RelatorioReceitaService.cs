using Escolar32.Context;
using Escolar32.Models;
using Microsoft.EntityFrameworkCore;

namespace Escolar32.Areas.Admin.Services
{
    public class RelatorioReceitaService
    {
        private readonly AppDbContext _context;

        public RelatorioReceitaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Receita>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resultado = from obj in _context.Receitas select obj;

            if (minDate.HasValue)
            {
                resultado = resultado.Where(x => x.DataReceita >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                resultado = resultado.Where(x => x.DataReceita <= maxDate.Value);
            }

            return await resultado                         
                         .OrderByDescending(x => x.DataReceita)
                         .ToListAsync();
        }
    }
}
