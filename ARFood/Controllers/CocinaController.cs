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
    public class CocinaController : Controller
    {

        ARFoodServices ARService;

        public CocinaController()
        {
            this.ARService = new ARFoodServices();
        }
        // GET: Cocina
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListaPedidos()
        {
            List<Documentos> PedidosDoc = ARService.GetAllOrdenesxMesa();
            return PartialView("_ListaPedidos", PedidosDoc);
        }

        public ActionResult DatosPedido()
        {
            return PartialView("_DatosPedido");
        }

        public ActionResult AccionesPedido()
        {
            return PartialView("_AccionesPedido");
        }

        public ActionResult Cocinero()
        {
            return View();
        }

        public ActionResult RecibeESP32(string Datos)
        {
            if (Datos != null)
            {
                if (Datos.Length > 0)
                {
                    ARService.EntradaESP32(Datos);
                }
            }
            ViewBag.Dato = Datos;
            return View();
        }

        public ActionResult VisualizarESP32()
        {
            return View(ARService.VusualizaEntradasESP32(false));
        }

        [WebMethod]
        public JsonResult VerificaMovimientos()
        {
            ApplicationDbContext xAR = new ApplicationDbContext();
            var consulta = from datos in xAR.esp32
                           where datos.Ya == 0
                           orderby datos.Fecha
                           select datos;

            List<ESP32> eSP = consulta.ToList();

            List<string> xtemp = new List<string>();
            if (eSP.Count > 0)
            {
                xtemp.Add(eSP[0].Texto);
                eSP[0].Ya = 1;
                xAR.SaveChanges();
            }

            return Json(xtemp, JsonRequestBehavior.AllowGet);
        }
    }
}