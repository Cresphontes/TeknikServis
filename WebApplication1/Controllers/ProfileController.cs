using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.IdentityEntities;
using static Web.BLL.Identity.MemberShipTools;

namespace WebApplication1.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult ProfileIndex()
        {
            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();

            User user = NewUserManager().FindById(id);

            return PartialView("Partials/_PartialProfile",user);
        }
    }
}