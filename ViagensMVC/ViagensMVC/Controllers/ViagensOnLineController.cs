using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViagensMVC.Db;

namespace ViagensMVC.Controllers
{
    public class ViagensOnLineController : Controller
    {
        // GET: ViagensOnLineController
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult Destinos()
        {
            using(var db = new ViagensOnlineDb())
            {
                return View(db.Destinos.ToList());
            }
        }
    }
}