using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Web.BLL.Helpers;
using Web.BLL.Repository;
using Web.Models.Entities;
using Web.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TroubleRecord()
        {
            var db = new TroubleRecordRepo().GetAll();

            var list = new List<TroubleRecordViewModel>();


            foreach (var item in db)
            {
                list.Add(new TroubleRecordViewModel()
                {
                    BrandTypes = item.BrandTypes,
                    Types = item.Types,
                    Message = item.Message,
                    PhotoPath = item.PhotoPath
                });
            }

            ViewBag.List = list;

            return View();
        }


        [HttpPost]
        public ActionResult TroubleRecord(TroubleRecordViewModel model)
        {
            try
            {
                var data = new TroubleRecord()
                {
                    BrandTypes = model.BrandTypes,
                    Types = model.Types,
                    Message = model.Message
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


                var a = new TroubleRecordRepo().Insert(data);
            }
            catch (Exception)
            {

                throw;
            }






            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {

            return View();
        }
    }
}