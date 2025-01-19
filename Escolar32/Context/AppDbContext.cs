using Escolar32.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Escolar32.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Escola> Escolas { get; set; }

        public DbSet<Bairro> Bairros { get; set; }

        public DbSet<Receita> Receitas { get; set; }

        public DbSet<Despesa> Despesas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aluno>()
                .HasOne(a => a.Escola)
                .WithMany(e => e.Alunos)
                .HasForeignKey(a => a.EscolaId)
                .IsRequired(false);
        }
        public DbSet<Escolar32.Models.Relatorio> Relatorio { get; set; }

    }
}