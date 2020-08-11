using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ARFood.Models
{
    public class Clientes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public Guid IDUser { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(13)]
        public string RFC { get; set; }
        [StringLength(100)]
        public string RazonSocial { get; set; }
        [StringLength(100)]
        public string Direccion { get; set;}
        [StringLength(100)]
        public string Calle { get; set; }
        [StringLength(5)]
        public string CP { get; set; }
        [StringLength(100)]
        public string Cuidad { get; set; }
        [StringLength(100)]
        public string Localidad { get; set; }
        [StringLength(100)]
        public string Estado { get; set; }

        [StringLength(50)]
        public string Tel1 { get; set; }
        [StringLength(50)]
        public string Tel2 { get; set; }
        [StringLength(100)]
        public string Correo { get; set; }
    }
}