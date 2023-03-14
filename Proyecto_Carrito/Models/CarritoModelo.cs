using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Proyecto_Carrito.Models{
    public class Carrito
    {
        [Key]
        public int IdCarrito { get; set; }

        public int IdUsuario { get; set; }

        public int Totalprecio { get; set; }

        public int Totalproductos { get; set; }

    }

    public class Producto_Carrito
    {
        
        public int IdProducto { get; set; }
        
        public int IdCarrito { get; set; }

        public int Precio { get; set; }

        public int CantProducto { get; set; }


    }
}


