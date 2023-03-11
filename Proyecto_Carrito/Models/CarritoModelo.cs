namespace Proyecto_Carrito;

public class Carrito
{
    public long IdCarrito { get; set; }

    public long IdUsuario { get; set; }

    public int Totalprecio { get; set; }

    public int Totalproductos { get; set; }

}

public class Producto_Carrito
{
    public long IdProducto { get; set; }

    public long IdCarrito { get; set; }

    public int Precio { get; set; }

    public int CantProducto { get; set; }

}

