using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Web.BLL.Identity;
using Web.Models.IdentityEntities;
using Web.Models.ViewModels;
using static Web.BLL.Identity.MemberShipTools;
using Web.BLL.Helpers;
using Web.BLL.Services;

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
                    BirthDate = model.BirthDate.Date,
                    Gender = model.Gender,
                    Country = model.Country,
                    City = model.City,
                    Adress = model.Adress,
                    UserName = model.Username,
                    ActivationCode = StringHelpers.GetCode()

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

                    string siteUrl = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host +
                                        (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);

                    var emailService = new EmailService();

                    var body = $"Merhaba <b>{newUser.Name} {newUser.Surname}</b><br>Hesabınızı aktif etmek için aşağıdaki linke tıklayınız<br> <a href='{siteUrl}/Account/Activation?code={newUser.ActivationCode}'>Aktivasyon Linki</a>";

                    await emailService.SendAsync(new IdentityMessage()
                    {
                        Body = body,
                        Subject = "Sitemize Hoşgeldiniz"
                    }, newUser.Email);



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
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Looks Like You Are Lost In Space",
                    ActionName = "Index",
                    ControllerName = "Home",
                    ErrorCode = 500
                };

                return RedirectToAction("Error", "Home");

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
                if(user.EmailConfirmed == true)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;

                    var userIdentity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

                    authManager.SignIn(new AuthenticationProperties()
                    {
                        IsPersistent = model.RememberMe

                    }, userIdentity);
                   
                }
                else
                {
                    TempData["ConfirmMessage"] = "Hesabınıza giriş yapmak için mailinize gelen aktivasyon linkine tıklayınız.";
                    return View("LoginIndex", model);
                }


            }
            catch (Exception ex)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Looks Like You Are Lost In Space",
                    ActionName = "Index",
                    ControllerName = "Home",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;

            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);



            return RedirectToAction("LoginIndex", "Account");

        }

        [HttpGet]
        public ActionResult Activation(string code)
        {

            try
            {
                var userStore = NewUserStore();

                var user = userStore.Users.FirstOrDefault(x => x.ActivationCode == code);

                if (user != null)
                {
                    if (user.EmailConfirmed == true)
                    {
                        ViewBag.Message = "<span class='alert alert-success'>Bu Hesap Daha Önce Aktive Edilmiştir.</span>";

                    }
                    else
                    {
                        user.EmailConfirmed = true;
                        userStore.Context.SaveChanges();

                        ViewBag.Message = "<span class='alert alert-success'>Aktivasyon işleminiz başarılı.</span>";
                    }

                }
                else
                {
                    ViewBag.Message = "<span class='alert alert-danger'>Aktivasyon işleminiz başarısız.</span>";
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"<span class='alert alert-danger'>Aktivasyon işleminde bir hata oluştu.</span>";
            }

            return View();
        } 


    }
}