using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ARFood.Models
{
    public class Recetas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int IDProducto { get; set; }
        public int Paso { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        public string Video { get; set; }
        public int Inicio { get; set; }
        public int Duracion { get; set; }
        public int Tiempo { get; set; }
    }
}