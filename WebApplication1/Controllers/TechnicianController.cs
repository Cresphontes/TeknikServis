using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Web.BLL.Repository;
using Web.Models.EntityIdentity;
using static Web.BLL.Identity.MemberShipTools;

namespace WebApplication1.Controllers
{
    public class TechnicianController : Controller
    {
        [Authorize(Roles = "Technician")]
        public ActionResult TechnicianTrouble()
        {

            var db = new UserTroubleRecordRepo();

            var userTrouble = db.GetAll();


            var id =HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();

            var user = NewUserManager().FindById(id);

            var troubleRecord = new TroubleRecordRepo().GetAll();

            ViewBag.User = user;
            ViewBag.TroubleRecord = troubleRecord;

            return View(userTrouble);
        }
        [Authorize(Roles = "Technician")]
        public ActionResult EditTechnicianTroubleWait(int id)
        {
            var db = new TroubleRecordRepo();
            var troubleRecord = db.GetById(id);

            if (troubleRecord.Wait == false)
            {
                troubleRecord.Wait = true;
                troubleRecord.AtService = false;
                troubleRecord.Done = false;
            }
            else
            {
                troubleRecord.Wait = false;
            }
            
            db.Update();

            var db1 = new UserTroubleRecordRepo();

            var userTrouble = db1.GetAll();


            var id1 = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();

            var user = NewUserManager().FindById(id1);

            var troubleRecord1 = new TroubleRecordRepo().GetAll();

            ViewBag.User = user;
            ViewBag.TroubleRecord = troubleRecord1;

            return View("TechnicianTrouble", userTrouble);
        }
        [Authorize(Roles = "Technician")]
        public ActionResult EditTechnicianTroubleAtService(int id)
        {
            var db = new TroubleRecordRepo();
            var troubleRecord = db.GetById(id);

            if (troubleRecord.AtService == false)
            {
                troubleRecord.Wait = false;
                troubleRecord.AtService = true;
                troubleRecord.Done = false;
            }
            else
            {
                troubleRecord.AtService = false;
            }
           
            db.Update();


            var db1 = new UserTroubleRecordRepo();

            var userTrouble = db1.GetAll();


            var id1 = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();

            var user = NewUserManager().FindById(id1);

            var troubleRecord1 = new TroubleRecordRepo().GetAll();

            ViewBag.User = user;
            ViewBag.TroubleRecord = troubleRecord1;

            return View("TechnicianTrouble", userTrouble);
        }
        [Authorize(Roles = "Technician")]
        public ActionResult EditTechnicianTroubleDone(int id)
        {

            var db = new TroubleRecordRepo();
            var troubleRecord = db.GetById(id);

            

            if (troubleRecord.Done == false)
            {
                troubleRecord.Wait = false;
                troubleRecord.AtService = false;
                troubleRecord.Done = true;
            }
            else
            {
                troubleRecord.Done = false;
            }
           
            db.Update();

            var db1 = new UserTroubleRecordRepo();

            var userTrouble = db1.GetAll();


            var id1 = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();

            var user = NewUserManager().FindById(id1);

            var troubleRecord1 = new TroubleRecordRepo().GetAll();

            ViewBag.User = user;
            ViewBag.TroubleRecord = troubleRecord1;

            return View("TechnicianTrouble", userTrouble);
        }
    }
}