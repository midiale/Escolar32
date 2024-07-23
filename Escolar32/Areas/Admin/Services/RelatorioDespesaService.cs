using Escolar32.Context;
using Escolar32.Models;
using Microsoft.EntityFrameworkCore;

namespace Escolar32.Areas.Admin.Services
{
    public class RelatorioDespesaService
    {
        private readonly AppDbContext _context;

        public RelatorioDespesaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Despesa>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resultado = from obj in _context.Despesas select obj;

            if (minDate.HasValue)
            {
                resultado = resultado.Where(x => x.DataDespesa >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                resultado = resultado.Where(x => x.DataDespesa <= maxDate.Value);
            }

            return await resultado
                         .OrderByDescending(x => x.DataDespesa)
                         .ToListAsync();
        }
    }
}
