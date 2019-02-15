using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.Entities;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
         public ActionResult ArizaKayit()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ArizaKayit(ArizaKayit model)
        {
            return View();
        }
    }
}