using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.Enums;
using static Web.BLL.Identity.MemberShipTools;

namespace WebApplication1.Controllers
{
    public class BaseController : Controller
    {

        protected List<SelectListItem> CountryList()
        {

            var list = new List<SelectListItem>()
            {
                new SelectListItem{Text="---Select---",Value="null"},
                new SelectListItem{Text="Türkiye",Value="TR"},
                new SelectListItem { Text = "Almanya", Value = "ALM" },
                new SelectListItem { Text = "İtalya", Value = "ITL" },
                new SelectListItem { Text = "İngiltere", Value = "ING" },
                new SelectListItem{Text="ABD",Value="AB"}

            };

            return list;

        }

        protected List<SelectListItem> RoleList()
        {

            var list = new List<SelectListItem>()
            {
                new SelectListItem{Text = "Admin"},
                new SelectListItem{Text = "User"},
                new SelectListItem{Text = "Operator"},
                new SelectListItem{Text = "Technician"}

            };
            return list;
        }

        protected List<SelectListItem> UserList()
        {
            var userManager = NewUserManager();
            var roleManager = NewRoleManager();
            var userList = userManager.Users.ToList();
            var roleList = roleManager.Roles.ToList();

            var list = new List<SelectListItem>();

            foreach (var user in userList)
            {
                foreach (var role in roleList)
                {
                    foreach (var userRole in user.Roles.ToList())
                    {
                        if (userRole.RoleId == role.Id && role.Name == "Technician")
                        {
                            list.Add(new SelectListItem { Text = user.UserName});
                            break;
                        }
                        else
                        {
                            continue;
                        }
                       
                    }
                    
                    
                }
               
            }

            return list;
        }



        protected List<SelectListItem> TypesList()
        {

            var typesList = Enum.GetNames(typeof(Types)).ToList();

            var list = new List<SelectListItem>();

            foreach (var item in typesList)
            {
                list.Add(new SelectListItem { Text = item });
            }


            return list;
        }

        protected List<SelectListItem> BrandTypesList()
        {

            var brandTypesList = Enum.GetNames(typeof(BrandTypes)).ToList();

            var list = new List<SelectListItem>();

            foreach (var item in brandTypesList)
            {
                list.Add(new SelectListItem { Text = item });
            }


            return list;
        }


    }
}