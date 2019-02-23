using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web.BLL.Identity;
using Web.Models.IdentityEntities;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var rolls = new string[] { "Admin","User","Operator","Technician"};

            foreach (var role in rolls)
            {
                MemberShipTools.NewRoleManager().Create(new Role {

                    Name = role
                });
            }
        }


    }
}
