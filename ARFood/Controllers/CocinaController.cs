using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ARFood.Controllers
{
    public class CocinaController : Controller
    {
        // GET: Cocina
        public ActionResult Index()
        {
            return View();
        }
    }
}