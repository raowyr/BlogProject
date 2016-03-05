using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Devevil.Blog.MVC.Client.Models;

namespace Devevil.Blog.MVC.Client.Controllers
{
    public class BaseController : Controller
    {
        public ViewResult Error(string prmRefferalPath, string prmError)
        {
            ErrorViewModel evm = new ErrorViewModel();
            return View("_Error", evm);
        }

        public ViewResult Error(string prmRefferalPath)
        {
            ErrorViewModel evm = new ErrorViewModel();
            return View("_Error", evm);
        }
    }
}
