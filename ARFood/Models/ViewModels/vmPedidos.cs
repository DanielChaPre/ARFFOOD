using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARFood.Models.ViewModels
{
    public class vmPedidos
    {
        public IEnumerable<Documentos> GetDocumentos { get; set; }
        public IEnumerable<DocPartidas> GetDocPartidas { get; set; }
        public IEnumerable<DocPartidasPersonalizar> GetDocPartidasPersonalizars { get; set; }
    }
}