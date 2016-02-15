using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Devevil.Blog.MVC.Client.Models;

namespace Devevil.Blog.MVC.Client.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            HomePageViewModel m = new HomePageViewModel();
            PostViewModel p1 = new PostViewModel();
            PostViewModel p2 = new PostViewModel();
            PostViewModel p3 = new PostViewModel();

            p1.Titolo = "tuitolo 1!";
            p1.Testo = "asdbasmndb asmndbam sndb asd...";
            p1.Data = DateTime.Today;

            p2.Titolo = "tuitolo 2!";
            p2.Testo = "asdbasmndb asmndbam sndb asd...";
            p2.Data = DateTime.Today;

            p3.Titolo = "tuitolo 3!";
            p3.Testo = "asdbasmndb asmndbam sndb asd...";
            p3.Data = DateTime.Today;

            m.Aforisma = "Aforisma random";
            m.PostPreview.Add(p1);
            m.PostPreview.Add(p2);
            m.PostPreview.Add(p3);

            return View(m);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SendMessage(MessageViewModel prmHomeModel)
        {
            if (ModelState.IsValid)
            {
                if (prmHomeModel != null)
                {

                }
               
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Prova ajax");
            return Content(sb.ToString());
        }

    }
}
