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
}