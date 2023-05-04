using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Carrito.Models;
using Proyecto_Carrito.Data;

namespace Proyecto_Carrito.Controllers
{

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

            if (_context.Producto_Carrito == null)
            {
                return NotFound();
            }
            return await _context.Producto_Carrito.ToListAsync();
        }
        [HttpGet("productocarrito/{id}")]
        public async Task<ActionResult<IEnumerable<Producto_Carrito>>> GetProductoCarritoById(int id)
        {
            if (_context.Producto_Carrito == null)
            {
                return NotFound();
            }
            var productoCarrito = await (_context.Producto_Carrito).Where(p => p.IdProducto_Carrito == id).ToListAsync();

            return productoCarrito;
        }

        [HttpGet("productocarrito/producto/{id}")]
        public async Task<ActionResult<IEnumerable<Producto_Carrito>>> GetProductoCarritoByIdProducto(int id)
        {
            if (_context.Producto_Carrito == null)
            {
                return NotFound();
            }
            var productoCarrito = await (_context.Producto_Carrito).Where(p => p.IdProducto == id).ToListAsync();

            return productoCarrito;
        }
        [HttpGet("productocarrito/carrito/{idcarrito}")]
        public async Task<ActionResult<IEnumerable<Producto_Carrito>>> GetProductoCarritoByIdCarrito(int idcarrito)
        {
            if (_context.Producto_Carrito == null)
            {
                return NotFound();
            }
            var productosenCarrito = await (_context.Producto_Carrito).Where(p => p.IdCarrito == idcarrito).ToListAsync();

            return productosenCarrito;
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
            if (!_context.Producto_Carrito.Any(p => p.IdProducto == productoCarrito.IdProducto && p.IdCarrito == productoCarrito.IdCarrito))
            {
                _context.Producto_Carrito.Add(productoCarrito);
                _context.SaveChanges();
                return Ok(productoCarrito);
            }
            else
            {
                var actualproducto = _context.Producto_Carrito.FirstOrDefault(p => p.IdProducto == productoCarrito.IdProducto && p.IdCarrito == productoCarrito.IdCarrito);

                if (actualproducto == null)
                {
                    return NotFound();
                }

                actualproducto.CantProducto += productoCarrito.CantProducto;
                _context.Entry(actualproducto).Property(x => x.CantProducto).IsModified = true;
                _context.SaveChanges();
                return Ok(productoCarrito);
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

            return Ok(actualproducto);
        }

        [HttpDelete("productocarrito/{id}")]
        public async Task<ActionResult> EliminarProductoById(int id)
        {
            if (_context.Producto_Carrito == null)
            {
                return NotFound();
            }
            var actualproducto = _context.Producto_Carrito.FirstOrDefault(p => p.IdProducto_Carrito == id);

            if (actualproducto == null)
            {
                return NotFound();
            }
            _context.Producto_Carrito.Remove(actualproducto);

            await _context.SaveChangesAsync();

            return Ok(actualproducto);
        }
        [HttpDelete("productocarrito/producto/{idProducto}")]
        public async Task<ActionResult> EliminarIdProductoDelCarrito(int idProducto, [FromBody] Producto_Carrito productoCarrito)
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

            return Ok(actualproducto);
        }
        [HttpDelete("productocarrito/{idProducto}/{cantidad}")]
        public async Task<ActionResult> EliminarCantProductoCarrito(int idProducto, int cantidad, [FromBody] Producto_Carrito productoCarrito)
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
            if (actualproducto.CantProducto - cantidad < 0)
            {
                _context.Producto_Carrito.Remove(actualproducto);

                await _context.SaveChangesAsync();
            }
            else
            {
                actualproducto.CantProducto -= cantidad;
                _context.Entry(actualproducto).Property(x => x.CantProducto).IsModified = true;
                _context.SaveChanges();

            }
            return Ok(actualproducto);
        }

        //http de TABLA CARRITO

        [HttpGet("carrito/{idcarrito}")]
        public async Task<ActionResult<IEnumerable<Carrito>>> GetByIdCarrito(int idcarrito)
        {
            if (_context.Carrito == null)
            {
                return NotFound();
            }
            var carrito = await (_context.Carrito).Where(p => p.IdCarrito == idcarrito).ToListAsync();
            return carrito;
        }
        [HttpGet("carrito/usuario/{idusuario}")]
        public async Task<ActionResult<IEnumerable<Carrito>>> GetByIdUsuario(int idusuario)
        {
            if (_context.Carrito == null)
            {
                return NotFound();
            }
            var carrito = await (_context.Carrito).Where(p => p.IdUsuario == idusuario).ToListAsync();
            return carrito;
        }


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
            if (!_context.Carrito.Any(p => p.IdCarrito == carrito.IdCarrito && p.IdUsuario == carrito.IdUsuario))
            {
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
                    TotalPrecio = g.Sum(p => p.Precio * p.CantProducto),
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
                return Ok(carrito);
            }
            carrito.Totalprecio = camposcarrito.TotalPrecio;
            carrito.Totalproductos = camposcarrito.TotalProductos;
            // _context.Entry(actualproducto).Property(x => x.CantProducto).IsModified = true;
            _context.SaveChanges();

            return Ok(carrito);
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

            return Ok(actualcarrito);
        }

        //http de TABLA TRANSACCIONES

        //Get de todas las transaciones
        [HttpGet("transacciones")]
        public async Task<ActionResult<IEnumerable<Transacciones>>> GetTransacciones()
        {

            if (_context.Transacciones == null)
            {
                return NotFound();
            }
            return await _context.Transacciones.ToListAsync();
        }
        //Get de una transaccion por su id, id de la transaccion
        [HttpGet("transacciones/{id}")]
        public async Task<ActionResult<IEnumerable<Transacciones>>> GetTransaccionById(int id)
        {
            if (_context.Transacciones == null)
            {
                return NotFound();
            }
            var transaccion = await (_context.Transacciones).Where(p => p.IdTransaccion == id).ToListAsync();

            return transaccion;
        }
        //Get de todas las transaciones por el id del carrito
        [HttpGet("transacciones/carrito/{idcarrito}")]
        public async Task<ActionResult<IEnumerable<Transacciones>>> GetTransaccionesByIdCarrito(int idcarrito)
        {
            if (_context.Transacciones == null)
            {
                return NotFound();
            }
            var transaccionesDECarrito = await (_context.Transacciones).Where(p => p.IdCarrito == idcarrito).ToListAsync();

            return transaccionesDECarrito;
        }
        //Post una nueva transaccion
        [HttpPost("transacciones")]
        public ActionResult<Transacciones> CrearTransaccion(Transacciones transaccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Transacciones == null)
            {
                return NotFound();
            }
            if (!_context.Transacciones.Any(p => p.IdTransaccion == transaccion.IdTransaccion))
            {
                _context.Transacciones.Add(transaccion);
                _context.SaveChanges();
                return Ok(transaccion);
            }
            return NotFound();
        }
        //Delete transaccion por su id de transaccion
        [HttpDelete("transacciones/{id}")]
        public async Task<ActionResult> EliminarTransaccion(int id)
        {
            if (_context.Transacciones == null)
            {
                return NotFound();
            }
            var transaccion = _context.Transacciones.FirstOrDefault(p => p.IdTransaccion == id);

            if (transaccion == null)
            {
                return NotFound();
            }
            _context.Transacciones.Remove(transaccion);

            await _context.SaveChangesAsync();

            return Ok(transaccion);
        }

    }

}
