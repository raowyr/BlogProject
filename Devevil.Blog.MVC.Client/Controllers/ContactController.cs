using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Devevil.Blog.MVC.Client.Models;

namespace Devevil.Blog.MVC.Client.Controllers
{
    public class ContactController : Controller
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
            if (ModelState.IsValid)
            {
                model.Message = "Grazie per avermi inviato questo messaggio! Ti ricontatterò il prima possibile!";
            }
            return View(model);
        }

    }
}
