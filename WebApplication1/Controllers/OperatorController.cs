using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Web.BLL.Repository;
using Web.Models.Entities;
using Web.Models.EntityIdentity;
using Web.Models.ViewModels;
using static Web.BLL.Identity.MemberShipTools;

namespace WebApplication1.Controllers
{
    public class OperatorController : BaseController
    {
        // GET: Operator
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

        [HttpGet]
        public ActionResult EditTrouble(int? id)
        {
            try
            {

                ViewBag.UserList = UserList();
                ViewBag.Types = TypesList();
                ViewBag.BrandTypes = BrandTypesList();

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
                        UserName = ""

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
                    Message = troubleRecord.Message

                };

                var userTroubleList = new UserTroubleRecordRepo().GetAll();
                var roles = NewRoleManager().Roles.ToList();

                foreach (var userTrouble in userTroubleList)
                {
                    if (userTrouble.Id2 == newTroubleRecord.Id)
                    {
                        foreach (var role in userTrouble.User.Roles)
                        {
                            foreach (var role1 in roles)
                            {
                                if (role.RoleId == role1.Id && role1.Name == "Technician")
                                {
                                    newTroubleRecord.UserName = userTrouble.User.UserName;
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

        [HttpPost]
        public ActionResult EditTrouble(TroubleRecordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Partials/_PartialEditTrouble", model);
            }

            var dbUserTrouble = new UserTroubleRecordRepo();
            var troubleRecords = new TroubleRecordRepo().GetAll();
            var roles = NewRoleManager().Roles.ToList();
            var userTroubleList = new UserTroubleRecordRepo().GetAll();
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

            if (model.PhotoPath != "" && model.PostedFile != null)
            {
                troubleRecord.PhotoPath = model.PhotoPath;
            }

            foreach (var item in userTroubleList)
            {
                if (item.TroubleRecord.Id == model.Id)
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

            var userTrouble = new UserTroubleRecord()
            {
                Id = user.Id,
                Id2 = troubleRecord.Id
            };


            dbUserTrouble.Insert(userTrouble);

            return RedirectToAction("OperatorIndex");

        }
    }
}