using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilter
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)//metodu ezerek metoda gitmeden önce validation işlemini actionfilter ile merkezi hale getirdik.
        {
            var controller = context.RouteData.Values["controller"];
            var action=context.RouteData.Values["action"];

            var param = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Book")).Value;

            if(param is null) { context.Result = new BadRequestObjectResult($"Dto is null:{controller}{action}"); return; }

            if (!context.ModelState.IsValid) { context.Result = new UnprocessableEntityObjectResult(context.ModelState); }
        }
    }
}
