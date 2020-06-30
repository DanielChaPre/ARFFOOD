using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ARFood.Models
{
    public class Lotes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string LOTE { get; set; }
        public int IDProv { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime Caducidad { get; set; }

    }
}