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
        public DbSet<Imovel> Imovel { get; set; }

    }
}
