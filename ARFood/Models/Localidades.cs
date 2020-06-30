using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ARFood.Models
{
    public class Localidades
    {
        [Key]
        public string Localidad { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }

    }
}