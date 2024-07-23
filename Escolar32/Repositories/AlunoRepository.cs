using Escolar32.Context;
using Escolar32.Models;
using Escolar32.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Escolar32.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDbContext _context;

        public AlunoRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Aluno> Alunos => _context.Alunos.Include(y => y.Escola);

        public IEnumerable<Aluno> ExAlunos => _context.Alunos.Include(y=>y.Escola).Where(x => x.ExAluno);

        public IEnumerable<Aluno> NomeUsuario => _context.Alunos.Include(y => y.Escola);


        public Aluno GetAlunoById(int alunoId)
        {
            return _context.Alunos.Include(y => y.Escola).FirstOrDefault(a => a.AlunoId == alunoId);
        }

        public IQueryable<Aluno> GetAlunoByUsuario(string nomeUsuario)
        {
            return _context.Alunos.Include(y => y.Escola).Where(a => a.NomeUsuario == nomeUsuario);
        }

        public Aluno GetAlunoByContrato(string nomeUsuario)
        {
            return _context.Alunos.Include(y => y.Escola).FirstOrDefault(a => a.NomeUsuario == nomeUsuario);
        }

    }
}
