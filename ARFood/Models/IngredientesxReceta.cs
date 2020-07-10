using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ARFood.Models
{
    public class IngredientesxReceta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int IDReceta { get; set; }
        public int Paso { get; set;}
        public int IDIngrediente { get; set; }
        public double Cantidad { get; set; }
        public int UnidadMedida { get; set; }
        public Boolean AddorRemove { get; set; }
        public int Precio { get; set; }

    }
}