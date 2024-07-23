using Escolar32.Models;

namespace Escolar32.Repositories.Interfaces
{
    public interface IBairroRepository
    {
        IEnumerable<Bairro> Bairros { get; }
    }
}

