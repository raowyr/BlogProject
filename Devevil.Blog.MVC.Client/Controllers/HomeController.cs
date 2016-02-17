using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Devevil.Blog.Model.Domain.Entities;
using Devevil.Blog.MVC.Client.Models;
using Devevil.Blog.Nhibernate.DAL.Base;
using Devevil.Blog.Nhibernate.DAL.Repositories;

namespace Devevil.Blog.MVC.Client.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            HomePageViewModel m = new HomePageViewModel();

            //Carica gli ultimi 3 POST
            using (UnitOfWork uow = new UnitOfWork())
            {
                PageRepository pr = new PageRepository(uow.Current);
                IList<Page> pages = pr.GetTop5Post();
                if (pages != null)
                {
                    int k = 0;
                    foreach (var p in pages)
                    {
                        PostViewModel pTemp = new PostViewModel();

                        pTemp.Data = p.Date.Value;
                        pTemp.Testo = p.BodyText;
                        pTemp.Titolo = p.Title;

                        if (k == 0)
                            m.PostDetail.Add(pTemp);
                        else if (k == 1)
                            m.PostDetail.Add(pTemp);
                        else
                            m.PostPreview.Add(pTemp);

                        k++;
                    }
                }
            }
            
            //PostViewModel p1 = new PostViewModel();
            //PostViewModel p2 = new PostViewModel();
            //PostViewModel p3 = new PostViewModel();

            //p1.Titolo = "tuitolo 1!";
            //p1.Testo = "asdbasmndb asmndbam sndb asd...";
            //p1.Data = DateTime.Today;

            //p2.Titolo = "tuitolo 2!";
            //p2.Testo = "asdbasmndb asmndbam sndb asd...";
            //p2.Data = DateTime.Today;

            //p3.Titolo = "tuitolo 3!";
            //p3.Testo = "asdbasmndb asmndbam sndb asd...";
            //p3.Data = DateTime.Today;

            m.Aforisma = "Aforisma random";
            //m.PostPreview.Add(p1);
            //m.PostPreview.Add(p2);
            //m.PostPreview.Add(p3);

            return View(m);
        }

    }
}
