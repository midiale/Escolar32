using Escolar32.Context;
using Escolar32.Models;
using Escolar32.Repositories.Interfaces;

namespace Escolar32.Repositories
{
    public class BairroRepository : IBairroRepository
    {
        private readonly AppDbContext _context;

        public BairroRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Bairro> Bairros => _context.Bairros;
    }
}

