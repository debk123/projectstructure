using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pro_UILayer.Controllers
{
    public class CarUIController : Controller
    {
        // GET: CarUI
        public ActionResult showCars()
        {
            return View();
        }
    }
}