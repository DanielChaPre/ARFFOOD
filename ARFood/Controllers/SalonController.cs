using ARFood.Models;
using ARFood.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace ARFood.Controllers
{
    public class SalonController : Controller
    {
        ARFoodServices ARService;

        public SalonController()
        {
            this.ARService = new ARFoodServices();
        }
        // GET: Salon
        public ActionResult Index()
        {
            if (Session["IdMesa"] !=null)
            {

            }
            return View();
        }

        public ActionResult PruebaRec()
        {
            return View();
        }
        public ActionResult SalonDesign(string IDPedido)
        {
            if (IDPedido == "-25")
            {
                ViewBag.idPedido = IDPedido;
            }
            return View();
        }

        public ActionResult OtroSalon()
        {
            return View();
        }

        public ActionResult SelectSalon()
        {
            return View();
        }


        public ActionResult GuardarSeleccion()
        {
            //string xResult = "";
            //if (xMesas != null)
            //{
            //    foreach (string mesa in xMesas)
            //    {
            //        string xx = mesa.Substring(2);
            //    }
            //}
            //if (Session["ListadoPlatillos"] != null)
            //{
            //    var myvar = 1;
            //}
            //return RedirectToAction("Index", "Home");
            return View();
        }

        [WebMethod]
        public JsonResult SaveSeleccion(List<string> xMesas)
        {
            string xResult = "";
            Session["MesasSeleccionadas"] = xMesas;
            //if (xMesas != null)
            //{
            //    foreach(string mesa in xMesas)
            //    {
            //        string xx = mesa.Substring(2);
            //    }
                
            //}
            //else
            //{

            //}
            //if (Session["ListadoPlatillos"] != null)
            //{
            //    var myvar = 1;
            //}
            //RedirectToAction("Index", "Home");
            return Json(xResult, JsonRequestBehavior.AllowGet);
        }

        [WebMethod]
        public JsonResult CargarSalon()
        {
            List<Salon> salons = ARService.getSalon();
            string[] xtemp = { "-25", "", "", "", "" };
            string[][] xResult = { xtemp };
            Array.Clear(xResult, xResult.GetUpperBound(0), 1);
            for (int i=0; i<salons.Count;i++)
            {
                string[] xMesa = { salons[i].Nombre, salons[i].X.ToString(), salons[i].Y.ToString(), salons[i].width.ToString(), salons[i].height.ToString() };
                Array.Resize(ref xResult, xResult.Length + 1);
                xResult[xResult.GetUpperBound(0)] = xMesa;
            }
            return Json(xResult, JsonRequestBehavior.AllowGet);
        }

        [WebMethod]
        public JsonResult GuardaSalon(List<string> MesasValues)
        {
            string xResult = "";
            if (MesasValues != null)
            {
                xResult = ARService.SaveSalons(MesasValues);
            }
            return Json(xResult, JsonRequestBehavior.AllowGet);
        }

        [WebMethod]
        public JsonResult SeleccionaMesa(string MesaValues)
        {
            string xResult = "";
            if (MesaValues != null)
            {
                xResult = "";
            }
            return Json(xResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Banquetes()
        {
            return View();
        }

        public ActionResult BuscaDisponibilidad(string Fecha, string Hora)
        {
            var folder = Server.MapPath("~/Mesas/");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            else 
            {

                if (Fecha != null && Hora != null)
                {
                    if (Fecha.Length > 0 && Hora.Length > 0)
                    {
                        string txtFecha = Fecha.Substring(6, 4) + Fecha.Substring(3,2) + Fecha.Substring(0,2);
                        string xFile = folder + txtFecha + ".txt";
                        StreamWriter File = new StreamWriter(xFile);
                        File.Flush();
                        string xFecha = Fecha;
                        List<MesasDisponibles> xMesas = ARService.BuscarMesasDisponiblesAllDay(xFecha);
                        foreach (var item in xMesas)
                        {
                            File.WriteLine("M" + item.IDMesa + "-" + item.FechaInicio.ToString("dd/MM/yyyy HH:mm") + "-" + item.FechaFin.ToString("dd/MM/yyyy HH:mm"));
                        }
                        File.Close();
                    }
                }
            }
            return View();
        }
    }
}