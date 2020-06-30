using ARFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ARFood.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MostrarFamilias()
        {
            using (ApplicationDbContext dbModel = new ApplicationDbContext())
            {
                var FamiliasMenu = dbModel.Familias.ToList();
                return PartialView("_MostrarFamilias", FamiliasMenu);
            }
        }

        public ActionResult MostrarDetalleFamilia(int? ID)
        {
            using (ApplicationDbContext dbModel = new ApplicationDbContext())
            {
                if (ID != null)
                {
                    var DetalleFamilia = dbModel.Productos.Where(x=>x.Familia == ID).ToList();
                    if (DetalleFamilia == null)
                    {
                        return HttpNotFound();
                    }
                    return PartialView("_MostrarDetalleFamilia", DetalleFamilia);
                }
                return PartialView("_MostrarDetalleFamilia");
            }
        }

        public ActionResult MostrarPlatillos()
        {
            using (ApplicationDbContext dbModel = new ApplicationDbContext())
            {
                if (Session["ARFoodUser"] != null)
                {
                    List<int> Platillos = (List<int>)Session["ARFoodUser"];

                    if (codigos == null || codigos.Count == 0)
                    {
                        Session["Videojuegos"] = null;
                        dbModel.Videojuegos.SqlQuery("UPDATE Videojuegos set Cantidad = 0").SingleOrDefaultAsync();
                        //Session["VideojuegosD"] = null;
                    }
                    else
                    {
                        Session["Videojuegos"] = codigos;
                        //Session["VideojuegosD"] = codigosD;
                    }
                    if (vJuego != null)
                    {
                        foreach (var juego in vJuego)
                        {
                            Videojuegos Ojuego = new Videojuegos();
                            var video = dbModel.Videojuegos.Where(x => x.Id == juego.Id).FirstOrDefault();
                            if (video.Cantidad != juego.Cantidad && juego.Cantidad >= 0)
                            {
                                video.Cantidad = juego.Cantidad;
                                dbModel.Entry(video).State = System.Data.Entity.EntityState.Modified;
                                dbModel.SaveChanges();
                                if (video.Cantidad == 0)
                                {
                                    codigos.Remove(video.Id);
                                }
                            }

                        }
                    }
                    if (Session["Videojuegos"] != null)
                    {
                        List<Videojuegos> videjuegos = this.video.BuscarVideoJuegos(codigos);
                        return View(videjuegos);
                    }
                }
                return PartialView("_MostrarDetalleFamilia");
            }
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