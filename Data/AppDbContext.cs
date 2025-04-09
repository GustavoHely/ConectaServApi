using Microsoft.EntityFrameworkCore;
using PlataformaPrestadores.Api.Models;

namespace PlataformaPrestadores.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}