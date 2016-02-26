using System;
using System.Collections.Generic;
using System.IO;
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
        #region Index
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Login
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
        #endregion

        #region AccountManagment
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
        #endregion

        #region Categories

        //Recupero dei dati relativi alle categorie
        [Authorize]
        public ActionResult Categories()
        {
            IList<CategoryViewModel> categoryList  = null;
            try
            {
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
                            cvm.Descrizione = c.Description;
                            cvm.Nome = c.Name;
                            cvm.Id = c.Id;

                            categoryList.Add(cvm);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CategoryViewModel cvm = new CategoryViewModel();
                cvm.Descrizione = "Errore durante il caricamento della lista delle categorie presenti...";
                cvm.Nome = "OOPS...";
                cvm.Id = 0;

                categoryList = new List<CategoryViewModel>();
                categoryList.Add(cvm);
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
                        cvm.Nome = c.Name;
                        cvm.Descrizione = c.Description;
                        cvm.Id = c.Id;
                        cvm.FileName = c.ImagePath;
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
                        string fileName = null;
                        if (model.File != null && model.File.ContentLength > 0)
                        {
                            //SALVA IL FILE
                            fileName = Path.GetFileName(model.File.FileName);
                            var path = Path.Combine(Server.MapPath("/Uploads"), fileName);
                            model.File.SaveAs(path);
                            model.FileName = fileName;
                        }
                        CategoryRepository cr = new CategoryRepository(uow.Current);
                        Category c = cr.GetById(model.Id);

                        if (c != null)
                        {
                            c.ModifyCategory(model.Nome, model.Descrizione);

                            if (!String.IsNullOrEmpty(fileName))
                                c.SetImagePath(fileName);
                            else
                                model.FileName = c.ImagePath;

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
                        string fileName = null;
                        if (model.File != null && model.File.ContentLength > 0)
                        {
                            //SALVA IL FILE
                            fileName = Path.GetFileName(model.File.FileName);
                            var path = Path.Combine(Server.MapPath("/Uploads"), fileName);
                            model.File.SaveAs(path);
                            model.FileName = fileName;
                        }

                        CategoryRepository cr = new CategoryRepository(uow.Current);
                        Category c = new Category(model.Nome, model.Descrizione);
                        c.SetImagePath(fileName);

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

        [Authorize]
        public ActionResult CategoryRemoveImage(int id)
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
                        c.SetImagePath(null);

                        cr.SaveOrUpdate(c);
                        uow.Commit();

                        return RedirectToAction("CategoryDetail", new { id = c.Id });
                    }
                    else
                    {
                        cvm.Message = "Si è verificato un errore durante l'aggiornamento dei dati!";
                        return View("CategoryDetail", cvm);
                    }
                }
            }
            catch (Exception ex)
            {
                cvm.Message = "OOPS... si è verificato un problema!";
                return View("CategoryDetail", cvm);
            }
        }
        #endregion

        #region Tags

        //Recupero dei dati relativi alle categorie
        [Authorize]
        public ActionResult Tags()
        {
            IList<TagViewModel> tagList = null;
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    TagRepository tr = new TagRepository(uow.Current);
                    IList<Tag> tmpList = tr.FindAll().ToList();
                    if (tmpList != null)
                    {
                        tagList = new List<TagViewModel>();
                        foreach (var c in tmpList)
                        {
                            TagViewModel cvm = new TagViewModel();

                            cvm.Nome = c.Name;
                            cvm.Id = c.Id;

                            tagList.Add(cvm);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TagViewModel cvm = new TagViewModel();

                cvm.Nome = "OOPS... si è verificato un problema durante il caricamento dei tags!";
                cvm.Id = 0;

                tagList = new List<TagViewModel>();
                tagList.Add(cvm);
            }
            return View(tagList);
        }

        //Recupera i dettagli della singola categoria
        [Authorize]
        public ActionResult TagDetail(int id)
        {
            TagViewModel cvm = new TagViewModel();
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    TagRepository tr = new TagRepository(uow.Current);
                    Tag t = tr.GetById(id);
                    if (t != null)
                    {
                        cvm.Nome = t.Name;
                        cvm.Id = t.Id;
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
        public ActionResult TagDetail(TagViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        TagRepository tr = new TagRepository(uow.Current);
                        Tag t = tr.GetById(model.Id);

                        if (t != null)
                        {
                            t.ModifyName(model.Nome);

                            tr.SaveOrUpdate(t);
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
        public ActionResult TagNew()
        {
            return View();
        }

        //Salvataggio di una nuova categoria
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult TagNew(TagViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        TagRepository tr = new TagRepository(uow.Current);
                        Tag t = new Tag(model.Nome);

                        tr.SaveOrUpdate(t);
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

        #endregion

        #region Pages

        //Recupero dei dati relativi alle categorie
        [Authorize]
        public ActionResult Pages()
        {
            IList<PageViewModel> postList = null;
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    PageRepository pr = new PageRepository(uow.Current);
                    IList<Page> tmpList = pr.FindAll().ToList();
                    if (tmpList != null)
                    {
                        postList = new List<PageViewModel>();
                        foreach (var p in tmpList)
                        {
                            PageViewModel pvm = new PageViewModel();

                            pvm.Id = p.Id;
                            pvm.Data = p.Date.Value;
                            pvm.Titolo = p.Title;

                            postList.Add(pvm);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PageViewModel pvm = new PageViewModel();
                pvm.Titolo = "Errore durante il caricamento della lista delle categorie presenti...";
                //pvm.Categoria = "OOPS...";
                pvm .Id = 0;

                postList = new List<PageViewModel>();
                postList.Add(pvm);
            }
            return View(postList);
        }

        //Recupera i dettagli della singola categoria
        //[Authorize]
        //public ActionResult PageDetail(int id)
        //{
        //    PageViewModel pvm = new PageViewModel();
        //    try
        //    {
        //        using (UnitOfWork uow = new UnitOfWork())
        //        {
        //            PageRepository pr = new PageRepository(uow.Current);
        //            Page p = pr.GetById(id);
        //            if (p != null)
        //            {
        //                pvm.
        //                cvm.Descrizione = c.Description;
        //                cvm.Id = c.Id;
        //                cvm.FileName = c.ImagePath;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        pvm.Message = "Si è verificato un errore durante il caricamento dati";
        //    }
        //    return View(pvm);
        //}

        #endregion
    }
}
