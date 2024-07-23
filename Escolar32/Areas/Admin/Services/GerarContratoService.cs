using Escolar32.Context;
using Escolar32.Models;
using Microsoft.EntityFrameworkCore;

namespace Escolar32.Areas.Admin.Services
{
    public class GerarContratoService
    {
        
        private readonly AppDbContext _context;
        public GerarContratoService(AppDbContext context)
        
        {
            _context = context;
        }
        public async Task<IEnumerable<Aluno>> GerarContratosReport(int id)
        {

            var alunos = await _context.Alunos.FirstOrDefaultAsync(a => a.AlunoId == id);


            if (alunos != null)
            {

                return new List<Aluno> { alunos };

            }
            else
            {

                return Enumerable.Empty<Aluno>();
            };



        }

    }
}