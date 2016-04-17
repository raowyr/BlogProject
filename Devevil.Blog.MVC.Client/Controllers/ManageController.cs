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
    public class ManageController : BaseController
    {

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
            try
            {
                if (ModelState.IsValid)
                {
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        AuthorRepository ar = new AuthorRepository(uow.Current);
                        Author au = ar.GetAuthorByEmail(model.Email);

                        if (au != null && au.ValidatePassword(model.Password) && au.IsAdministrator)
                        {
                            //Login OK
                            if (FormsAuthentication.IsEnabled && FormsAuthentication.CookiesSupported)
                                FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("Index", "Manage");
                        }
                        else
                        {
                            model.Message = "Email e/o password errate, oppure non hai i diritti per accedere!";
                            return View(model);
                        }
                    }
                }
                else
                    return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            if (FormsAuthentication.IsEnabled && FormsAuthentication.CookiesSupported && Request.Cookies[FormsAuthentication.FormsCookieName] != null)
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
            try
            {
                UserViewModel uvm = new UserViewModel();
                if (FormsAuthentication.IsEnabled && FormsAuthentication.CookiesSupported && Request.Cookies[FormsAuthentication.FormsCookieName] != null)
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

                    return View(uvm);
                }
                else
                {
                    uvm.Message = "Sessione utente probabilmente scaduta! Riprova a fare il login e assicurati di avere i cookie abilitati!";
                    return View(uvm);
                }
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
            
        }

        //Modifica dei dati relativi all'account correntemente in uso
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AccountManagment(UserViewModel model)
        {
            try
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

                                AuthorRepository ar = new AuthorRepository(uow.Current);
                                Author au = ar.GetAuthorByEmail(username);

                                au.ModifyAuthor(model.Nome, model.Cognome, model.Nascita, model.Email);

                                ar.SaveOrUpdate(au);

                                uow.Commit();

                                model.Message = "Modifica dei dati eseguita con successo!";
                            }
                        }
                        else
                            model.Message = "Si è verificato un problema durante il recupero dei dati. Non è stato possibile apportare le modifiche richieste!";
                    }
                    return View(model);
                }
                else
                    return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
        #endregion

        #region Categories

        //Recupero dei dati relativi alle categorie
        [Authorize]
        public ActionResult Categories()
        {
            try
            {
                IList<CategoryViewModel> categoryList = null;
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
                return View(categoryList);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        //Recupera i dettagli della singola categoria
        [Authorize]
        public ActionResult CategoryDetail(int id)
        {
            try
            {
                CategoryViewModel cvm = new CategoryViewModel();
                using (UnitOfWork uow = new UnitOfWork())
                {
                    CategoryRepository cr = new CategoryRepository(uow.Current);
                    Category c = cr.GetById(id);
                    if (c != null)
                    {
                        cvm.Nome = c.Name;
                        cvm.Descrizione = c.Description;
                        cvm.Id = c.Id;
                        cvm.FileName = c.ImageName;
                    }
                }
                return View(cvm);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        //Modifica i dettagli di una categoria selezionata
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryDetail(CategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
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
                                model.FileName = c.ImageName;

                            cr.SaveOrUpdate(c);
                            uow.Commit();

                            model.Message = "Modifica eseguita con successo!";
                        }
                        else
                            model.Message = "Si è verificato un errore durante l'aggiornamento dei dati! Recupero dell'entità da modificare non avvenuto!";
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
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
            try
            {
                if (ModelState.IsValid)
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
                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [Authorize]
        public ActionResult CategoryRemoveImage(int id)
        {
            try
            {
                CategoryViewModel cvm = new CategoryViewModel();
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
                        return Error("Si è verificato un errore durante la rimozione dell'immagine");
                    }
                }
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
        #endregion

        #region Tags

        //Recupero dei dati relativi alle categorie
        [Authorize]
        public ActionResult Tags()
        {
            try
            {
                IList<TagViewModel> tagList = null;
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
                return View(tagList);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        //Recupera i dettagli della singola categoria
        [Authorize]
        public ActionResult TagDetail(int id)
        {
            try
            {
                TagViewModel cvm = new TagViewModel();
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
                return View(cvm);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        //Modifica i dettagli di una categoria selezionata
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult TagDetail(TagViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
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
                        }
                        else
                            model.Message = "Si è verificato un errore durante l'aggiornamento dei dati! Recupero entità da modifica non avvenuto con successo!";
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
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
            try
            {
                if (ModelState.IsValid)
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
                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        #endregion

        #region Pages

        //Recupero dei dati relativi alle categorie
        [Authorize]
        public ActionResult Pages()
        {
            try
            {
                IList<PageViewModel> postList = null;
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
                            pvm.Autore = p.Author.NameAndSurname;
                            pvm.Categoria = p.Category.Name;

                            postList.Add(pvm);
                        }
                    }
                }
                return View(postList);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        //Recupera i dettagli della singola categoria
        [Authorize]
        public ActionResult PageDetail(int id)
        {
            try
            {
                PageViewModel pvm = new PageViewModel();
                using (UnitOfWork uow = new UnitOfWork())
                {
                    PageRepository pr = new PageRepository(uow.Current);
                    AuthorRepository ar = new AuthorRepository(uow.Current);
                    BlogRepository br = new BlogRepository(uow.Current);
                    CategoryRepository cr = new CategoryRepository(uow.Current);

                    Page p = pr.GetById(id);
                    if (p != null)
                    {
                        IList<Author> tmpAuthors = ar.FindAll().ToList();
                        if (tmpAuthors != null && tmpAuthors.Count > 0)
                        {
                            IEnumerable<SelectListItem> tmpAuthorsItems;

                            tmpAuthorsItems =   from s in tmpAuthors
                                                select new SelectListItem
                                                {
                                                    Text = s.NameAndSurname,
                                                    Value = s.Id.ToString()
                                                };

                            pvm.Authors = tmpAuthorsItems;
                            pvm.SelectedAuthor = p.Author.Id.ToString();
                        }
                        IList<Blog.Model.Domain.Entities.Blog> tmpBlogs = br.FindAll().ToList();
                        if (tmpBlogs != null && tmpBlogs.Count > 0)
                        {
                            IEnumerable<SelectListItem> tmpBlogsItems;

                            tmpBlogsItems = from b in tmpBlogs
                                              select new SelectListItem
                                              {
                                                  Text = b.Name,
                                                  Value = b.Id.ToString()
                                              };

                            pvm.Blogs = tmpBlogsItems;
                            pvm.SelectedBlog = p.Blog.Id.ToString();
                        }
                        IList<Category> tmpCategories = cr.FindAll().ToList();
                        if (tmpCategories != null && tmpCategories.Count > 0)
                        {
                            IEnumerable<SelectListItem> tmpCategoriesItems;

                            tmpCategoriesItems = from b in tmpCategories
                                            select new SelectListItem
                                            {
                                                Text = b.Name,
                                                Value = b.Id.ToString()
                                            };

                            pvm.Categories = tmpCategoriesItems;
                            pvm.SelectedCategory = p.Category.Id.ToString();
                        }

                        pvm.Data = p.Date.Value;
                        pvm.Id = p.Id;
                        pvm.Titolo = p.Title;
                        pvm.Descrizione = p.Description;
                        pvm.FileName = p.ImageName;
                        pvm.Body = p.BodyText;

                        if (p.Tags != null && p.Tags.Count > 0)
                            pvm.Tags = String.Join(", ", p.Tags.Select(x=>x.Name));
                    }
                }
                return View(pvm);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        //Modifica i dettagli di una pagina selezionata
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PageDetail(PageViewModel model)
        {
            try
            {
                //Carica tutti gli elementi necessari a video
                using (UnitOfWork uow = new UnitOfWork())
                {
                    AuthorRepository ar = new AuthorRepository(uow.Current);
                    BlogRepository br = new BlogRepository(uow.Current);
                    CategoryRepository cr = new CategoryRepository(uow.Current);

                    //Ricarica la lista autori
                    IList<Author> tmpAuthors = ar.FindAll().ToList();
                    if (tmpAuthors != null && tmpAuthors.Count > 0)
                    {
                        IEnumerable<SelectListItem> tmpAuthorsItems;

                        tmpAuthorsItems = from s in tmpAuthors
                                          select new SelectListItem
                                          {
                                              Text = s.NameAndSurname,
                                              Value = s.Id.ToString()
                                          };

                        model.Authors = tmpAuthorsItems;
                    }

                    IList<Blog.Model.Domain.Entities.Blog> tmpBlogs = br.FindAll().ToList();
                    if (tmpBlogs != null && tmpBlogs.Count > 0)
                    {
                        IEnumerable<SelectListItem> tmpBlogsItems;

                        tmpBlogsItems = from b in tmpBlogs
                                        select new SelectListItem
                                        {
                                            Text = b.Name,
                                            Value = b.Id.ToString()
                                        };

                        model.Blogs = tmpBlogsItems;
                    }

                    IList<Category> tmpCategories = cr.FindAll().ToList();
                    if (tmpCategories != null && tmpCategories.Count > 0)
                    {
                        IEnumerable<SelectListItem> tmpCategoriesItems;

                        tmpCategoriesItems = from b in tmpCategories
                                             select new SelectListItem
                                             {
                                                 Text = b.Name,
                                                 Value = b.Id.ToString()
                                             };

                        model.Categories = tmpCategoriesItems;
                    }
                }
                if (ModelState.IsValid)
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

                        model.SelectedAuthor = model.SelectedAuthor;
                        model.SelectedBlog = model.SelectedBlog;
                        model.SelectedCategory = model.SelectedCategory;

                        PageRepository pr = new PageRepository(uow.Current);
                        AuthorRepository ar = new AuthorRepository(uow.Current);
                        BlogRepository br = new BlogRepository(uow.Current);
                        CategoryRepository cr = new CategoryRepository(uow.Current);
                        TagRepository tr = new TagRepository(uow.Current);

                        Page p = pr.GetById(model.Id);
                        if (p != null)
                        {
                            Author au = ar.GetById(Convert.ToInt32(model.SelectedAuthor));
                            Blog.Model.Domain.Entities.Blog bb = br.GetById(Convert.ToInt32(model.SelectedBlog));
                            Category cc = cr.GetById(Convert.ToInt32(model.SelectedCategory));

                            p.Modifypage(model.Titolo, model.Descrizione, model.Data, model.Body, au, bb, cc);

                            if (!String.IsNullOrEmpty(fileName))
                                p.SetImagePath(fileName);
                            else
                                model.FileName = p.ImageName;

                            if (!String.IsNullOrEmpty(model.Tags))
                            {
                                foreach (var t in model.Tags.Split(','))
                                {
                                    if (!String.IsNullOrEmpty(t))
                                    {
                                        Tag tg = tr.GetTagByName(t.TrimStart().TrimEnd());
                                        if (tg != null)
                                        {
                                            p.AddTag(tg);
                                        }
                                        else
                                        {
                                            Tag tempTag = new Tag(t.TrimStart().TrimEnd());
                                            p.AddTag(tempTag);
                                        }
                                    }
                                }
                            }

                            pr.SaveOrUpdate(p);
                            uow.Commit();

                            model.Message = "Modifica eseguita con successo!";
                        }
                        else
                            model.Message = "Si è verificato un errore durante l'aggiornamento dei dati!";
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        //Modifica i dettagli di una pagina selezionata
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PageNew(PageViewModel model) 
        {
            try
            {
                //Carica tutti gli elementi necessari a video
                using (UnitOfWork uow = new UnitOfWork())
                {
                    AuthorRepository ar = new AuthorRepository(uow.Current);
                    BlogRepository br = new BlogRepository(uow.Current);
                    CategoryRepository cr = new CategoryRepository(uow.Current);

                    //Ricarica la lista autori
                    IList<Author> tmpAuthors = ar.FindAll().ToList();
                    if (tmpAuthors != null && tmpAuthors.Count > 0)
                    {
                        IEnumerable<SelectListItem> tmpAuthorsItems;

                        tmpAuthorsItems = from s in tmpAuthors
                                          select new SelectListItem
                                          {
                                              Text = s.NameAndSurname,
                                              Value = s.Id.ToString()
                                          };

                        model.Authors = tmpAuthorsItems;
                    }

                    IList<Blog.Model.Domain.Entities.Blog> tmpBlogs = br.FindAll().ToList();
                    if (tmpBlogs != null && tmpBlogs.Count > 0)
                    {
                        IEnumerable<SelectListItem> tmpBlogsItems;

                        tmpBlogsItems = from b in tmpBlogs
                                        select new SelectListItem
                                        {
                                            Text = b.Name,
                                            Value = b.Id.ToString()
                                        };

                        model.Blogs = tmpBlogsItems;
                    }

                    IList<Category> tmpCategories = cr.FindAll().ToList();
                    if (tmpCategories != null && tmpCategories.Count > 0)
                    {
                        IEnumerable<SelectListItem> tmpCategoriesItems;

                        tmpCategoriesItems = from b in tmpCategories
                                             select new SelectListItem
                                             {
                                                 Text = b.Name,
                                                 Value = b.Id.ToString()
                                             };

                        model.Categories = tmpCategoriesItems;
                    }
                }

                if (ModelState.IsValid)
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

                        model.SelectedAuthor = model.SelectedAuthor;
                        model.SelectedBlog = model.SelectedBlog;
                        model.SelectedCategory = model.SelectedCategory;

                        PageRepository pr = new PageRepository(uow.Current);
                        AuthorRepository ar = new AuthorRepository(uow.Current);
                        BlogRepository br = new BlogRepository(uow.Current);
                        CategoryRepository cr = new CategoryRepository(uow.Current);
                        TagRepository tr = new TagRepository(uow.Current);

                        Author au = ar.GetById(Convert.ToInt32(model.SelectedAuthor));
                        Blog.Model.Domain.Entities.Blog bb = br.GetById(Convert.ToInt32(model.SelectedBlog));
                        Category cc = cr.GetById(Convert.ToInt32(model.SelectedCategory));

                        Page p = new Page(model.Titolo, model.Descrizione, model.Data, model.Body, au, bb, cc);

                        if (!String.IsNullOrEmpty(model.Tags))
                        {
                            foreach(var t in model.Tags.Split(','))
                            {
                                if (!String.IsNullOrEmpty(t))
                                {
                                    Tag tg = tr.GetTagByName(t.TrimStart().TrimEnd());
                                    if (tg != null)
                                    {
                                        p.AddTag(tg);
                                    }
                                    else
                                    {
                                        Tag tempTag = new Tag(t.TrimStart().TrimEnd());
                                        p.AddTag(tempTag);
                                    }
                                }
                            }
                        }

                        if (!String.IsNullOrEmpty(fileName))
                            p.SetImagePath(fileName);
                        else
                            model.FileName = p.ImageName;

                        pr.SaveOrUpdate(p);
                        uow.Commit();

                        model.Message = "Salvataggio eseguito con successo!";
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [Authorize]
        public ActionResult PageNew()
        {
            try
            {
                PageViewModel pvm = new PageViewModel();
                using (UnitOfWork uow = new UnitOfWork())
                {
                    AuthorRepository ar = new AuthorRepository(uow.Current);
                    BlogRepository br = new BlogRepository(uow.Current);
                    CategoryRepository cr = new CategoryRepository(uow.Current);

                    //Ricarica la lista autori
                    IList<Author> tmpAuthors = ar.FindAll().ToList();
                    if (tmpAuthors != null && tmpAuthors.Count > 0)
                    {
                        IEnumerable<SelectListItem> tmpAuthorsItems;

                        tmpAuthorsItems = from s in tmpAuthors
                                          select new SelectListItem
                                          {
                                              Text = s.NameAndSurname,
                                              Value = s.Id.ToString()
                                          };

                        pvm.Authors = tmpAuthorsItems;

                    }

                    IList<Blog.Model.Domain.Entities.Blog> tmpBlogs = br.FindAll().ToList();
                    if (tmpBlogs != null && tmpBlogs.Count > 0)
                    {
                        IEnumerable<SelectListItem> tmpBlogsItems;

                        tmpBlogsItems = from b in tmpBlogs
                                        select new SelectListItem
                                        {
                                            Text = b.Name,
                                            Value = b.Id.ToString()
                                        };

                        pvm.Blogs = tmpBlogsItems;
                    }

                    IList<Category> tmpCategories = cr.FindAll().ToList();
                    if (tmpCategories != null && tmpCategories.Count > 0)
                    {
                        IEnumerable<SelectListItem> tmpCategoriesItems;

                        tmpCategoriesItems = from b in tmpCategories
                                             select new SelectListItem
                                             {
                                                 Text = b.Name,
                                                 Value = b.Id.ToString()
                                             };

                        pvm.Categories = tmpCategoriesItems;
                    }

                    pvm.Data = DateTime.Today.Date;
                }
                return View(pvm);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [Authorize]
        public ActionResult PageRemoveImage(int id)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    PageRepository pr = new PageRepository(uow.Current);

                    Page p = pr.GetById(id);

                    if (p != null)
                    {
                        p.SetImagePath(null);

                        pr.SaveOrUpdate(p);
                        uow.Commit();

                        return RedirectToAction("PageDetail", new { id = p.Id });
                    }
                    else
                    {
                        return Error("Si è verificato un errore durante la rimozione dell'immagine");
                    }
                }
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        //[HttpGet]
        //public ActionResult LookupTags(string q, int limit)
        //{
        //    // We can get a list of tags from the database, but 
        //    // for this example, I will populate a list with values.
        //    List<string> tags = new List<string>();
        //    tags.Add("asp");
        //    tags.Add("mvc");
        //    tags.Add("microsoft");
        //    tags.Add("sql server");
        //    tags.Add("jQuery");
        //    tags.Add("ajax");
  
        //    // Select the tags that match the query, and get the 
        //    // number or tags specified by the limit.
        //    var retValue = tags
        //        .Where(x => x.StartsWith(q))
        //        .OrderBy(x => x)
        //        .Take(limit)
        //        .Select(r => new { Tag = r
        //    });

        //    // Return the result set as JSON
        //    return Json(retValue);
        //}

        [HttpGet]
        public ActionResult LookupTags(string term)
        {
            try
            {
                IList<string> retValue = null;

                using (UnitOfWork uow = new UnitOfWork())
                {
                    TagRepository tr = new TagRepository(uow.Current);

                    retValue = tr.FindAll().Where(x => x.Name.StartsWith(term))
                    .OrderBy(x => x)
                    //.Take(limit)
                    .Select(r => r.Name).ToList();
                }

                // Return the result set as JSON
                return Json(retValue, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }

            
        }

        #endregion

        public ActionResult UploadPartial()
        {
            var appData = Server.MapPath("~/Uploads");
            var images = Directory.GetFiles(appData).Select(x => new ImagesViewModel
            {
                Url = Url.Content("/Uploads/" + Path.GetFileName(x))
            });
            return View(images);
        }

        public void UploadNow(HttpPostedFileWrapper upload)
        {
            if (upload != null)
            {
                string ImageName = upload.FileName;
                string path = System.IO.Path.Combine(Server.MapPath("~/Uploads"), ImageName);
                upload.SaveAs(path);
            }

        }

    }
}
