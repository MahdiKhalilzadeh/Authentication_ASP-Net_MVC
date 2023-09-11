using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyApp_Auth.Controllers
{
    [Authorize]
    public class UserPanelController : Controller
    {
        // GET: UserPanel
        public ActionResult Index()
        {
            return View();
        }
    }
}