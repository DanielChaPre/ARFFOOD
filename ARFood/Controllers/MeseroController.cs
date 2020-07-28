using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ARFood.Controllers
{
    public class MeseroController : Controller
    {
        // GET: Mesero
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ModalDialog()
        {
            return View();
        }
    }
}