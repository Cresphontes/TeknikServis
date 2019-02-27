using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Web.BLL.Helpers;
using Web.BLL.Identity;
using Web.BLL.Repository;
using Web.Models.Entities;
using Web.Models.EntityIdentity;
using Web.Models.Enums;
using Web.Models.IdentityEntities;
using Web.Models.ViewModels;
using static Web.BLL.Identity.MemberShipTools;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {


        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TroubleRecord(string type)
        {
            var troubleRecord = new TroubleRecordViewModel();

            if (type != null)
            {
                switch (type)
                {
                    case "Buzdolabi":
                        troubleRecord.Types = Types.buzdolabi;
                        break;
                    case "Bulasik":
                        troubleRecord.Types = Types.bulasik;
                        break;
                    case "Camasir":
                        troubleRecord.Types = Types.camasir;
                        break;
                    case "davlumbaz":
                        troubleRecord.Types = Types.davlumbaz;
                        break;
                    case "Firin":
                        troubleRecord.Types = Types.firin;
                        break;
                    case "Mikrodalga":
                        troubleRecord.Types = Types.mikrodalga;
                        break;
                    case "Ocak":
                        troubleRecord.Types = Types.ocak;
                        break;

                }
                return View(troubleRecord);
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult TroubleRecord(TroubleRecordViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userManager = MemberShipTools.NewUserManager();
                var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
                User user = userManager.FindById(id);

                var data = new TroubleRecord()
                {
                    BrandTypes = model.BrandTypes,
                    Types = model.Types,
                    Message = model.Message,
                    Wait = model.Wait,
                    AtService = model.AtService,
                    Done = model.Done


                };


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

                    data.PhotoPath = "/Upload/" + fileName + fileExt;

                }
                new TroubleRecordRepo().Insert(data);

                var db = new UserTroubleRecordRepo();

                var userTroubleRecord = new UserTroubleRecord();

                userTroubleRecord.Id= user.Id;
                userTroubleRecord.Id2 = data.Id;

                db.Insert(userTroubleRecord);
               
            }
            catch (Exception ex)
            {

                throw ex;
              
            }

            TempData["message"] = "Arıza Talebiniz Başarı ile Oluşturulmuştur.";
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {

            return View();
        }
    }
}