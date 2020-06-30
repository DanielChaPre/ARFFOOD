using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ARFood.Models
{
    public class LotesInv
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int IDAlm { get; set; }
        public int IDProd { get; set; }
        public double Entradas { get; set; }
        public double Salidas { get; set; }
        public double Cantidad { get; set; }

    }
}