using DevExpress.CodeParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Helpers
{
    public static class ViewHelper
    {
        public static string RenderViewToString(this Controller controller, string viewPath, object model = null, bool partial = false)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            var context = controller.ControllerContext;
            var controllerName = context.RouteData.Values["controller"].ToString();
            var path = $"/views/{controllerName}/{viewPath}.cshtml";
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, path);
            else
                viewEngineResult = ViewEngines.Engines.FindView(context, path, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view, context.Controller.ViewData, context.Controller.TempData, sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }
    }
}
