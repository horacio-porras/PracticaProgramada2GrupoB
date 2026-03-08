using Microsoft.EntityFrameworkCore;
using PracticaProgramada.Models;

namespace PracticaProgramada.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
    }
}
