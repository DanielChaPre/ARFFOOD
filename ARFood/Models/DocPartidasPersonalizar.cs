using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace ARFood.Models
{
    public class DocPartidasPersonalizar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public Guid IDDoc { get; set; }
        public int NPartida { get; set; }
        public int IDProdPersonalizado { get; set; }
        public int IDProdAgregado { get; set; }
        public string Descripcion { get; set; }
        public double Cantidad { get; set; }
        public int UnidadMedida { get; set; }
        public double Precio { get; set; }
        public double IVA { get; set; }
    }
}