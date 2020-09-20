using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class RepatriatesController : Controller
    {
        // GET: Repatriates
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegistrationPartial()
        {
            return PartialView();
        }
    }
}