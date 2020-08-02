using ARFood.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public string GuardaPedido(List<ProductosPedidos> productos, int IDCliente,  int IDUser, double subTotal, string hasDate, string IDDocumento, string IDMesa)
        {
            string SeGuardo = "Error al Guardar, favor de intentarlo de nuevo";
            bool NewElement = true;
            DocPartidas xdocPartidas = null;
            DocPartidasPersonalizar docPPersonalizar = null;

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
            Guid xID = Guid.NewGuid();
            Documentos documentos = new Documentos();
            if (IDDocumento != null)
            {
                if (IDDocumento.Length > 0)
                {
                    xID = Guid.Parse(IDDocumento);
                    documentos = contex.Documentos.Where(x => x.ID == xID).FirstOrDefault();
                }
            }
            documentos.ID = xID;
            documentos.IDCliente = IDCliente;
            documentos.IDTipo = 1;
            documentos.IDMesa = IDMesa != null? Convert.ToInt32(IDMesa) : 0;
            documentos.Observaciones = "";
            documentos.Fecha = fecha;
            documentos.FechaEntrega = useDate;
            documentos.Total = subTotal * 1.16;
            documentos.IVA = subTotal * 0.16;
            documentos.IDUsuario = IDUser;
            documentos.Pago = 0;
            if (IDDocumento == null)
            {
                contex.Documentos.Add(documentos);
            }
            contex.SaveChanges();
            int xPartida = 1;
            foreach (ProductosPedidos xPedido in productos)
            {
                NewElement = true;
                if (IDDocumento != null)
                {
                    if (IDDocumento.Length > 0)
                    {
                        xdocPartidas = contex.DocPartidas.Where(x => x.IDDoc == xID && x.IDProd == xPedido.ID).FirstOrDefault();
                        NewElement = false;
                    }
                }
                if (xdocPartidas == null)
                {
                    xdocPartidas = new DocPartidas();
                    NewElement = true;
                    xdocPartidas.IDDoc = xID;
                    xdocPartidas.NPartida = xPartida;
                    xdocPartidas.IDProd = xPedido.ID;
                    xdocPartidas.IDMesa = IDMesa != null ? Convert.ToInt32(IDMesa) : 0;
                    xdocPartidas.Descripcion = xPedido.Descripcion;
                    xdocPartidas.Cantidad = xPedido.Cantidad;
                    xdocPartidas.Surtido = xPedido.Surtido > 0 ? xPedido.Surtido : 0;
                    xdocPartidas.UnidadMedida = xPedido.UnidadMedida;
                    xdocPartidas.Observaciones = xPedido.Observaciones != null ? xPedido.Observaciones : "" ;
                    xdocPartidas.Precio = xPedido.Precio;
                    xdocPartidas.IVA = xPedido.IVA;
                }
                if (IDDocumento == null || NewElement)
                {
                    contex.DocPartidas.Add(xdocPartidas);
                }
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
                            if (IDDocumento != null)
                            {
                                if (IDDocumento.Length > 0)
                                {
                                    docPPersonalizar = contex.docPartidasPersonalizars.Where(x => x.IDDoc == xID && x.IDProdPersonalizado == xPedido.ID && x.IDProdAgregado == xPedido.ComplementodeProducto.Find(y => y.idProducto == xPedido.ID && y.idComplemento == xComplemento.idComplemento).idComplemento).FirstOrDefault();
                                    if (docPPersonalizar == null)
                                    {
                                        docPPersonalizar = new DocPartidasPersonalizar();
                                    }
                                }
                            }
                            else
                            {
                                docPPersonalizar = new DocPartidasPersonalizar();
                            }
                            docPPersonalizar.IDDoc = xID;
                            docPPersonalizar.NPartida = xPartida;
                            docPPersonalizar.IDProdPersonalizado = xPedido.ID;
                            docPPersonalizar.IDProdAgregado = xComplemento.idComplemento;
                            docPPersonalizar.Descripcion = xComplemento.Descripcion;
                            docPPersonalizar.Cantidad = xComplemento.cantidad * (xComplemento.Seleccionado ? 1 : -1);
                            docPPersonalizar.UnidadMedida = xComplemento.UnidadMedida;
                            docPPersonalizar.Precio = xComplemento.Precio;
                            docPPersonalizar.IVA = xComplemento.Precio * 0.16;
                            if (IDDocumento == null)
                            {
                                contex.docPartidasPersonalizars.Add(docPPersonalizar);
                            }
                            contex.SaveChanges();
                        }
                    }
                }
                xPartida++;
            }
            return "GUID:" + xID.ToString() ;
        }

        public List< string> GuardaNewBlankPedido(int IDCliente, int IDUser, string IDMesa)
        {
            string SeGuardo = "Error al Guardar, favor de intentarlo de nuevo";
            
            DateTime fecha = DateTime.Now;
            DateTime useDate = fecha; ;

            MesasDisponibles NewMesa = new MesasDisponibles();
            NewMesa.IDMesa = Convert.ToInt32( IDMesa);
            NewMesa.FechaInicio = fecha;
            NewMesa.FechaFin = fecha;
            contex.mesasdisponibles.Add(NewMesa);
            contex.SaveChanges();


            Guid xID = Guid.NewGuid();
            Documentos documentos = new Documentos();
            documentos.ID = xID;
            documentos.IDCliente = IDCliente;
            documentos.IDTipo = 1;
            documentos.IDMesa = NewMesa.ID;
            documentos.Observaciones = "";
            documentos.Fecha = fecha;
            documentos.FechaEntrega = useDate;
            documentos.Total = 0;
            documentos.IVA = 0;
            documentos.IDUsuario = IDUser;
            documentos.Pago = 0;
            documentos.Estatus = "A";
            contex.Documentos.Add(documentos);
            contex.SaveChanges();
            List<string> xReturn = new List<string>();    
            xReturn.Add( xID.ToString());
            xReturn.Add( NewMesa.ID.ToString());
            return xReturn;
        }

        public void GuardaTransferPedido(string idPed, string IDMesa)
        {
            DateTime fecha = DateTime.Now;
            DateTime useDate = fecha; ;

            MesasDisponibles NewMesa = new MesasDisponibles();
            NewMesa.IDMesa = Convert.ToInt32(IDMesa);
            NewMesa.FechaInicio = fecha;
            NewMesa.FechaFin = fecha;
            contex.mesasdisponibles.Add(NewMesa);
            contex.SaveChanges();

            Guid xID = Guid.Parse(idPed);

            var consulta = from datos in contex.Documentos
                           where datos.ID == xID
                           select datos;


            Documentos documentos = consulta.FirstOrDefault();
            documentos.IDMesa = NewMesa.ID;
            contex.SaveChanges();

            var consultaPartida = from datos in contex.DocPartidas
                                  where datos.IDDoc == xID
                                  select datos;

            List<DocPartidas> docPartidas = consultaPartida.ToList();
            foreach (var xPartida in docPartidas)
            {
                xPartida.IDMesa = NewMesa.ID;
                contex.SaveChanges();
            }
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

        public List<Documentos> GetOrdenesxMesa(int Mesa)
        {
            var consulta = from datos in contex.Documentos
                           join Mesasdi in contex.mesasdisponibles on datos.IDMesa equals Mesasdi.ID
                           where Mesasdi.IDMesa == Mesa && datos.Estatus == "A" && (datos.Pago < (datos.Total + datos.IVA) || datos.Total ==0)
                           orderby datos.Fecha
                           select datos;
            return consulta.ToList();
        }

        public void SaveAgregaOrdenAMesa(int Mesa)
        {
            MesasDisponibles newMesa = new MesasDisponibles();
            DateTime xDate = DateTime.Now;
            newMesa.IDMesa = Mesa;
            newMesa.FechaInicio = xDate;
            newMesa.FechaFin = xDate;
            contex.mesasdisponibles.Add(newMesa);
            contex.SaveChanges();
            Documentos newDoc = new Documentos();
            newDoc.ID = Guid.NewGuid();
            newDoc.IDTipo = 1;
            newDoc.IDCliente = 0;
            newDoc.IDMesa = newMesa.ID;
            newDoc.Nombre = Mesa.ToString() + "-" + xDate.ToString("HHMM");
            newDoc.Observaciones = "";
            newDoc.Total = 0;
            newDoc.IVA = 0;
            newDoc.IDUsuario = 0;
            newDoc.Fecha = xDate;
            newDoc.FechaEntrega = xDate;
            newDoc.Pago = 0;
            newDoc.Estatus = "A";
            contex.Documentos.Add(newDoc);
            contex.SaveChanges();
        }

        public void SaveNombreMesa(string IDDoc, string NombreMesa )
        {
            Guid xID = Guid.Parse(IDDoc);
            var consulta = contex.Documentos.Where(x => x.ID == xID).FirstOrDefault();
            consulta.Nombre = NombreMesa;
            contex.SaveChanges();
        }

        public void SavePagarOrden(List<Guid> xOrdenes)
        {
            var consulta = from datos in contex.Documentos
                           where xOrdenes.Contains (datos.ID)
                           select datos;
            foreach(var Item in consulta)
            {
                Item.Estatus = "P";
                Item.Pago = Item.Total;
                contex.SaveChanges();
            }
        }
    }
}