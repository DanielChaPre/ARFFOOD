using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ARFood.Models
{
    public class Colonias
    {
        [Key]
        public string IDCol { get; set; }
        public string CP { get; set; }
        public string Nombre { get; set; }
    }
}