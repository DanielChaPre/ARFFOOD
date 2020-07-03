using ARFood.Models;
using ARFood.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer;

namespace ARFood.Controllers
{
    public class HomeController : Controller
    {
        ARFoodServices ARService;
        string VBTest = "";
        bool ShouldWait = true;

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
            ShouldWait = true;
            if (id != null)
            {
                List<int> ListadoPlatillos;
                if (Session["ListadoPlatillos"] == null)
                {
                    ListadoPlatillos = new List<int>();
                }
                else
                {
                    ListadoPlatillos = Session["ListadoPlatillos"] as List<int>;
                }
                if (id > 0)
                {
                    ListadoPlatillos.Add(id.Value);
                    Session["ListadoPlatillos"] = ListadoPlatillos;
                }
                else
                {
                    ListadoPlatillos.Remove(id.Value);
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
                    List<Productos> productos = this.ARService.BuscarProductos(ListadoPlatillos);
                    VBTest = "total Productos: " + productos.Count.ToString();
                    ShouldWait = false;
                    return PartialView("_MostrarPlatillos", productos);
                }
                //return View(this.ARService.GetProductos(id.Value));
            }
            ViewBag.ListadoPlatillos = Session["ListadoPlatillos"];
            ShouldWait = false;
            return PartialView("_MostrarPlatillos");
        }

        public ActionResult SubTotal()
        {
            List<int> ListadoPlatillos;
            if (Session["ListadoPlatillos"] != null)
            {
                ListadoPlatillos = Session["ListadoPlatillos"] as List<int>;
                ViewBag.SubTotal = "total Productos" +  ListadoPlatillos.Count.ToString();
            }
            ViewBag.SubTotal2 = "PRueba";
            return PartialView("_SubTotal");
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