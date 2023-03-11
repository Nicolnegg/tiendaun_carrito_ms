using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Proyecto_Carrito.Models;
using Proyecto_Carrito.Context;

namespace Proyecto_Carrito.Controllers;

[Route("api/carrito")]

[ApiController]
public class CarritoController : ControllerBase
{
    // [HttpGet]
    // public string Get()
    // {
    //     return "¡Hola, mundo!";
    // }
    private readonly MyDbContext _context;

    public CarritoController(MyDbContext context)
    {
        _context = context;
        Console.WriteLine("aqui");
    }

    [HttpGet]
    public ActionResult<IEnumerable<Producto_Carrito>> GetUsuarios()
    {
        return _context.ProductosCarritos.ToList();
    }
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
