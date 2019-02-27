using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
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
        public ActionResult AdminProfile()
        {

            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            if (id != null)
            {
                User user = NewUserManager().FindById(id);

                return PartialView("Partials/_PartialAdminProfile", user);
            }
            else
            {
                User defaultUser = new User()
                {
                    Name = "",
                    Surname = "",

                };

                return PartialView("Partials/_PartialAdminProfile", defaultUser);
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult EditAdminProfile(string id)
        {
            try
            {
                ViewBag.CountryList = CountryList();
                ViewBag.RoleList = RoleList();

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
                        UserName = "",
                        EmailConfirmed = false
                    };

                    var newUser1 = new AdminEditUserViewModel()
                    {
                        Adress = user1.Adress,
                        BirthDate = user1.BirthDate.ToString("yyyy-MM-dd"),
                        City = user1.City,
                        Country = user1.Country,
                        Email = user1.Email,
                        Gender = user1.Gender,
                        Name = user1.Name,
                        Surname = user1.Surname,
                        PhoneNumber = user1.PhoneNumber,
                        Username = user1.UserName,
                        EmailConfirmed = user1.EmailConfirmed,


                    };

                    return PartialView("Partials/_PartialEditAdminProfile", newUser1);
                }
                var user = NewUserManager().FindById(id);

                var newUser = new AdminEditUserViewModel()
                {
                    Adress = user.Adress,
                    BirthDate = user.BirthDate.ToString("yyyy-MM-dd"),
                    City = user.City,
                    Country = user.Country,
                    Email = user.Email,
                    Gender = user.Gender,
                    Name = user.Name,
                    Surname = user.Surname,
                    PhoneNumber = user.PhoneNumber,
                    Username = user.UserName,
                    EmailConfirmed = user.EmailConfirmed

                };


                return PartialView("Partials/_PartialEditAdminProfile", newUser);
            }
            catch (Exception ex)
            {

                TempData["model"] = new ErrorViewModel()
                {
                    Text = "Bir Hata Oluştu",
                    ActionName = "Users",
                    ControllerName = "Admin",
                    ErrorCode = 500
                };

                return RedirectToAction("Error", "Home");
            }
        }
        [Authorize(Roles="Admin")]
        [HttpGet]
        public ActionResult Users()
        {
            ViewBag.CountryList = CountryList();

            try
            {
                var users = NewUserManager().Users.ToList();
                var roles = NewRoleManager().Roles.ToList();
                

                if (users != null && roles != null)
                {
                    ViewBag.Roles = roles;
                    ViewBag.RoleList = RoleList();


                    return View(users);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {

                TempData["model"] = new ErrorViewModel()
                {
                    Text = "Bir Hata Oluştu",
                    ActionName = "Users",
                    ControllerName = "Admin",
                    ErrorCode = 500
                };

                return RedirectToAction("Error", "Home");
            }

           
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> EditModal(AdminEditUserViewModel model)
        {
            ViewBag.CountryList = CountryList();
            var roles = NewRoleManager().Roles.ToList();

            ViewBag.Roles = roles;
            ViewBag.RoleList = RoleList();

            if (!ModelState.IsValid)
            {
                return PartialView("Partials/_PartialEditModal", model);
            }

            try
            {
                
                var userManager = NewUserManager();

                User user = userManager.FindByName(model.Username);

                foreach (var role in roles)
                {
                    foreach (var userRole in user.Roles)
                    {
                        if (userRole.RoleId == role.Id)
                        {
                            await userManager.RemoveFromRoleAsync(user.Id, role.Name);
                            await userManager.UpdateAsync(user);
                            break;
                        }
                    }
                }

                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Adress = model.Adress;
                user.BirthDate = Convert.ToDateTime(model.BirthDate);
                user.City = model.City;
                user.Country = model.Country;
                user.Gender = model.Gender;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.UserName = model.Username;



                await userManager.AddToRoleAsync(user.Id, model.Role);
                await userManager.UpdateAsync(user);

                var users = userManager.Users.ToList();


                ViewBag.Roles = roles;
                ViewBag.RoleList = RoleList();


                return RedirectToAction("Users", users);
            }
            catch (Exception ex)
            {

                TempData["model"] = new ErrorViewModel()
                {
                    Text = "Bir Hata Oluştu",
                    ActionName = "Users",
                    ControllerName = "Admin",
                    ErrorCode = 500
                };

                return RedirectToAction("Error", "Home");

            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult DeleteUsers(string id)
        {
            try
            {
                var roles = NewRoleManager().Roles.ToList();

                var userManager = NewUserManager();
                var userStore = NewUserStore();

                var users = userStore.Users.ToList();


                var user = userManager.FindById(id);

                userManager.Delete(user);
                userStore.Context.SaveChanges();

                ViewBag.Roles = roles;


                return View("Users", users);
            }
            catch (Exception ex)
            {

                TempData["model"] = new ErrorViewModel()
                {
                    Text = "Bir Hata Oluştu",
                    ActionName = "Users",
                    ControllerName = "Admin",
                    ErrorCode = 500
                };

                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult EditModal(string id)
        {

            try
            {
                ViewBag.CountryList = CountryList();
                ViewBag.RoleList = RoleList();

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
                        UserName = "",
                        EmailConfirmed = false
                    };

                    var newUser1 = new AdminEditUserViewModel()
                    {
                        Adress = user1.Adress,
                        BirthDate = user1.BirthDate.ToString("yyyy-MM-dd"),
                        City = user1.City,
                        Country = user1.Country,
                        Email = user1.Email,
                        Gender = user1.Gender,
                        Name = user1.Name,
                        Surname = user1.Surname,
                        PhoneNumber = user1.PhoneNumber,
                        Username = user1.UserName,
                        EmailConfirmed = user1.EmailConfirmed,
                        

                    };

                    return PartialView("Partials/_PartialEditModal", newUser1);
                }
                var user = NewUserManager().FindById(id);

                var newUser = new AdminEditUserViewModel()
                {
                    Adress = user.Adress,
                    BirthDate = user.BirthDate.ToString("yyyy-MM-dd"),
                    City = user.City,
                    Country = user.Country,
                    Email = user.Email,
                    Gender = user.Gender,
                    Name = user.Name,
                    Surname = user.Surname,
                    PhoneNumber = user.PhoneNumber,
                    Username = user.UserName,
                    EmailConfirmed = user.EmailConfirmed

                };


                return PartialView("Partials/_PartialEditModal", newUser);
            }
            catch (Exception ex)
            {

                TempData["model"] = new ErrorViewModel()
                {
                    Text = "Bir Hata Oluştu",
                    ActionName = "Users",
                    ControllerName = "Admin",
                    ErrorCode = 500
                };

                return RedirectToAction("Error", "Home");
            }
        }
    }
}