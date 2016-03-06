using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Devevil.Blog.MVC.Client.Models;

namespace Devevil.Blog.MVC.Client.Controllers
{
    public class ContactController : BaseController
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MessageViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Message = "Grazie per avermi inviato questo messaggio! Ti ricontatterò il prima possibile!";
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

    }
}
