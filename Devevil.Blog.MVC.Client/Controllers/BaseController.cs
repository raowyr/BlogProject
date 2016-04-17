using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Devevil.Blog.MVC.Client.Models;
using Devevil.Blog.Logger.Log4Net;

namespace Devevil.Blog.MVC.Client.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            Logger<BaseController>.Configure();
        }

        public ViewResult Error(Exception prmError)
        {
            ErrorViewModel evm = new ErrorViewModel();
            if(Request!=null && Request.Url!=null)
                evm.RefferalUrl = Request.Url.ToString();
            evm.Message = prmError.Message + "/" + prmError.Source + "/" + prmError.StackTrace;

            Logger.Log4Net.Logger<BaseController>.Error(prmError);

            return View("_Error", evm);
        }

        public ViewResult Error(string prmError)
        {
            ErrorViewModel evm = new ErrorViewModel();
            if (Request != null && Request.Url != null)
                evm.RefferalUrl = Request.Url.ToString();
            evm.Message = prmError;

            Logger.Log4Net.Logger<BaseController>.Error(prmError);

            return View("_Error", evm);
        }

        //public ViewResult Error()
        //{
        //    ErrorViewModel evm = new ErrorViewModel();
        //    if (Request != null && Request.Url != null)
        //        evm.RefferalUrl = Request.UrlReferrer.ToString();
        //    return View("_Error", evm);
        //}
    }
}
