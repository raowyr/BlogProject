﻿using System;
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
            HomePageModel m = new HomePageModel();
            PostModel p1 = new PostModel();
            PostModel p2 = new PostModel();
            PostModel p3 = new PostModel();

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

    }
}
