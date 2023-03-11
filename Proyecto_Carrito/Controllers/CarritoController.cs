using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Carrito.Controllers;

[ApiController]
[Route("api/carrito")]
public class CarritoController : ControllerBase
{
    private readonly List<Producto_Carrito> _productosCarrito;

    public CarritoController()
    {
        _productosCarrito = new List<Producto_Carrito>();
    }

    [HttpPost]
    public IActionResult AgregarProducto([FromBody] Producto_Carrito producto)
    {
        Console.WriteLine("AQUI");    
        _productosCarrito.Add(producto);
        return Ok();
    }

    [HttpDelete("{idProducto}")]
    public IActionResult EliminarProducto(long idProducto)
    {
        Console.WriteLine("elimina");
        var productoAEliminar = _productosCarrito.FirstOrDefault(p => p.IdProducto == idProducto);
        if (productoAEliminar != null)
        {
            _productosCarrito.Remove(productoAEliminar);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
    [HttpGet]
    public IActionResult ObtenerProductos()
    {
        Console.WriteLine("Consulta");
        return Ok(_productosCarrito);
    }

}


