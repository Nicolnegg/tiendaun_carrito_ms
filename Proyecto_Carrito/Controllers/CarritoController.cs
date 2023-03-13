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
        public async Task<ActionResult<IEnumerable<Carrito>>> Get()
        {

            if (_context.Carrito== null)
            {
                return NotFound();
            }
            return await _context.Carrito.ToListAsync();
                        
        }
        // [HttpPost]
        // public ActionResult<Producto_Carrito> AgregarProductoCarrito(Producto_Carrito productoCarrito)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     _context.ProductosCarritos.Add(productoCarrito);
        //     _context.SaveChanges();

        //     return CreatedAtAction(nameof(Get), new { IdProducto = productoCarrito.IdProducto }, productoCarrito);
        // }
        // private readonly List<Producto_Carrito> _productosCarrito;

        // public CarritoController()
        // {
        //     _productosCarrito = new List<Producto_Carrito>();
        //     Console.WriteLine(_productosCarrito);
        // }        
        // [HttpGet]
        //     public IActionResult ObtenerProductos()
        //     {
        //         Console.WriteLine("Consulta");
        //         return Ok(_productosCarrito);
        //     }

    }

    // public class CarritoController : ControllerBase
    // {

    //     private readonly List<Producto_Carrito> _productosCarrito;

    //     public CarritoController()
    //     {
    //         _productosCarrito = new List<Producto_Carrito>();
    //         Console.WriteLine(_productosCarrito);
    //     }


    //     [HttpPost]
    //     public IActionResult AgregarProducto([FromBody] Producto_Carrito producto)
    //     {
    //         Console.WriteLine("AQUI");    
    //         _productosCarrito.Add(producto);
    //         Console.WriteLine(_productosCarrito);
    //         return Ok();
    //     }

    //     [HttpDelete("{idProducto}")]
    //     public IActionResult EliminarProducto(long idProducto)
    //     {
    //         Console.WriteLine("elimina");
    //         var productoAEliminar = _productosCarrito.FirstOrDefault(p => p.IdProducto == idProducto);
    //         if (productoAEliminar != null)
    //         {
    //             _productosCarrito.Remove(productoAEliminar);
    //             return Ok();
    //         }
    //         else
    //         {
    //             return NotFound();
    //         }
    //     }
    //     [HttpGet]
    //     public IActionResult ObtenerProductos()
    //     {
    //         Console.WriteLine("Consulta");
    //         return Ok(_productosCarrito);
    //     }

    // }

}
