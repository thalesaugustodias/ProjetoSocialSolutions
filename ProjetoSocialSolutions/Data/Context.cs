using Microsoft.EntityFrameworkCore;
using ProjetoSocialSolutions.Models;

namespace ProjetoSocialSolutions.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Clientes> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>()
                .HasIndex(p => new { p.Cpf, p.Email })
                .IsUnique(true);
        }
        public DbSet<Imovel> Imovel { get; set; }

    }
}
