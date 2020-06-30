using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ARFood.Models
{
    public class Estados
    {
        [Key]
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Nombre { get; set; }
    }
}