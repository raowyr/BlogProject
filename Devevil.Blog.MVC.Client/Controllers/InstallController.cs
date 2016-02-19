using Devevil.Blog.Nhibernate.DAL;
using Devevil.Blog.Nhibernate.DAL.Base;
using Devevil.Blog.Nhibernate.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Devevil.Blog.MVC.Client.Controllers
{
    public class InstallController : Controller
    {
        //
        // GET: /Install/

        public ActionResult Index()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                BlogRepository br = new BlogRepository(uow.Current);
                try
                {
                    if (br.FindAll().Count > 0)
                    {
                        //Vai alla pagina di gestione
                    }
                    else
                        return RedirectToAction("Setup", "Install");
                }
                catch (Exception ex)
                {
                    //Il database non esiste, lo inizializzo
                    SessionManager.Instance.BuildSchema();
                    return RedirectToAction("Setup", "Install");
                }
            }
            return View();
        }
        public ActionResult Setup()
        {
            return View();
        }
    }
}
