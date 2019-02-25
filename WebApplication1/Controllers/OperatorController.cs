using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.BLL.Repository;
using Web.Models.Entities;
using Web.Models.Enums;
using Web.Models.ViewModels;
using static Web.BLL.Identity.MemberShipTools;

namespace WebApplication1.Controllers
{
    public class OperatorController : BaseController
    {
        // GET: Operator
        public ActionResult OperatorIndex()
        {

            var db = new TroubleRecordRepo().GetAll().ToList();

            return View(db);
        }

        [HttpGet]
        public ActionResult EditTrouble(int id)
        {
            try
            {
              

                ViewBag.UserList = userList;

                ViewBag.Types = TypesList();
                ViewBag.BrandTypes = BrandTypesList();


                if (id == null)
                {
                    TroubleRecord troubleRecord1 = new TroubleRecord();

                    var newTroubleRecord1 = new TroubleRecordViewModel()
                    {
                         Types=troubleRecord1.Types,
                         BrandTypes=troubleRecord1.BrandTypes,
                         PhotoPath=troubleRecord1.PhotoPath,
                         Message=troubleRecord1.Message
                    };

                    return PartialView("Partials/_PartialEditTrouble", newTroubleRecord1);
                }

                var db = new TroubleRecordRepo();

                var troubleRecord = db.GetById(id);

                var newTroubleRecord = new TroubleRecordViewModel()
                {
                    Types = troubleRecord.Types,
                    BrandTypes = troubleRecord.BrandTypes,
                    PhotoPath = troubleRecord.PhotoPath,
                    Message = troubleRecord.Message

                };


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
    }
}