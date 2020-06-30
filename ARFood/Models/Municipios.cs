using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ARFood.Models
{
    public class Municipios
    {
        [Key]
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
    }
}