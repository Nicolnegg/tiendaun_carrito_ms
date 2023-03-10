namespace Proyecto_Carrito;

public class Carrito
{
    public long Id_Carrito { get; set; }

    public long Id_Usuario { get; set; }

    public int Total_precio { get; set; }

    public int Total_productos { get; set; }

}

public class Producto_Carrito
{
    public long Id_Producto { get; set; }

    public long Id_Carrito { get; set; }

    public int SubTotal_precio { get; set; }

    public int Cant_producto { get; set; }

}
