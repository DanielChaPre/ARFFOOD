using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ARFood.Models
{
    public class MINV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int IDDoc { get; set; }
        public int IDProd { get; set; }
        public int IDAlm { get; set; }
        public int IDLote { get; set; }
        public double Cantidad { get; set; }
        public int EntSal { get; set; }
        public DateTime Fecha { get; set; }
    }
}