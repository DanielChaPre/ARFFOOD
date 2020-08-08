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
            CocineroData xCheft = ARService.GetAllOrdenesxMesa();
            return PartialView("_ListaPedidos", xCheft);
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

        public ActionResult RecibeESP32(string Dato)
        {
            if (Dato != null)
            {
                if (Dato.Length > 0)
                {
                    ARService.EntradaESP32(Dato);
                }
            }
            ViewBag.Dato = Dato;
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