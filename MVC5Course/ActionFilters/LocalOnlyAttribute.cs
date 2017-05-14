using System;
using System.Web.Mvc;
namespace MVC5Course.Controllers
{
    public class LocalOnlyAttribute : ActionFilterAttribute
    {
        public string MyProperty { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.Request.IsLocal)
            {
                filterContext.Result = new RedirectResult("/");
            }
        }
public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
    }
}