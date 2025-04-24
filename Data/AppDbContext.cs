using Microsoft.EntityFrameworkCore;
using ConectaServApi.Models;

namespace ConectaServApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Prestador> Prestadores { get; set; }

    }
}