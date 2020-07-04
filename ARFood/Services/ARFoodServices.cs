using ARFood.Models;
using Microsoft.Ajax.Utilities;
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
            if (Familia >0 )
            {
                var consulta = from datos in contex.Productos
                               where Familia.Equals(datos.Familia)
                               select datos;
                return consulta.ToList();
            }
            return null;
        }

        public List<Productos> BuscarProductos(List<int> Productos)
        {
            var consulta = from datos in contex.Productos
                           where Productos.Contains(datos.ID)
                           select datos;
                           //{
                           //    ID = datos.ID,
                           //    Producto = datos.Producto,
                           //    Descripcion = datos.Descripcion,
                           //    Cantidad = datos.Cantidad,
                           //    Precio = datos.Precio,
                           //    IVA = datos.IVA
                           //};
            return consulta.ToList();

        }
    }
}