using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Web.BLL.Identity;
using Web.Models.IdentityEntities;
using Web.Models.ViewModels;
using static Web.BLL.Identity.MemberShipTools;

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
        public async Task<ActionResult> RegisterIndex(RegisterViewModel model)
        {
            ViewBag.CountryList = CountryList();

            if (!ModelState.IsValid)
            {
                return View("RegisterIndex", model);
            }

            try
            {
                var userManager = NewUserManager();
                var userStore = NewUserStore();

                var isUserNameExist = await userStore.FindByNameAsync(model.Username);

                if (isUserNameExist != null)
                {
                    ModelState.AddModelError("UserName", "Bu kullanıcı adı daha önceden alınmıştır.");

                    return View("RegisterIndex", model);
                }

                var newUser = new User()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    BirthDate = model.BirthDate,
                    Gender = model.Gender,
                    Country = model.Country,
                    City = model.City,
                    Adress = model.Adress,
                    UserName = model.Username
                };

                var result = await userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    if (userStore.Users.Count() == 1)
                    {
                        await userManager.AddToRoleAsync(newUser.Id, "Admin");

                    }
                    else
                    {
                        await userManager.AddToRoleAsync(newUser.Id, "User");
                    }

                    TempData["Message"] = "Kaydınız başarı ile oluşturulmuştur.";

                }
                else
                {
                    var err = "";
                    foreach (var error in result.Errors)
                    {
                        err += error + " ";
                    }

                    ModelState.AddModelError("", err);
                    return View("RegisterIndex", model);
                }

                return RedirectToAction("RegisterIndex");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public ActionResult LoginIndex()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> LoginIndex(LoginViewModel model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View("LoginIndex", model);
                }

                var userManager = NewUserManager();

                var user = await userManager.FindAsync(model.Username, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                    return View("LoginIndex", model);
                }

                var authManager = HttpContext.GetOwinContext().Authentication;

                var userIdentity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

                authManager.SignIn(userIdentity);

            }
            catch (Exception ex)
            {

                throw;
            }


            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;

            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("LoginIndex", "Account");

        }
    }
}