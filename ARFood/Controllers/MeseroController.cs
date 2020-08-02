using ARFood.Models;
using ARFood.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace ARFood.Controllers
{
    public class MeseroController : Controller
    {

        ARFoodServices ARService;

        public MeseroController()
        {
            this.ARService = new ARFoodServices();
        }
        // GET: Mesero
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ModalDialog()
        {
            return View();
        }

        public ActionResult SurtePedido(string IDOrden)
        {

            if (IDOrden != null)
            {
                List<Guid> xID = new List<Guid>();
                xID.Add(Guid.Parse(IDOrden));
                List<DocPartidas> ListadoDocPartidas = ARService.getDocumentosPartidas(xID);
                return PartialView("_SurtePedido", ListadoDocPartidas);
            }
            return PartialView("_SurtePedido");
        }

        public ActionResult MostrarListaPedidosASurtir()
        {
            if (Session["IDMesaSeleccionada"] != null)
            {
                int MesaID = Convert.ToInt32(Session["IDMesaSeleccionada"].ToString());
                List<Documentos> MesasxDoc = ARService.GetOrdenesxMesa(MesaID);
                ViewBag.MesasCargadas = MesasxDoc.Count();
                Session["IDMessaApartada"] = MesasxDoc;
                return PartialView("_MostrarListaPedidosASurtir", MesasxDoc);
            }
            return PartialView("_MostrarListaPedidosASurtir");
        }

        public ActionResult Herramientas()
        {
            if (Session["IDMesaSeleccionada"] != null)
            {
                int MesaID = Convert.ToInt32(Session["IDMesaSeleccionada"].ToString());
                List<Documentos> MesasxDoc = ARService.GetOrdenesxMesa(MesaID);
                ViewBag.MesasCargadas = MesasxDoc.Count();
                Session["IDMessaApartada"] = MesasxDoc;
                return PartialView("_Herramientas", MesasxDoc);
            }
            return PartialView("_Herramientas");
        }

        [WebMethod]
        public JsonResult TransferPedido(string IDDoc, string IDMesa)
        {
            ARService.GuardaTransferPedido(IDDoc, IDMesa.Substring(2));
            return Json("Mesa Guardada", JsonRequestBehavior.AllowGet);
        }

        [WebMethod]
        public JsonResult GuardaNombrePedido(string IDDoc, string Nombre)
        {
            ARService.SaveNombreMesa(IDDoc, Nombre);
            return Json("Mesa Guardada", JsonRequestBehavior.AllowGet);
        }

        public ActionResult PagarOrdern(string IDDoc)
        {
            if (Session["IDMesaSeleccionada"] != null)
            {
                int MesaID = Convert.ToInt32(Session["IDMesaSeleccionada"].ToString());
                List<Documentos> MesasxDoc = ARService.GetOrdenesxMesa(MesaID);
                ViewBag.MesasCargadas = MesasxDoc.Count();
                Session["IDMessaApartada"] = MesasxDoc;
                return PartialView("_PagarOrdern", MesasxDoc);
            }
            return PartialView("_PagarOrdern");
        }

        [WebMethod]
        public JsonResult PagarOrden(List<string> xOrden)
        {
            List<Guid> xOrdenes = new List<Guid>();
            foreach(var item in xOrden)
            {
                xOrdenes.Add(Guid.Parse(item));
            }
            ARService.SavePagarOrden(xOrdenes);
            return Json("Orden Pagada Correctamente", JsonRequestBehavior.AllowGet);
        }
    }
}