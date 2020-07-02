using ARFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARFood.Services
{
    public class ARFoodServices
    {
        ApplicationDbContext contex;
        public ARFoodServices()
        {
            this.contex = new ApplicationDbContext();
        }
        public List<Familias> GetFamilias()
        {
            return this.contex.Familias.ToList();
        }
        public List<Productos> GetProductos(int Familia)
        {
            var consulta = from datos in contex.Productos
                           where Familia.Equals(datos.Familia)
                           select datos;
            return consulta.ToList();
        }

        public List<Productos> BuscarProductos(List<int> Productos)
        {
            var consulta = from datos in contex.Productos
                           where Productos.Contains(datos.ID)
                           select datos;
            return consulta.ToList();

        }
    }
}