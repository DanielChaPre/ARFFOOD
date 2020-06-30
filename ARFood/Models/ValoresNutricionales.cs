using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ARFood.Models
{
    public class ValoresNutricionales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int IDProd { get; set; }
        public int Calorias { get; set; }
        public int KCALP { get; set; }
        public int Carbohidratos { get; set; }
        public int CarbohidratosP { get; set; }
        public int Sodio { get; set; }
        public int SodioP { get; set; }
        public int Proteinas { get; set; }
        public int ProteinasP { get; set; }
        public int Lipidos { get; set; }
        public int LipidosP { get; set; }
    }
}