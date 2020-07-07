using ARFood.Models;
using ARFood.Services;
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
                    List<int> AddProd = new List<int>();
                    foreach(var item in ListadoPlatillos)
                    {
                        AddProd.Add(item.ID);
                    }
                    List<Productos> productos = this.ARService.BuscarProductos(AddProd);
                    for (int i = 0; i< productos.Count(); i++)
                    {
                        ListadoPlatillos.Find(x=> x.ID == productos[i].ID).Producto = productos[i].Producto;
                        ListadoPlatillos.Find(x => x.ID == productos[i].ID).Descripcion = productos[i].Descripcion;
                        ListadoPlatillos.Find(x => x.ID == productos[i].ID).Precio = productos[i].Precio;
                        SubTotal += ListadoPlatillos.Find(x => x.ID == productos[i].ID).Precio * ListadoPlatillos.Find(x => x.ID == productos[i].ID).Cantidad;
                    }
                    ViewBag.SubTotal = SubTotal.ToString("###,##0.00");
                    return PartialView("_MostrarPlatillos", ListadoPlatillos);
                }
                //return View(this.ARService.GetProductos(id.Value));
            }
            ViewBag.ListadoPlatillos = Session["ListadoPlatillos"];
            return PartialView("_MostrarPlatillos");
        }

        public ActionResult Pedido()
        {
            if (Session["ListadoPlatillos"] == null)
            {
                return PartialView("_MostrarPlatillos");
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
            ARService.GuardaPedido(ListadoPlatillos, id);            
            return PartialView("_MostrarPlatillos");
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