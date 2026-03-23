using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2ACV.Models;

namespace PracticaMvcCore2ACV.Data
{
    public class LibrosContext : DbContext
    {
        public LibrosContext(DbContextOptions<LibrosContext> options) : base(options) { }
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Generos> Generos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<VistaPedidos> VistaPedidos { get; set; }
    }
}
