using ARFood.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Razor;
using System.Web.UI.WebControls;
using System.Windows.Markup;

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

        public List<ComplementoProductos> BuscarProductosComplementarios(List<int> Productos)
        {
            var consulta = from datos in contex.complementoProductos
                           where Productos.Contains(datos.idProducto)
                           orderby datos.idProducto, datos.idComplemento
                           select datos;
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
                DocPartidas xdocPartidas = new DocPartidas();
                xdocPartidas.IDDoc = xID;
                xdocPartidas.NPartida = xPartida;
                xdocPartidas.IDProd = xPedido.ID;
                xdocPartidas.Descripcion = xPedido.Descripcion;
                xdocPartidas.Cantidad = xPedido.Cantidad;
                xdocPartidas.UnidadMedida = xPedido.UnidadMedida;
                xdocPartidas.Observaciones = xPedido.Observaciones;
                xdocPartidas.Precio = xPedido.Precio;
                xdocPartidas.IVA = xPedido.IVA;
                contex.DocPartidas.Add(xdocPartidas);
                contex.SaveChanges();
                List<int> idProdPersonalizado = new List<int>();
                idProdPersonalizado.Add(xdocPartidas.IDProd);
                List<ComplementoProductos> DatosProductosComplementarios = BuscarProductosComplementarios(idProdPersonalizado);
                if (xPedido.ComplementodeProducto != null)
                {
                    foreach (ComplementoProductos xComplemento in xPedido.ComplementodeProducto)
                    {
                        if (xComplemento.Seleccionado != DatosProductosComplementarios.Find(x => x.idComplemento == xComplemento.idComplemento).Seleccionado)
                        {
                            DocPartidasPersonalizar docPPersonalizar = new DocPartidasPersonalizar();
                            docPPersonalizar.IDDoc = xID;
                            docPPersonalizar.NPartida = xPartida;
                            docPPersonalizar.IDProdPersonalizado = xPedido.ID;
                            docPPersonalizar.IDProdAgregado = xComplemento.idComplemento;
                            docPPersonalizar.Descripcion = xComplemento.Descripcion;
                            docPPersonalizar.Cantidad = xComplemento.cantidad * (xComplemento.Seleccionado ? 1 : -1);
                            docPPersonalizar.UnidadMedida = xComplemento.UnidadMedida;
                            docPPersonalizar.Precio = xComplemento.Precio;
                            docPPersonalizar.IVA = xComplemento.Precio * 0.16;
                            contex.docPartidasPersonalizars.Add(docPPersonalizar);
                            contex.SaveChanges();
                        }
                    }
                }
                xPartida++;
            }
            return "GUID:" + xID.ToString() ;
        }

        public List<Documentos> getDocumentos(List<Guid> xID)
        {
//                Guid xID = Guid.Parse(IDDoc);
            var consulta = from datos in contex.Documentos
                            where xID.Contains( datos.ID) 
                            select datos;
            return consulta.ToList();
        }

        public List<DocPartidas> getDocumentosPartidas(List<Guid> xID)
        {
            var consulta = from datos in contex.DocPartidas
                            where xID.Contains( datos.IDDoc) 
                            select datos;
            return consulta.ToList();
        }

        public List<DocPartidasPersonalizar> getDocumentosPartidasPersonalizar(List<Guid> xID)
        {
            var consulta = from datos in contex.docPartidasPersonalizars
                            where xID.Contains( datos.IDDoc)
                            select datos;
            return consulta.ToList();
        }

        public List<Salon> getSalon()
        {
            var consulta = from datos in contex.salons
                           select datos;
            return consulta.ToList();
        }

        public string SaveSalons(List<string> SalonDesign)
        {
            string xResult = "Error al guardar";
            for (int i = 0; i < SalonDesign.Count; i++)
            {
                string[] words = SalonDesign[i].Split(',');
                Salon salon = new Salon();
                salon.Nombre = words[0].ToString();
                salon.X = Convert.ToInt32(words[1].ToString());
                salon.Y = Convert.ToInt32(words[2].ToString());
                salon.width = Convert.ToInt32(words[3].ToString());
                salon.height = Convert.ToInt32(words[4].ToString());
                contex.salons.Add(salon);
                contex.SaveChanges();
            }
            return xResult;
        }

        public List<MesasDisponibles> BuscarMesasDisponibles(string xFecha)
        {
            //DateTime xDate = Convert.ToDateTime(string.Format("yyyy-MM-dd hh:mm tt",xFecha));
            DateTime xDate;
            if (xFecha.Contains("undef"))
            {
                xDate = DateTime.Now;
            }
            else
            {
                xFecha = xFecha.Replace('.', '-');
                xDate = DateTime.ParseExact(xFecha, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            }
            var consulta = from datos in contex.mesasdisponibles
                           where datos.FechaInicio <= xDate && datos.FechaFin >= xDate
                           select datos;
            return consulta.ToList();
        }
    }
}