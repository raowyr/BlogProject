﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Devevil.Blog.Model.Domain.Entities;
using Devevil.Blog.MVC.Client.Models;
using Devevil.Blog.Nhibernate.DAL.Base;
using Devevil.Blog.Nhibernate.DAL.Repositories;

namespace Devevil.Blog.MVC.Client.Controllers
{
    public class ManageController : Controller
    {
        //
        // GET: /Manage/

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (FormsAuthentication.IsEnabled && FormsAuthentication.CookiesSupported && Request.Cookies[FormsAuthentication.FormsCookieName]!=null)
                return RedirectToAction("Index", "Manage");
            else
                return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        AuthorRepository ar = new AuthorRepository(uow.Current);
                        Author au = ar.GetAuthorByEmail(model.Email);

                        if (au != null && au.ValidatePassword(model.Password))
                        {
                            //Login OK
                            if (FormsAuthentication.IsEnabled)
                                FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("Index", "Manage");
                        }
                        else
                        {
                            model.Message = "Email e/o password errate!";
                            return View(model);
                        }
                    }
                }
                catch (Exception ex)
                {
                    model.Message = "OOPS... si è verificato un problema!";
                    return View(model);
                }
            }
            else
                return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            if (FormsAuthentication.IsEnabled)
            {
                FormsAuthentication.SignOut();
            }
            return RedirectToAction("Index", "Manage");
        }

        //Recupero dei dati relativi all'account correntemente in uso
        [Authorize]
        public ActionResult AccountManagment()
        {
            UserViewModel uvm = new UserViewModel();
            try
            {
                if (FormsAuthentication.IsEnabled && FormsAuthentication.CookiesSupported)
                {
                    string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                    if (!String.IsNullOrEmpty(username))
                    {
                        using (UnitOfWork uow = new UnitOfWork())
                        {
                            AuthorRepository ar = new AuthorRepository(uow.Current);
                            Author au = ar.GetAuthorByEmail(username);

                            if (au != null)
                            {
                                uvm.Cognome = au.Surname;
                                uvm.Email = au.Email;
                                uvm.Nascita = au.BirthDate.Value;
                                uvm.Nome = au.Name;
                                uvm.Password = au.Password;
                            }
                            else
                                uvm.Message = "Si è verificato un problema durante il caricamento dati.";
                        }
                    }
                    else
                        uvm.Message = "Si è verificato un problema durante il caricamento dati.";
                }
            }
            catch (Exception ex)
            {
                uvm.Message = "Si è verificato un problema durante il caricamento dati.";
            }
            return View(uvm);
        }

        //Modifica dei dati relativi all'account correntemente in uso
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AccountManagment(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (FormsAuthentication.IsEnabled && FormsAuthentication.CookiesSupported)
                {
                    string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                    if (!String.IsNullOrEmpty(username))
                    {
                        using (UnitOfWork uow = new UnitOfWork())
                        {
                            try
                            {
                                AuthorRepository ar = new AuthorRepository(uow.Current);
                                Author au = ar.GetAuthorByEmail(username);

                                au.ModifyAuthor(model.Nome, model.Cognome, model.Nascita, model.Email);

                                ar.SaveOrUpdate(au);

                                uow.Commit();

                                model.Message = "Modifica dei dati eseguita con successo!";
                            }
                            catch (Exception ex)
                            {
                                model.Message = "Si è verificato un problema durante il salvataggio dei dati.";
                            }
                        }
                    }
                    else
                        model.Message = "Si è verificato un problema durante il salvataggio dei dati.";
                }
            }
            return View(model);
        }

        //Recupero dei dati relativi alle categorie
        [Authorize]
        public ActionResult Categories()
        {
            IList<CategoryViewModel> categoryList  = null;
            using (UnitOfWork uow = new UnitOfWork())
            {
                CategoryRepository cr = new CategoryRepository(uow.Current);
                IList<Category> tmpList = cr.FindAll().ToList();
                if (tmpList != null)
                {
                    categoryList = new List<CategoryViewModel>();
                    foreach (var c in tmpList)
                    {
                        CategoryViewModel cvm = new CategoryViewModel();
                        cvm.CategoryDescription = c.Description;
                        cvm.CategoryName = c.Name;
                        cvm.Id = c.Id;

                        categoryList.Add(cvm);
                    }
                }
            }
            return View(categoryList);
        }

        //Recupera i dettagli della singola categoria
        [Authorize]
        public ActionResult CategoryDetail(int id)
        {
            CategoryViewModel cvm = new CategoryViewModel();
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    CategoryRepository cr = new CategoryRepository(uow.Current);
                    Category c = cr.GetById(id);
                    if (c != null)
                    {
                        cvm.CategoryName = c.Name;
                        cvm.CategoryDescription = c.Description;
                        cvm.Id = c.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                cvm.Message = "Si è verificato un errore durante il caricamento dati";
            }
            return View(cvm);
        }

        //Modifica i dettagli di una categoria selezionata
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryDetail(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        CategoryRepository cr = new CategoryRepository(uow.Current);
                        Category c = cr.GetById(model.Id);

                        if (c != null)
                        {
                            c.ModifyCategory(model.CategoryName, model.CategoryDescription);

                            cr.SaveOrUpdate(c);
                            uow.Commit();

                            model.Message = "Modifica eseguita con successo!";

                            return View(model);
                        }
                        else
                        {
                            model.Message = "Si è verificato un errore durante l'aggiornamento dei dati!";
                            return View(model);
                        }
                    }
                }
                catch (Exception ex)
                {
                    model.Message = "OOPS... si è verificato un problema!";
                    return View(model);
                }
            }
            else
                return View(model);
        }

        [Authorize]
        public ActionResult CategoryNew()
        {
            return View();
        }

        //Salvataggio di una nuova categoria
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryNew(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        CategoryRepository cr = new CategoryRepository(uow.Current);
                        Category c = new Category(model.CategoryName, model.CategoryDescription);

                        cr.SaveOrUpdate(c);
                        uow.Commit();

                        model.Message = "Salvataggio eseguito correttamente!";
                    }
                }
                catch (Exception ex)
                {
                    model.Message = "Errore durante il salavataggio dei dati!";
                }
            }
            return View(model);
        }
    }
}