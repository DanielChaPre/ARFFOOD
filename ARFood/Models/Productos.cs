using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ARFood.Models
{
    public class Productos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        //Campo temporal para ejecutar el menu, no guarda información
        public int Cantidad { get; set; }
        public int UnidadMedida { get; set; }
        public int UnidadAlterna { get; set; }
        public double FactorConversion { get; set; }
        public int Familia { get; set; }
        public int Sugerencia { get; set; }
        public int Tipo { get; set; }
        public bool Venta { get; set; }
        public double Precio { get; set; }
        public double IVA { get; set; }
        public DateTime Fecha { get; set; }
        public string Imagen { get; set; }
        public string Video { get; set; }

    }

    public class ProductosPedidos
    {
        public int ID { get; set; }
        public int IDUsuario { get; set; }
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public int Surtido { get; set; }
        public int UnidadMedida { get; set; }
        public double Precio { get; set; }
        public double SubTotal { get; set; }
        public double IVA { get; set; }
        [MaxLength(100)]
        public string Observaciones { get; set; }
        public List<ComplementoProductos> ComplementodeProducto { get; set; }
    }

    public class ProductosPersonalizados
    {
        public int ID { get; set; }
        public int IDProd { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
    }


    public class ComplementoProductos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int idProducto { get; set; }
        public int idComplemento { get; set; }
        public string Descripcion { get; set; }
        public int cantidad { get; set; }
        public int UnidadMedida { get; set; }
        public double Precio { get; set; }
        public Boolean EsIngrediente { get; set; }
        public Boolean Seleccionado { get; set; }
    }
}