 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyApp_Auth.Areas.Admin.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        // GET: Admin/Panel
        public ActionResult Index()
        {
            return View();
        }
    }
}