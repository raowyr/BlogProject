using Devevil.Blog.Nhibernate.DAL;
using Devevil.Blog.Nhibernate.DAL.Base;
using Devevil.Blog.Nhibernate.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Devevil.Blog.MVC.Client.Models;
using Devevil.Blog.Model.Domain.Entities;

namespace Devevil.Blog.MVC.Client.Controllers
{
    public class InstallController : BaseController
    {
        //
        // GET: /Install/

        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    BlogRepository br = new BlogRepository(uow.Current);
                    try
                    {
                        if (br.FindAll().Count > 0)
                        {
                            //Vai alla pagina di gestione
                            return RedirectToAction("Index", "Manage");
                        }
                        else
                            return View();
                    }
                    catch (Exception ex)
                    {
                        //Il database non esiste, lo inizializzo
                        SessionManager.Instance.BuildSchema();
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SetupViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Model.Domain.Entities.Blog b = new Model.Domain.Entities.Blog(model.Blog,
                        model.Descrizione);

                    Author au = new Author(model.Nome,
                        model.Cognome,
                        model.Nascita,
                        model.Email,
                        true,
                        model.Password,
                        b);

                    Category cc = new Category("Nessuna categoria", "Categoria generica");

                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        BlogRepository br = new BlogRepository(uow.Current);
                        AuthorRepository ar = new AuthorRepository(uow.Current);
                        CategoryRepository cr = new CategoryRepository(uow.Current);

                        br.Save(b);
                        ar.Save(au);
                        cr.Save(cc);

                        uow.Commit();
                    }
                    //Vai alla pagina di gestione
                    return RedirectToAction("Index", "Manage");
                }
                else
                    return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}
