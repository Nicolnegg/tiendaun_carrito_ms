using Microsoft.EntityFrameworkCore;
using Proyecto_Carrito.Models;

namespace Proyecto_Carrito.Data
{
    public class MyDbContext : DbContext
    {                                                                                                                                                                                                                 
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Carrito>? Carrito { get; set; }
        public DbSet<Producto_Carrito>? Producto_Carrito { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrito>().ToTable("Carrito");
            modelBuilder.Entity<Producto_Carrito>().ToTable("Producto_Carrito");
            modelBuilder.Entity<Producto_Carrito>().HasNoKey();
        }
    }
}

    
