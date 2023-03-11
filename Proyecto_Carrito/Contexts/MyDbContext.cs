using Microsoft.EntityFrameworkCore;
using Proyecto_Carrito.Models;

namespace Proyecto_Carrito.Context
{
    public class MyDbContext : DbContext
    {                                                                                                                                                                                                                 
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            ProductosCarritos = new List<Producto_Carrito>();
        }

        public DbSet<Carrito>? Carritos { get; set; }
        public List<Producto_Carrito> ProductosCarritos { get; set; }
    }
}