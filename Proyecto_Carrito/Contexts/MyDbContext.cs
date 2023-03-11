using Microsoft.EntityFrameworkCore;

namespace Proyecto_Carrito.Context
{
    public class MyDbContext : DbContext
    {                                                                                                                                                                                                                 
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public MyDbContext()
        {
        }

        public DbSet<Carrito>? Carritos { get; set; }
        public DbSet<Producto_Carrito>? ProductosCarritos { get; set; }
    }
}