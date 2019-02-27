using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Web.BLL.Helpers;
using Web.BLL.Repository;
using Web.Models.Entities;
using Web.Models.EntityIdentity;
using Web.Models.IdentityEntities;
using Web.Models.ViewModels;
using static Web.BLL.Identity.MemberShipTools;

namespace WebApplication1.Controllers
{
    public class OperatorController : BaseController
    {
       
        public ActionResult OperatorProfile()
        {
            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            if (id != null)
            {
                User user = NewUserManager().FindById(id);

                return PartialView("Partials/_PartialOperatorProfile", user);
            }
            else
            {
                User defaultUser = new User()
                {
                    Name = "",
                    Surname = "",

                };

                return PartialView("Partials/_PartialOperatorProfile", defaultUser);
            }
        }
        [Authorize(Roles = "Operator")]
        public ActionResult EditOperatorProfile(string id)
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

                    return PartialView("Partials/_PartialEditOperatorProfile", newUser1);
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


                return PartialView("Partials/_PartialEditOperatorProfile", newUser);
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
        [Authorize(Roles = "Operator")]
        public ActionResult OperatorIndex()
        {

            var troubleRecords = new TroubleRecordRepo().GetAll();
            var roles = NewRoleManager().Roles.ToList();
            var userTroubleList = new UserTroubleRecordRepo().GetAll();
            var users = NewUserManager().Users.ToList();

            if (troubleRecords != null && roles != null)
            {
                ViewBag.Roles = roles;
                ViewBag.userTroubleList = userTroubleList;
                ViewBag.Users = users;

                return View(troubleRecords);
            }
            else
            {
                return View();
            }


        }
        [Authorize(Roles = "Operator")]
        [HttpGet]
        public ActionResult EditTrouble(int? id)
        {
            try
            {

                ViewBag.UserList = UserList();


                var users = NewUserManager().Users.ToList();
                ViewBag.Users = users;


                if (id == null)
                {
                    TroubleRecord troubleRecord1 = new TroubleRecord();

                    var newTroubleRecord1 = new TroubleRecordViewModel()
                    {
                        Id = troubleRecord1.Id,
                        Types = troubleRecord1.Types,
                        BrandTypes = troubleRecord1.BrandTypes,
                        PhotoPath = troubleRecord1.PhotoPath,
                        Message = troubleRecord1.Message,
                        UserName = "",


                    };

                    return PartialView("Partials/_PartialEditTrouble", newTroubleRecord1);
                }

                var db = new TroubleRecordRepo();

                var troubleRecord = db.GetById(id);

                var newTroubleRecord = new TroubleRecordViewModel()
                {
                    Id = troubleRecord.Id,
                    Types = troubleRecord.Types,
                    BrandTypes = troubleRecord.BrandTypes,
                    PhotoPath = troubleRecord.PhotoPath,
                    Message = troubleRecord.Message,


                };

                var userTroubleList = new UserTroubleRecordRepo().GetAll();
                var roles = NewRoleManager().Roles.ToList();

                foreach (var userTrouble in userTroubleList)
                {
                    if (userTrouble.Id2 == newTroubleRecord.Id)
                    {
                        foreach (var item1 in users)
                        {
                            if (userTrouble.Id == item1.Id)
                            {
                                foreach (var item2 in item1.Roles)
                                {
                                    foreach (var item3 in roles)
                                    {
                                        if (item2.RoleId == item3.Id && item3.Name == "Technician")
                                        {
                                            newTroubleRecord.UserName = userTrouble.User.UserName;
                                        }
                                    }

                                }
                            }

                        }


                    }

                }

                return PartialView("Partials/_PartialEditTrouble", newTroubleRecord);
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
        [Authorize(Roles = "Operator")]
        [HttpPost]
        public ActionResult EditTrouble(TroubleRecordViewModel model)
        {
            ViewBag.UserList = UserList();


            if (model.PostedFile != null)
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("Partials/_PartialEditTrouble", model);
                }
            }

            var troubleDb = new TroubleRecordRepo();

            var dbUserTrouble = new UserTroubleRecordRepo();
            var troubleRecords = troubleDb.GetAll();
            var roles = NewRoleManager().Roles.ToList();
            var userTroubleList = dbUserTrouble.GetAll();
            var users = NewUserManager().Users.ToList();

            ViewBag.Roles = roles;
            ViewBag.userTroubleList = userTroubleList;
            ViewBag.Users = users;

            var userManager = NewUserManager();
            var user = userManager.FindByName(model.UserName);

            var dbTrouble = new TroubleRecordRepo();
            var troubleRecord = dbTrouble.GetById(model.Id);

            troubleRecord.Types = model.Types;
            troubleRecord.BrandTypes = model.BrandTypes;
            troubleRecord.Message = model.Message;

            if (model.PostedFile != null && model.PostedFile.ContentLength > 0)
            {

                var file = model.PostedFile;
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string fileExt = Path.GetExtension(file.FileName);
                fileName = StringHelpers.UrlFormatConverter(fileName);
                fileName += StringHelpers.GetCode();
                var klasorYolu = Server.MapPath("~/Upload/");
                var dosyaYolu = Server.MapPath("~/Upload/") + fileName + fileExt;

                if (!Directory.Exists(klasorYolu))
                    Directory.CreateDirectory(klasorYolu);

                file.SaveAs(dosyaYolu);

                WebImage img = new WebImage(dosyaYolu);
                img.Resize(500, 500, false);
                img.Save(dosyaYolu);

                troubleRecord.PhotoPath = "/Upload/" + fileName + fileExt;

            }
            else
            {
                troubleRecord.PhotoPath = troubleRecord.PhotoPath;
            }

            troubleDb.Update(troubleRecord);

            foreach (var item in userTroubleList)
            {
                foreach (var item0 in troubleRecords)
                {
                    if (item.Id2 == item0.Id && item0.Id == model.Id)
                    {
                        foreach (var item1 in users)
                        {
                            if (item.Id == item1.Id)
                            {
                                foreach (var item2 in item1.Roles)
                                {
                                    foreach (var item3 in roles)
                                    {
                                        if (item2.RoleId == item3.Id && item3.Name == "Technician")
                                        {
                                            dbUserTrouble.Delete(item);
                                        }
                                    }

                                }
                            }

                        }

                    }

                }


            }

            var userTrouble = new UserTroubleRecord()
            {
                Id = user.Id,
                Id2 = troubleRecord.Id
            };


            dbUserTrouble.Insert(userTrouble);

            return RedirectToAction("OperatorIndex");

        }

        [Authorize(Roles = "Operator")]
        [HttpGet]
        public ActionResult DeleteTroubles(int? id)
        {
            try
            {

                var db = new TroubleRecordRepo();

                var trouble = db.GetById(id);
                db.Delete(trouble);

                var newDb = new TroubleRecordRepo();
                var troubleList = newDb.GetAll();

                var roles = NewRoleManager().Roles.ToList();
                var userTroubleList = new UserTroubleRecordRepo().GetAll();
                var users = NewUserManager().Users.ToList();

                if (troubleList != null && roles != null)
                {
                    ViewBag.Roles = roles;
                    ViewBag.userTroubleList = userTroubleList;
                    ViewBag.Users = users;

                    return View("OperatorIndex", troubleList);
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
    }
}