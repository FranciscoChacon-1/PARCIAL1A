using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Security.Cryptography;
namespace PARCIAL1A.Models
{
    public class Parcial1AContext : DbContext
    {
        public Parcial1AContext(DbContextOptions<Parcial1AContext> options) : base(options)
        {

        }
        public DbSet<Autores> Autores { get; set; }
        public DbSet<Autorlibro> Autorlibro { get; set;}
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Posts> Posts { get; set; }

    }
}
