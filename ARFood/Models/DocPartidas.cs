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
        public int IDDoc { get; set; }
        public int NPartida { get; set; }
        public int IDProd { get; set; }
        public double Cantidad { get; set; }
        public int UnidadMedida { get; set; }
        public string Observaciones { get; set; }
        public double Precio { get; set; }
        public double IVA { get; set; }

    }
}