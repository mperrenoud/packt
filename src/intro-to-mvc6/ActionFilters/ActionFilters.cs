using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace intro_to_mvc6.ActionFilters
{
    public abstract class ModelStateTempDataTransfer : ActionFilterAttribute
    {
        protected static readonly string Key = typeof(ModelStateTempDataTransfer).FullName;
    }

    public class ExportModelStateToTempData : ModelStateTempDataTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controller = filterContext.Controller as Controller;
            //Only export when ModelState is not valid
            if (!controller.ViewData.ModelState.IsValid)
            {
                //Export if we are redirecting
                if ((filterContext.Result is RedirectResult) ||
                    (filterContext.Result is RedirectToRouteResult) ||
                    (filterContext.Result is RedirectToActionResult))
                {
                    var modelState = JsonConvert.SerializeObject(
                        controller.ViewData
                            .ModelState
                            .Select(x =>
                            {
                                return new KeyValuePair<string, List<string>>(x.Key,
                                    x.Value.Errors.Select(y => y.ErrorMessage).ToList());
                            })
                            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
                    controller.TempData[Key] = modelState;
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }

    public class ImportModelStateFromTempData : ModelStateTempDataTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controller = filterContext.Controller as Controller;

            var tempModelState = controller.TempData[Key] as string;
            if (tempModelState == null)
            {
                return;
            }

            Dictionary<string, List<string>> modelState = (Dictionary<string, List<string>>)JsonConvert.DeserializeObject(
                tempModelState, typeof(Dictionary<string, List<string>>));

            if (modelState.Keys.Count > 0)
            {
                //Only Import if we are viewing
                if (filterContext.Result is ViewResult)
                {
                    foreach (var entry in modelState)
                    {
                        foreach (var error in entry.Value)
                        {
                            controller.ViewData.ModelState.AddModelError(entry.Key, error);
                        }
                    }
                }
                else
                {
                    //Otherwise remove it.
                    controller.TempData.Remove(Key);
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
