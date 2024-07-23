using Escolar32.Models;

namespace Escolar32.Repositories.Interfaces
{
    public interface IEscolaRepository
    {
        IEnumerable<Escola> Escolas { get; }
        Escola GetEscolaById(int id);
    }
}
