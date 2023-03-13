using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Proyecto_Carrito.Models;
using System.Collections.Generic;
using Proyecto_Carrito.Data;

namespace Proyecto_Carrito.Controllers{

    [Route("api/carrito")]

    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CarritoController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto_Carrito>>> Get()
        {

            if (_context.Producto_Carrito== null)
            {
                return NotFound();
            }
            return await _context.Producto_Carrito.ToListAsync();                       
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Producto_Carrito>>> GetProductoCarritoById(int id)
        {
            if (_context.Producto_Carrito== null)
            {
                return NotFound();
            }
            var productoCarrito = await (_context.Producto_Carrito).Where(p => p.IdProducto == id).ToListAsync();
            
            return  productoCarrito;
        }
        
        [HttpPost]
        public ActionResult<Producto_Carrito> AgregarProductoCarrito(Producto_Carrito productoCarrito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!_context.Producto_Carrito.Any(p => p.IdCarrito == productoCarrito.IdCarrito)){
                
                _context.Producto_Carrito.Add(productoCarrito);
            }
            else{
                var existingProductoCarrito = _context.Producto_Carrito.FirstOrDefault(p => p.IdCarrito == productoCarrito.IdCarrito);
                existingProductoCarrito.CantProducto += productoCarrito.CantProducto;
                
                _context.Entry(existingProductoCarrito).Property(x => x.CantProducto).IsModified = true;
            }
            _context.SaveChanges();
            


            return CreatedAtAction(nameof(Get), new { IdProducto = productoCarrito.IdProducto }, productoCarrito);
        }
        // [HttpDelete("{id}/{cantidad}")]
        // public async Task<ActionResult> EliminarProductoCarrito(int id, int cantidad)
        // {
        //     var productoCarrito = await _context.Producto_Carrito.FindAsync(id);

        //     if (productoCarrito == null)
        //     {
        //         return NotFound();
        //     }
        //     if(productoCarrito.CantProducto==1){
        //         _context.Producto_Carrito.Remove(productoCarrito);

        //     }
        //     else{
        //         productoCarrito.CantProducto = productoCarrito.CantProducto-cantidad;
        //         _context.Entry(productoCarrito).Property(x => x.Nombre).IsModified = true;
        //     }

            
        //     await _context.SaveChangesAsync();

        //     return Ok();
        // }

    }

}
