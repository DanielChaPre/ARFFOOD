using ARFood.Models;
using ARFood.Services;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer;
using System.Web.UI.WebControls;
using System.Web.Services;
using QRCoder;
using System.Drawing;
using System.IO;
using ARFood.Models.ViewModels;

namespace ARFood.Controllers
{
    public class HomeController : Controller
    {
        ARFoodServices ARService;

        public HomeController()
        {
            this.ARService = new ARFoodServices();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MostrarFamilias()
        {
            var FamiliasMenu = ARService.GetFamilias();
            return PartialView("_MostrarFamilias", FamiliasMenu);
        }

        public ActionResult MostrarDetalleFamilia(int? ID)
        {
            if (ID > 0)
            {
                var ProductosMenu = ARService.GetProductos(ID.Value);
                return PartialView("_MostrarDetalleFamilia", ProductosMenu);
            }
            else
            {
                return PartialView("_MostrarDetalleFamilia");
            }
        }


        public ActionResult MostrarPlatillos(int? id)
        {
            if (id != null)
            {
                List<ProductosPedidos> ListadoPlatillos;
                ProductosPedidos NewProducto = new ProductosPedidos();
                double SubTotal = 0;
                if (Session["ListadoPlatillos"] == null)
                {
                    ListadoPlatillos = new List<ProductosPedidos>();
                }
                else
                {
                    ListadoPlatillos = Session["ListadoPlatillos"] as List<ProductosPedidos>;
                }
                if (id > 0)
                {
                    if (ListadoPlatillos.Exists(x => x.ID == id.Value))
                    {
                        ListadoPlatillos.Find(x => x.ID == id.Value).Cantidad += 1;
                    }
                    else
                    {
                        NewProducto.ID = id.Value;
                        NewProducto.Cantidad = 1;
                        ListadoPlatillos.Add(NewProducto);
                    }
                    Session["ListadoPlatillos"] = ListadoPlatillos;
                }
                else
                {
                    ListadoPlatillos.Find(x => x.ID == (id.Value * -1)).Cantidad += -1;
                    if (ListadoPlatillos.Find(x => x.ID == (id.Value * -1)).Cantidad < 1)
                    {
                        ListadoPlatillos.Remove(ListadoPlatillos.Find(x => x.ID == (id.Value * -1)));
                    }
                    if (ListadoPlatillos == null || ListadoPlatillos.Count == 0)
                    {
                        Session["ListadoPlatillos"] = null;
                    }
                    else
                    {
                        Session["ListadoPlatillos"] = ListadoPlatillos;
                    }
                }
                if (Session["ListadoPlatillos"] != null)
                {
                    int i = 0;
                    List<int> AddProd = new List<int>();
                    foreach(var item in ListadoPlatillos)
                    {
                        AddProd.Add(item.ID);
                    }
                    List<Productos> productos = this.ARService.BuscarProductos(AddProd);
                    for (i = 0; i< productos.Count(); i++)
                    {
                        ListadoPlatillos.Find(x=> x.ID == productos[i].ID).Producto = productos[i].Producto;
                        ListadoPlatillos.Find(x => x.ID == productos[i].ID).Descripcion = productos[i].Descripcion;
                        ListadoPlatillos.Find(x => x.ID == productos[i].ID).Precio = productos[i].Precio;
                        ListadoPlatillos.Find(x => x.ID == productos[i].ID).UnidadMedida = productos[i].UnidadMedida;
                        SubTotal += ListadoPlatillos.Find(x => x.ID == productos[i].ID).Precio * ListadoPlatillos.Find(x => x.ID == productos[i].ID).Cantidad;
                    }
                    List<ComplementoProductos> complementos = this.ARService.BuscarProductosComplementarios(AddProd);
                    if (complementos != null)
                    {
                        if (complementos.Count() > 0)
                        {
                            for (i = 0; i < AddProd.Count(); i++)
                            {
                                if (ListadoPlatillos.Find(x => x.ID == AddProd[i]).ComplementodeProducto == null)
                                {
                                    IEnumerable<ComplementoProductos> xComplementos = complementos.Where(x => x.idProducto == AddProd[i]);
                                    List<ComplementoProductos> xComplemento = new List<ComplementoProductos>();
                                    for (int e = 0; e<xComplementos.Count();e++)
                                    {
                                        xComplemento.Add(xComplementos.ElementAt(e) as ComplementoProductos);
                                    }
                                    ListadoPlatillos.Find(x => x.ID == AddProd[i]).ComplementodeProducto = xComplemento;
                                }
                            }
                        }
                    }
                    Session["SubTotal"] = SubTotal.ToString("###,##0.00");
                    return PartialView("_MostrarPlatillos", ListadoPlatillos);
                }
                //return View(this.ARService.GetProductos(id.Value));
            }
            ViewBag.ListadoPlatillos = Session["ListadoPlatillos"];
            return PartialView("_MostrarPlatillos");
        }


        [WebMethod]
        public JsonResult PersonalizarPlatillos(string RidProdCustom, string Comentarios, string Accion, List< string> chkValues)
        {
            int id;
            double SubTotal = 0;
            if (RidProdCustom != null)
            {
                id = Convert.ToInt32(RidProdCustom);
                List<ProductosPedidos> ListadoPlatillos;
                ProductosPedidos NewProducto = new ProductosPedidos();
                if (Session["ListadoPlatillos"] == null)
                {
                    ListadoPlatillos = new List<ProductosPedidos>();
                }
                else
                {
                    ListadoPlatillos = Session["ListadoPlatillos"] as List<ProductosPedidos>;
                }
                if (id > 0)
                {
                    if (Accion.Equals("Agregar"))
                    {
                        ListadoPlatillos.Find(x => x.ID == id).Observaciones = Comentarios;
                    }
                    else
                    {
                        ListadoPlatillos.Find(x => x.ID == id).Observaciones = "";
                    }
                    if (chkValues.Count > 0)
                    {
                        for(int i = 1; i<chkValues.Count; i++)
                        {
                            ListadoPlatillos.Find(x => x.ID == id).ComplementodeProducto[i-1].Seleccionado = Convert.ToBoolean(Convert.ToInt32( chkValues[i]));
                        }
                    }
                    Session["ListadoPlatillos"] = ListadoPlatillos;
                }
                if (Session["ListadoPlatillos"] != null)
                {
                    for (int i = 0; i < ListadoPlatillos.Count(); i++)
                    {
                        SubTotal += ListadoPlatillos[i].Precio * ListadoPlatillos[i].Cantidad;
                    }
                    Session["SubTotal"] = SubTotal.ToString("###,##0.00");
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }



        [WebMethod]
        public JsonResult CargaPersonalizar(int idProd)
        {
            string[] xtemp = {"-25","","","", ""};
            string[][] xResult = { xtemp};
            if (Session["ListadoPlatillos"] != null)
            {
                List<ProductosPedidos> ListadoPlatillos = Session["ListadoPlatillos"] as List<ProductosPedidos>;
                for(int i =0;i<ListadoPlatillos.Count();i++)
                {
                    if (ListadoPlatillos[i].ID == idProd)
                    {
                        if (ListadoPlatillos[i].ComplementodeProducto != null)
                        {
                            if (ListadoPlatillos[i].ComplementodeProducto.Count() > 0)
                            {
                                Array.Clear(xResult, xResult.GetUpperBound(0), 1);
                                for (int e = 0; e < ListadoPlatillos[i].ComplementodeProducto.Count(); e++)
                                {
                                    string[] xtemp1 = { ListadoPlatillos[i].ComplementodeProducto[e].idProducto.ToString(), ListadoPlatillos[i].ComplementodeProducto[e].idComplemento.ToString(), ListadoPlatillos[i].ComplementodeProducto[e].Descripcion.ToString(), ListadoPlatillos[i].ComplementodeProducto[e].Seleccionado.ToString(), "$ " + ListadoPlatillos[i].ComplementodeProducto[e].Precio.ToString("###,##0.00") + " MXN" };
                                    Array.Resize(ref xResult, xResult.Length + 1);
                                    xResult[xResult.GetUpperBound(0)] = xtemp1;
                                }
                            }
                        }
                        break;
                    }
                }

                //string[] xtemp1 = { "1", "1", "Uno", "1" };

                return Json(xResult, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
            //ViewBag.Personalizar = productosPersonalizados;

        }

        public ActionResult Personalizar (int? id)
        {
            return View();
        }

        [WebMethod]
        public JsonResult GuardarPedido(string HasDate)
        {
            if (Session["ListadoPlatillos"] == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            List<ProductosPedidos> ListadoPlatillos;
            ProductosPedidos NewProducto = new ProductosPedidos();
            int id;
            if (User.Identity.GetUserId() != null)
            {
                id = Convert.ToInt32(User.Identity.GetUserId().ToString());
            }
            else
            {
                id = 1;
            }
            ListadoPlatillos = Session["ListadoPlatillos"] as List<ProductosPedidos>;
            string xResult = ARService.GuardaPedido(ListadoPlatillos, id, Convert.ToInt32( Session["IDEmpleado"] ),  Convert.ToDouble(Session["SubTotal"]), HasDate);            
            if (xResult.Contains("GUID:"))
            {
                Guid xID = Guid.Parse(xResult.Substring(5));
                Session["Order-GUID"] = xID;
                Session["ListadoPlatillos"] = null;
            }
            return Json(xResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrdenCreada(string txtQRCode)
        {
            if (Session["Order-GUID"] == null)
            {
                txtQRCode = "8B0826BA-1E46-409E-AE3C-E7F72BADAD0D";
                Session["Order-GUID"] = Guid.Parse(txtQRCode);
            }
            vmPedidos xpedidos = new vmPedidos();
            if (Session["Order-GUID"] != null || txtQRCode.Contains("NewOrderGuid:"))
            {
                List<Guid> xID = new List<Guid>();
                xID.Add(Guid.Parse(Session["Order-GUID"].ToString()));
                List<Documentos> ListadoDocumentos = ARService.getDocumentos(xID);
                List<DocPartidas> ListadoDocPartidas = ARService.getDocumentosPartidas(xID);
                List<DocPartidasPersonalizar> ListadoDocPartidasPersonalizar = ARService.getDocumentosPartidasPersonalizar(xID);
                xpedidos.GetDocumentos = ListadoDocumentos;
                xpedidos.GetDocPartidas = ListadoDocPartidas;
                xpedidos.GetDocPartidasPersonalizars = ListadoDocPartidasPersonalizar;
                txtQRCode = xID[0].ToString();
            }
            ViewBag.txtQRCode = txtQRCode;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(txtQRCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            //imgBarCode.Height = 150;
            //imgBarCode.Width = 150;
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ViewBag.imageBytes = ms.ToArray();
                    //imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
            }
            return View(xpedidos);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();

        }
    }
}