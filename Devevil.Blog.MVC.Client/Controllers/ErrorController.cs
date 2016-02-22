using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Devevil.Blog.MVC.Client.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

    }
}
