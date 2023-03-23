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

        [HttpGet("productocarrito")]
        public async Task<ActionResult<IEnumerable<Producto_Carrito>>> GetProductosCarrito()
        {

            if (_context.Producto_Carrito== null)
            {
                return NotFound();
            }
            return await _context.Producto_Carrito.ToListAsync();                       
        }

        [HttpGet("productocarrito/{id}")]
        public async Task<ActionResult<IEnumerable<Producto_Carrito>>> GetProductoCarritoByIdProducto(int id)
        {
            if (_context.Producto_Carrito== null)
            {
                return NotFound();
            }
            var productoCarrito = await (_context.Producto_Carrito).Where(p => p.IdProducto == id).ToListAsync();
            
            return  productoCarrito;
        }
        
        [HttpPost("productocarrito")]
        public ActionResult<Producto_Carrito> AgregarProductoCarrito(Producto_Carrito productoCarrito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Producto_Carrito == null)
            {
                return NotFound();
            }
            if(!_context.Producto_Carrito.Any(p => p.IdProducto == productoCarrito.IdProducto && p.IdCarrito == productoCarrito.IdCarrito)){
                _context.Producto_Carrito.Add(productoCarrito);
                _context.SaveChanges();
                return Ok("Fue agregado el producto");
            }
            else{
                var actualproducto = _context.Producto_Carrito.FirstOrDefault(p => p.IdProducto == productoCarrito.IdProducto && p.IdCarrito == productoCarrito.IdCarrito);

                if (actualproducto == null)
                {
                    return NotFound();
                }

                actualproducto.CantProducto += productoCarrito.CantProducto;
                _context.Entry(actualproducto).Property(x => x.CantProducto).IsModified = true;
                _context.SaveChanges();
                return Ok("Se actualizo la cantidad");
            }
            
        }
        
        [HttpPut("productocarrito/{idProducto}")]
        public IActionResult ActualizarCantProductoCarrito(int idProducto, [FromBody] Producto_Carrito productoCarrito)
        {
            if (_context.Producto_Carrito == null)
            {
                return NotFound();
            }
            var actualproducto = _context.Producto_Carrito.FirstOrDefault(p => p.IdProducto == idProducto && p.IdCarrito == productoCarrito.IdCarrito);

            if (actualproducto == null)
            {
                return NotFound();
            }

            actualproducto.CantProducto += productoCarrito.CantProducto;
            _context.Entry(actualproducto).Property(x => x.CantProducto).IsModified = true;
            _context.SaveChanges();

            return Ok("actualizado");
        }

        [HttpDelete("productocarrito/{idProducto}")]
        public async Task<ActionResult> EliminarProductoCarrito(int idProducto, [FromBody] Producto_Carrito productoCarrito)
        {
            if (_context.Producto_Carrito == null)
            {
                return NotFound();
            }
            var actualproducto = _context.Producto_Carrito.FirstOrDefault(p => p.IdProducto == idProducto && p.IdCarrito == productoCarrito.IdCarrito);

            if (actualproducto == null)
            {
                return NotFound();
            }
            _context.Producto_Carrito.Remove(actualproducto);
            
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete("productocarrito/{idProducto}/{cantidad}")]
        public async Task<ActionResult> EliminarCantProductoCarrito(int idProducto,int cantidad, [FromBody] Producto_Carrito productoCarrito)
        {
            if (_context.Producto_Carrito == null)
            {
                return NotFound();
            }
            var actualproducto = _context.Producto_Carrito.FirstOrDefault(p => p.IdProducto == idProducto && p.IdCarrito == productoCarrito.IdCarrito);

            if (actualproducto == null)
            {
                return NotFound();
            }
            if(actualproducto.CantProducto == cantidad){
                _context.Producto_Carrito.Remove(actualproducto);

                await _context.SaveChangesAsync();
            }
            else{
                actualproducto.CantProducto -= cantidad;
                _context.Entry(actualproducto).Property(x => x.CantProducto).IsModified = true;
                _context.SaveChanges();
            }
            return Ok();
        }
        //http de TABLA CARRITO
        [HttpGet("carrito")]
        public async Task<ActionResult<IEnumerable<Carrito>>> GetCarrito()
        {

            if (_context.Carrito == null)
            {
                return NotFound();
            }
            return await _context.Carrito.ToListAsync();
        }
        [HttpPost("carrito")]
        public ActionResult<Producto_Carrito> CrearCarrito(Carrito carrito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Carrito == null)
            {
                return NotFound();
            }
            if(!_context.Carrito.Any(p => p.IdCarrito == carrito.IdCarrito  && p.IdUsuario == carrito.IdUsuario)){
                _context.Carrito.Add(carrito);
                _context.SaveChanges();
                return Ok(carrito);
            }
            return NotFound();
            
        }
        [HttpPut("carrito/{idcarrito}")]
        public IActionResult ActualizarCarrito(int idcarrito)
        {
            if (_context.Carrito == null)
            {
                return NotFound();
            }
            if (_context.Producto_Carrito == null)
            {
                return NotFound();
            }
            var camposcarrito = _context.Producto_Carrito
                .GroupBy(p => p.IdCarrito)
                .Select(g => new
                {
                    IdCarrito = g.Key,
                    TotalPrecio = g.Sum(p => p.Precio*p.CantProducto),
                    TotalProductos = g.Sum(p => p.CantProducto)
                })
                .FirstOrDefault(p => p.IdCarrito == idcarrito);

            
            var carrito = _context.Carrito.Find(idcarrito);
            if (carrito == null)
            {
                return NotFound();
            }
            if (camposcarrito == null)
            {
                carrito.Totalprecio = 0;
                carrito.Totalproductos = 0;
                _context.SaveChanges();
                return Ok("actualizado");
            }
            carrito.Totalprecio = camposcarrito.TotalPrecio;
            carrito.Totalproductos = camposcarrito.TotalProductos;
            // _context.Entry(actualproducto).Property(x => x.CantProducto).IsModified = true;
            _context.SaveChanges();

            return Ok("actualizado");
        }
        
        [HttpDelete("carrito/{idcarrito}")]
        public async Task<ActionResult> EliminarCarrito(int idcarrito)
        {
            if (_context.Carrito == null)
            {
                return NotFound();
            }
            var actualcarrito = _context.Carrito.FirstOrDefault(p => p.IdCarrito == idcarrito);

            if (actualcarrito == null)
            {
                return NotFound();
            }
            _context.Carrito.Remove(actualcarrito);

            await _context.SaveChangesAsync();

            return Ok();
        }
        

    }

}
