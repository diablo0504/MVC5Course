using System;
using System.Web.Mvc;
namespace MVC5Course.Controllers
{
    public class SharedViewBagAttribute :ActionFilterAttribute
    {
        public string MyProperty { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
            filterContext.Controller.ViewBag.Message = "Your application description page.";
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
        
    }
}