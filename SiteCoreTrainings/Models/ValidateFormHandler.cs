using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace SiteCoreTrainings.Models
{
    public class ValidateFormHandler : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var controller = controllerContext.HttpContext.Request.Form["fhController"];
            var action = controllerContext.HttpContext.Request.Form["fhAction"];
            var param = controllerContext.HttpContext.Request.Form["fhParam"];

            return !string.IsNullOrWhiteSpace(controller)
                   && !string.IsNullOrWhiteSpace(action)
                   && !string.IsNullOrWhiteSpace(param)
                   && controller == controllerContext.Controller.GetType().Name
                   && methodInfo.Name == action
                   && methodInfo.GetParameters().Select(p => p.Name).Contains(param)
                   && controllerContext.HttpContext.Request.HttpMethod == "POST";
        }
    }
}