using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class BaseController : Controller
    {
   
        protected List<SelectListItem> CountryList()
        {

            var list = new List<SelectListItem>()
            {
                new SelectListItem{Text="---Select---"},
                new SelectListItem{Text="Türkiye",Value="TR"},
                new SelectListItem { Text = "Almanya", Value = "ALM" },
                new SelectListItem { Text = "İtalya", Value = "ITL" },
                new SelectListItem { Text = "İngiltere", Value = "ING" },
                new SelectListItem{Text="ABD",Value="AB"}

            };



            return list;

           
          
        }
    }
}