using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.IdentityEntities;

namespace WebApplication1.Controllers
{
    public class AcoountController : Controller
    {

        [HttpGet]
        // GET: UserRegister
        public ActionResult RegisterIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterIndex(User model)
        {

           
            return View();
        }
    }
}