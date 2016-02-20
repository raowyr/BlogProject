using Devevil.Blog.Nhibernate.DAL;
using Devevil.Blog.Nhibernate.DAL.Base;
using Devevil.Blog.Nhibernate.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Devevil.Blog.MVC.Client.Models;

namespace Devevil.Blog.MVC.Client.Controllers
{
    public class InstallController : Controller
    {
        //
        // GET: /Install/

        [AllowAnonymous]
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
                }
                catch (Exception ex)
                {
                    //Il database non esiste, lo inizializzo
                    SessionManager.Instance.BuildSchema();
                }
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(SetupViewModel model)
        {
            if (ModelState.IsValid)
            {
                return null;
            }
            else
                return View(model);
        }
    }
}
