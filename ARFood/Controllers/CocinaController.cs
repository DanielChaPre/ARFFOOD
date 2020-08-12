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

        public ActionResult DetallePedidos(string idPartida)
        {
            if (idPartida!= null)
            {
                if (idPartida.Length>0)
                {
                    int ID = Convert.ToInt32(idPartida);
                    CocinaPartidas xCPartidas = new CocinaPartidas();
                    xCPartidas.GetDocPartidas = ARService.getDocumentosPartidasByID(ID);
                    xCPartidas.GetDocPartidasPersonalizars = ARService.getDocumentosPartidasPersonalizarByID(ID);
                    List<int> xProd = new List<int>();
                    xProd.Add(xCPartidas.GetDocPartidas.First().IDProd);
                    xCPartidas.GetProductos = ARService.BuscarProductos(xProd);
                    xCPartidas.GetRecetas = ARService.BuscarRecetas(xProd);
                    xCPartidas.GetIngredientes = ARService.BuscarIngredientes(xProd);
                    List<int> ingredientes = new List<int>();
                    foreach(var item in xCPartidas.GetIngredientes)
                    {
                        ingredientes.Add(item.IDProd);
                    }
                    xCPartidas.getIngrProd = ARService.BuscarIngrProd(ingredientes);
                    return PartialView("_DetallePedidos",xCPartidas);
                }
                
            }
            return PartialView("_DetallePedidos");
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

            //List<ESP32> eSP = consulta.ToList();

            ESP32 eSP = consulta.FirstOrDefault();
            List<string> xtemp = new List<string>();
            if (eSP != null)
            {
                xtemp.Add(eSP.Texto + "-" + eSP.ID);
                //eSP.Ya = 1;
                //xAR.SaveChanges();
            }

            return Json(xtemp, JsonRequestBehavior.AllowGet);
        }

        [WebMethod]
        public JsonResult AplicaMovimiento(string idMov)
        {
            if (idMov != null)
            {
                if (idMov.Length > 0)
                {
                    int xMov = Convert.ToInt32(idMov);
                    ApplicationDbContext xAR = new ApplicationDbContext();
                    var consulta = from datos in xAR.esp32
                                   where datos.ID == xMov
                                   orderby datos.Fecha
                                   select datos;

                    //List<ESP32> eSP = consulta.ToList();

                    ESP32 eSP = consulta.FirstOrDefault();
                    if (eSP != null)
                    {
                        eSP.Ya = 1;
                        xAR.SaveChanges();
                    }
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrendeLED()
        {
            //List<string> xLEDs = new List<string>();
            //xLEDs.Add (LED1 != null ? LED1 : "N");
            //xLEDs.Add(LED2 != null ? LED2 : "N");
            //xLEDs.Add(LED3 != null ? LED3 : "N");
            //xLEDs.Add(LED4 != null ? LED4 : "N");
            //xLEDs.Add(LED5 != null ? LED5 : "N");
            //xLEDs.Add(LED6 != null ? LED6 : "N");
            //ARService.SaveLEDs(xLEDs);

            return View();
        }

        public ActionResult LEDs()
        {
            return Redirect("http://192.168.0.37/VVVRRR");
            //return PartialView("_LEDs");
        }

        [WebMethod]
        public ActionResult WebLED()
        {
            return Json(new { url = "http://192.168.0.37/VVVRRR" });
            //return Redirect("http://www.google.com");
        }

        [WebMethod]
        public JsonResult GuardaLED(List<string> sLEDs)
        {
            if (sLEDs != null)
            {
                List<string> xLEDs = new List<string>();
                for (int i = 0; i < sLEDs.Count; i++)
                {
                    xLEDs.Add(sLEDs[i].ToString());
                }
                ARService.SaveLEDs(xLEDs);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrendeLEDs()
        {
            return View();
        }

        [WebMethod]
        public JsonResult VerificaLEDs()
        {
            ApplicationDbContext xAR = new ApplicationDbContext();
            var consulta = from datos in xAR.LEDs
                           orderby datos.ID
                           select datos;

            List<LED> xLED = consulta.ToList();

            string xtemp = "http://192.168.0.37/";
            if (xLED.Count > 0)
            {
                foreach(LED item in xLED)
                {
                    xtemp += item.Estado;
                }
            }

            return Json(xtemp, JsonRequestBehavior.AllowGet);
        }
    }
}