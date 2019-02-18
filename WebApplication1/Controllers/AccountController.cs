using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.IdentityEntities;

namespace WebApplication1.Controllers
{
    public class AccountController : BaseController
    {

        [HttpGet]
        // GET: UserRegister
        public ActionResult RegisterIndex()
        {
            ViewBag.CountryList = CountryList();
            return View();
        }

        [HttpPost]
        public ActionResult RegisterIndex(User model)
        {
            
           
            return View();
        }
    }
}