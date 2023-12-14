using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
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

        public ActionResult showdata()
        {
            var res = d.mixes.ToList();
            return View(res);
        }

        [HttpGet]
        public ActionResult showdataJson()
        {
         
            var res = d.mixes.ToList();
    //        var list = JsonConvert.SerializeObject(res,
    //Formatting.None,
    //new JsonSerializerSettings()
    //{
    //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    //});
            return View(res);
        }

        [HttpPost]
        public ActionResult Index(mix dd)
        {
            d.mixes.Add(dd);
            d.SaveChanges();
            return RedirectToAction("showdata");
        }
    }
}