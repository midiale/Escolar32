using Escolar32.Context;
using Escolar32.Models;
using Escolar32.Repositories.Interfaces;

namespace Escolar32.Repositories
{
    public class EscolaRepository:IEscolaRepository
    {
        private readonly AppDbContext _context;

        public EscolaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Escola> Escolas => _context.Escolas;
                
        public Escola GetEscolaById(int id)
        {
            var escolas = _context.Escolas.FirstOrDefault(a => a.EscolaId == id);
            return escolas;
        }
    }

    
}
