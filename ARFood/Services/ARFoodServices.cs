using ARFood.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Razor;
using System.Web.UI.WebControls;

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

        public string GuardaPedido(List<ProductosPedidos> productos, int IDCliente,  int IDUser, double subTotal, string hasDate)
        {
            string SeGuardo = "Error al Guardar, favor de intentarlo de nuevo";

            Guid xID = Guid.NewGuid();
            DateTime fecha = DateTime.Now;
            DateTime useDate = fecha; ;
            if (hasDate != null)
            {
                if (hasDate.Length > 0)
                {
                    useDate = Convert.ToDateTime(hasDate);
                    if (DateTime.Compare(DateTime.Now.AddMinutes(15), useDate) > 0)
                    {
                        return "Sólo se puede programar con al menos 15 minutos de diferencia";
                    }
                }
            }
            Documentos documentos = new Documentos();
            documentos.ID = xID;
            documentos.IDCliente = IDCliente;
            documentos.IDTipo = 1;
            documentos.Observaciones = "";
            documentos.Fecha = fecha;
            documentos.FechaEntrega = useDate;
            documentos.Total = subTotal * 1.16;
            documentos.IVA = subTotal * 0.16;
            documentos.IDUsuario = IDUser;
            documentos.Pago = 0;
            contex.Documentos.Add(documentos);
            contex.SaveChanges();
            int xPartida = 1;
            foreach (ProductosPedidos xPedido in productos)
            {
                DocPartidas docPartidas = new DocPartidas();
                docPartidas.IDDoc = xID;
                docPartidas.NPartida = xPartida;
                docPartidas.IDProd = xPedido.ID;
                docPartidas.Cantidad = xPedido.Cantidad;
                docPartidas.UnidadMedida = xPedido.UnidadMedida;
                docPartidas.Observaciones = xPedido.Observaciones;
                docPartidas.Precio = xPedido.Precio;
                docPartidas.IVA = xPedido.IVA;
                xPartida++;
                contex.DocPartidas.Add(docPartidas);
                contex.SaveChanges();
            }
            return "GUID:" + xID.ToString() ;
        }
    }
}