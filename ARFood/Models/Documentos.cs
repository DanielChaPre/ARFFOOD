using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ARFood.Models
{
    public class Documentos
    {
        [Key]
        public Guid ID { get; set; }
        public int IDTipo { get; set; }
        public Guid IDCliente { get; set; }
        public int IDMesa { get; set; }
        public string Nombre { get; set; }
        public string Observaciones { get; set; }
        public double Total { get; set; }
        public double IVA { get; set; }
        public int IDUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaEntrega { get; set; }
        public double Pago { get; set; }
        public string Estatus { get; set; }
    }
}