using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pro_DAL;
namespace Pro_UILayer.Controllers
{
    public class MixController : Controller
    {
        DecDb23NewEntities d = new DecDb23NewEntities();
        // GET: Mix
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(mix dd)
        {
            d.mixes.Add(dd);
            d.SaveChanges();
            return View();
        }
    }
}