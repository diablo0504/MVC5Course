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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
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
    }
}