using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.IdentityEntities;
using Web.Models.ViewModels;
using static Web.BLL.Identity.MemberShipTools;

namespace WebApplication1.Controllers
{
    public class AdminController :  BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Users()
        {

            var users = NewUserManager().Users.ToList();
            var roles = NewRoleManager().Roles.ToList();

            ViewBag.Roles = roles;
            return View(users);
        }

        [HttpGet]
        public ActionResult EditModal(string id)
        {
            ViewBag.CountryList = CountryList();

            if (id == null)
            {
                User user1 = new User()
                {
                    Adress = "",
                    BirthDate = DateTime.Now,
                    City = "",
                    Country = "",
                    Email = "",
                    Gender = "",
                    Name = "",
                    PhoneNumber = "",
                    UserName = ""
                };

                var newUser1 = new RegisterViewModel()
                {
                    Adress = user1.Adress,
                    BirthDate = user1.BirthDate.Date,
                    City = user1.City,
                    Country = user1.Country,
                    Email = user1.Email,
                    Gender = user1.Gender,
                    Name = user1.Name,
                    Surname=user1.Surname,
                    PhoneNumber = user1.PhoneNumber,
                    Username = user1.UserName

                };

                return PartialView("Partials/_PartialEditModal", newUser1);
            }
            var user = NewUserManager().FindById(id);

            var newUser = new RegisterViewModel()
            {
                Adress = user.Adress,
                BirthDate = user.BirthDate.Date,
                City = user.City,
                Country = user.Country,
                Email = user.Email,
                Gender = user.Gender,
                Name = user.Name,
                Surname=user.Surname,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName

            };


            return PartialView("Partials/_PartialEditModal", newUser);
        }
    }
}