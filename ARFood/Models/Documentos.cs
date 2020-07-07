﻿using System;
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int IDTipo { get; set; }
        public int IDCliente { get; set; }
        public string Observaciones { get; set; }
        public double Total { get; set; }
        public double IVA { get; set; }
        public int IDUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaEntrega { get; set; }
        public double Pago { get; set; }

    }
}