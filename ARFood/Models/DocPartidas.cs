using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace ARFood.Models
{
    public class DocPartidas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public Guid IDDoc { get; set; }
        public int NPartida { get; set; }
        public int IDMesa { get; set; }
        public int IDProd { get; set; }
        public string Descripcion { get; set; }
        public double Cantidad { get; set; }
        public double Surtido { get; set; }
        public int UnidadMedida { get; set; }
        public string Observaciones { get; set; }
        public double Precio { get; set; }
        public double IVA { get; set; }
        public List<ComplementoProductos> ComplementodeProducto { get; set; }

    }
}