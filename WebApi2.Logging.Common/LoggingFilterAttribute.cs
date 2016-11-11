using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;

namespace WebApi2.Logging.Common
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var trace = filterContext.ControllerContext.Configuration.Services.GetTraceWriter();
            trace.Info(filterContext.Request, 
                "Controller: " + filterContext.ControllerContext.ControllerDescriptor.ControllerType.FullName
                + Environment.NewLine 
                + "Action: " + filterContext.ActionDescriptor.ActionName, 
                "JSON", filterContext.ActionArguments);
        }
    }
}
