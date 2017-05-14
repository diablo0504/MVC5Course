using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [SharedViewBag]
        [LocalOnly]
        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            throw new ArgumentException("Error Handled!!");
            //return View();
        }
        [SharedViewBag(MyProperty = "")]
        public ActionResult PartialAbout()
        {
            ViewBag.Message = "Your application description page.";
            if (Request.IsAjaxRequest())
            {
                return PartialView("About");
            }
            else
            {
                return View();
            }

        }
        public ActionResult SomeAction()
        {
            return PartialView("SuccessRedirect","/");
        }

        public ActionResult UnKnow()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
        public ActionResult GetFile()
        {
            return File(Server.MapPath("~/Content/Image/wan.png"), "image/png", "NewName.png");
        }
        public ActionResult GetJson()
        {
            db.Configuration.LazyLoadingEnabled = false;

            //return View();
            return Json(db.Product.Take(5), JsonRequestBehavior.AllowGet);
        }
        public ActionResult VT()
        {
            ViewBag.IsEnabled = true;
            return View();
            //AutoHotKey
        }
        public ActionResult RazorTest()
        {
            ViewData.Model = new int[] { 1,2,3,4,5};
            return PartialView(ViewData.Model);
        }
    }
}