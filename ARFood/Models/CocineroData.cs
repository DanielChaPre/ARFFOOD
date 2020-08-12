using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARFood.Models
{
    public class CocineroData 
    {
        public IEnumerable<Documentos> GetDocumentos { get; set; }
        public IEnumerable<DocPartidas> GetDocPartidas { get; set; }
        public IEnumerable<DocPartidasPersonalizar> GetDocPartidasPersonalizars { get; set; }
        public IEnumerable<Recetas> GetRecetas { get; set; }
        public IEnumerable<Ingredientes> GetIngredientes { get; set; }
        public IEnumerable<MesasDisponibles> GetMesasDisponibles { get; set; }
        public IEnumerable<Productos> GetProductos { get; set; }

    }

    public class CocinaPartidas
    {
        public IEnumerable<DocPartidas> GetDocPartidas { get; set; }
        public IEnumerable<DocPartidasPersonalizar> GetDocPartidasPersonalizars { get; set; }
        public IEnumerable<Productos> GetProductos { get; set; }
        public IEnumerable<Recetas> GetRecetas { get; set; }
        public IEnumerable<Ingredientes> GetIngredientes { get; set; }
        public IEnumerable<Productos> getIngrProd { get; set; }
    }
}