using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Devevil.Blog.Model.Domain.Entities;
using Devevil.Blog.MVC.Client.Models;
using Devevil.Blog.Nhibernate.DAL.Base;
using Devevil.Blog.Nhibernate.DAL.Repositories;
using Sparc.TagCloud;

namespace Devevil.Blog.MVC.Client.Controllers
{
    public class PageController : BaseController
    {
        //
        // GET: /Page/

        public ActionResult Index(string page, int id)
        {
            try 
            {
                DetailPageViewModel m = new DetailPageViewModel();
                using (UnitOfWork uow = new UnitOfWork())
                {
                    //Carica gli ultimi 5 POST
                    PageRepository pr = new PageRepository(uow.Current);

                    IList<Page> pages = pr.GetTopPost(5);
                    if (pages != null && pages.Count>0)
                    {
                        int k = 0;
                        foreach (var p in pages)
                        {
                            PostViewModel pTemp = new PostViewModel();

                            pTemp.Id = p.Id;
                            pTemp.Data = p.Date.Value;
                            pTemp.Testo = p.BodyText;
                            pTemp.Titolo = p.Title;
                            pTemp.Autore = String.Format("{0} {1}", p.Author.Name, p.Author.Surname);
                            pTemp.Categoria = p.Category.Name;
                            pTemp.IdCategoria = p.Category.Id;
                            pTemp.ImageName = p.ImageName;
                            pTemp.Visualizzazioni = p.Views;
                            pTemp.NumComments = p.Comments.Count;

                            m.PostPreview.Add(pTemp);

                            k++;
                        }
                    }
                    else
                    {
                        PostViewModel pTemp = new PostViewModel();

                        pTemp.Id = 0;
                        pTemp.Data = DateTime.Today;
                        pTemp.Titolo = "OOPS...";
                        pTemp.Testo = "Sembra non siano presenti articoli...";
                        pTemp.Autore = "Pasquale Garzillo";

                        m.PostPreview.Add(pTemp);
                    }

                    if (id != 0)
                    {
                        Page p = pr.GetById(id);

                        p.AddView();
                        pr.SaveOrUpdate(p);
                        uow.Commit();

                        PostViewModel pTemp = new PostViewModel();
                        
                        pTemp.Id = p.Id;
                        pTemp.Data = p.Date.Value;
                        pTemp.Testo = p.BodyText;
                        pTemp.Titolo = p.Title;
                        pTemp.Autore = String.Format("{0} {1}", p.Author.Name, p.Author.Surname);
                        pTemp.Categoria = p.Category.Name;
                        pTemp.IdCategoria = p.Category.Id;
                        pTemp.ImageName = p.ImageName;
                        pTemp.Tags = p.Tags!=null && p.Tags.Count>0 ? p.Tags.Select(x => x.Name).ToList() : null;
                        pTemp.Visualizzazioni = p.Views;
                        pTemp.NumComments = p.Comments.Count;

                        if (pTemp.Tags != null && pTemp.Tags.Count > 0)
                        {
                            var tags = new TagCloudAnalyzer()
                                     .ComputeTagCloud(pTemp.Tags);
                            pTemp.TagCloud = tags;
                        }

                        if (p.Comments != null && p.Comments.Count > 0)
                        {
                            pTemp.Comments = (from c in p.Comments
                                              select new CommentViewModel() { UserMail = c.UserMail, TextComment = c.TextComment, UserName = c.UserName }).ToList<CommentViewModel>();
                        }


                        m.DetailedPost = pTemp;
                    }
                   
                    //Carica le ultime 5 categoria con maggiori post
                    CategoryRepository cr = new CategoryRepository(uow.Current);
                    IList<Category> tempCats = cr.GetTopCategoryByPostCount(3);
                    if (tempCats != null && tempCats.Count > 0)
                    {
                        foreach (var c in tempCats)
                        {
                            CategoryViewModel cvTemp = new CategoryViewModel();

                            cvTemp.Id = c.Id;
                            cvTemp.Nome = c.Name;
                            cvTemp.Descrizione = c.Description;
                            cvTemp.FileName = c.ImageName;

                            m.CategoriesPreview.Add(cvTemp);
                        }
                    }
                    else
                    {
                        CategoryViewModel cvTemp = new CategoryViewModel();

                        cvTemp.Id = 0;
                        cvTemp.Nome = "OOPS...";
                        cvTemp.Descrizione = "Sembra non siano presenti categorie...";

                        m.CategoriesPreview.Add(cvTemp);
                    }
                }
                return View(m);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        public ActionResult List(string category, int idCategory, int page)
        {
            if (idCategory != 0 && page > 0)
            {
                try
                {
                    ListPageViewModel m = new ListPageViewModel();
                    m.Page = page;
                    m.IdCategory = idCategory;
                    m.CategoryName = category;

                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        //Carica gli ultimi 5 POST
                        PageRepository pr = new PageRepository(uow.Current);

                        IList<Page> pages = pr.GetTopPost(5);
                        if (pages != null && pages.Count > 0)
                        {
                            int k = 0;
                            foreach (var p in pages)
                            {
                                PostViewModel pTemp = new PostViewModel();

                                pTemp.Id = p.Id;
                                pTemp.Data = p.Date.Value;
                                pTemp.Testo = p.BodyText;
                                pTemp.Titolo = p.Title;
                                pTemp.Autore = String.Format("{0} {1}", p.Author.Name, p.Author.Surname);
                                pTemp.Categoria = p.Category.Name;
                                pTemp.IdCategoria = p.Category.Id;
                                pTemp.ImageName = p.ImageName;
                                pTemp.Visualizzazioni = p.Views;
                                pTemp.NumComments = p.Comments.Count;

                                m.PostPreview.Add(pTemp);

                                k++;
                            }
                        }
                        else
                        {
                            PostViewModel pTemp = new PostViewModel();

                            pTemp.Id = 0;
                            pTemp.Data = DateTime.Today;
                            pTemp.Titolo = "OOPS...";
                            pTemp.Testo = "Sembra non siano presenti articoli...";
                            pTemp.Autore = "Pasquale Garzillo";

                            m.PostPreview.Add(pTemp);
                        }

                        //Carica le ultime 5 categoria con maggiori post
                        CategoryRepository cr = new CategoryRepository(uow.Current);
                        IList<Category> tempCats = cr.GetTopCategoryByPostCount(3);
                        if (tempCats != null && tempCats.Count > 0)
                        {
                            foreach (var c in tempCats)
                            {
                                CategoryViewModel cvTemp = new CategoryViewModel();

                                cvTemp.Id = c.Id;
                                cvTemp.Nome = c.Name;
                                cvTemp.Descrizione = c.Description;
                                cvTemp.FileName = c.ImageName;

                                m.CategoriesPreview.Add(cvTemp);
                            }
                        }
                        else
                        {
                            CategoryViewModel cvTemp = new CategoryViewModel();

                            cvTemp.Id = 0;
                            cvTemp.Nome = "OOPS...";
                            cvTemp.Descrizione = "Sembra non siano presenti categorie...";

                            m.CategoriesPreview.Add(cvTemp);
                        }


                        pages = pr.GetPostByCategoryOrderedAndPaginated(idCategory, page, 10);
                        int totalPages = pr.GetNumberOfPagesByCategory(idCategory);
                        if (pages != null && pages.Count > 0)
                        {
                            m.TotalPages = totalPages;
                            foreach (var p in pages)
                            {
                                PostViewModel pTemp = new PostViewModel();

                                pTemp.Id = p.Id;
                                pTemp.Data = p.Date.Value;
                                pTemp.Testo = p.BodyText;
                                pTemp.Titolo = p.Title;
                                pTemp.Autore = String.Format("{0} {1}", p.Author.Name, p.Author.Surname);
                                pTemp.Categoria = p.Category.Name;
                                pTemp.IdCategoria = p.Category.Id;
                                pTemp.ImageName = p.ImageName;
                                pTemp.Visualizzazioni = p.Views;
                                pTemp.NumComments = p.Comments.Count;

                                m.Posts.Add(pTemp);
                            }
                        }
                    }

                    return View(m);
                }
                catch (Exception ex)
                {
                    return Error(ex);
                }
            }
            else
                return Error("Errore durante la paginazione dei risultati!!!");
        }

        public ActionResult ListTopPages(int page)
        {
                try
                {
                    ListPageViewModel m = new ListPageViewModel();
                    m.Page = page;

                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        //Carica gli ultimi 5 POST
                        PageRepository pr = new PageRepository(uow.Current);

                        IList<Page> pages = pr.GetTopPost(5);
                        if (pages != null && pages.Count > 0)
                        {
                            int k = 0;
                            foreach (var p in pages)
                            {
                                PostViewModel pTemp = new PostViewModel();

                                pTemp.Id = p.Id;
                                pTemp.Data = p.Date.Value;
                                pTemp.Testo = p.BodyText;
                                pTemp.Titolo = p.Title;
                                pTemp.Autore = String.Format("{0} {1}", p.Author.Name, p.Author.Surname);
                                pTemp.Categoria = p.Category.Name;
                                pTemp.IdCategoria = p.Category.Id;
                                pTemp.ImageName = p.ImageName;
                                pTemp.Visualizzazioni = p.Views;
                                pTemp.NumComments = p.Comments.Count;

                                m.PostPreview.Add(pTemp);

                                k++;
                            }
                        }
                        else
                        {
                            PostViewModel pTemp = new PostViewModel();

                            pTemp.Id = 0;
                            pTemp.Data = DateTime.Today;
                            pTemp.Titolo = "OOPS...";
                            pTemp.Testo = "Sembra non siano presenti articoli...";
                            pTemp.Autore = "Pasquale Garzillo";

                            m.PostPreview.Add(pTemp);
                        }

                        //Carica le ultime 5 categoria con maggiori post
                        CategoryRepository cr = new CategoryRepository(uow.Current);
                        IList<Category> tempCats = cr.GetTopCategoryByPostCount(3);
                        if (tempCats != null && tempCats.Count > 0)
                        {
                            foreach (var c in tempCats)
                            {
                                CategoryViewModel cvTemp = new CategoryViewModel();

                                cvTemp.Id = c.Id;
                                cvTemp.Nome = c.Name;
                                cvTemp.Descrizione = c.Description;
                                cvTemp.FileName = c.ImageName;

                                m.CategoriesPreview.Add(cvTemp);
                            }
                        }
                        else
                        {
                            CategoryViewModel cvTemp = new CategoryViewModel();

                            cvTemp.Id = 0;
                            cvTemp.Nome = "OOPS...";
                            cvTemp.Descrizione = "Sembra non siano presenti categorie...";

                            m.CategoriesPreview.Add(cvTemp);
                        }


                        pages = pr.GetPostByViews(page, 10);
                        int totalPages = pr.GetNumberOfTotalPages();
                        if (pages != null && pages.Count > 0)
                        {
                            m.TotalPages = totalPages;
                            foreach (var p in pages)
                            {
                                PostViewModel pTemp = new PostViewModel();

                                pTemp.Id = p.Id;
                                pTemp.Data = p.Date.Value;
                                pTemp.Testo = p.BodyText;
                                pTemp.Titolo = p.Title;
                                pTemp.Autore = String.Format("{0} {1}", p.Author.Name, p.Author.Surname);
                                pTemp.Categoria = p.Category.Name;
                                pTemp.IdCategoria = p.Category.Id;
                                pTemp.ImageName = p.ImageName;
                                pTemp.Visualizzazioni = p.Views;
                                pTemp.NumComments = p.Comments.Count;

                                m.Posts.Add(pTemp);
                            }
                        }
                    }

                    return View(m);
                }
                catch (Exception ex)
                {
                    return Error(ex);
                }
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostComment(DetailPageViewModel model, FormCollection collection)
        {
            try
            {
                int pageId = Convert.ToInt32(collection["IdPagina"]);
                if (ModelState.IsValid)
                {
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        PageRepository pr = new PageRepository(uow.Current);
                        Page p = pr.GetById(pageId);

                        Comment c = new Comment(model.Comment.UserName, model.Comment.UserMail, model.Comment.TextComment, p);
                        p.AddComment(c);
                        uow.Commit();
                    }
                }

                DetailPageViewModel m = new DetailPageViewModel();
                using (UnitOfWork uow = new UnitOfWork())
                {
                    //Carica gli ultimi 5 POST
                    PageRepository pr = new PageRepository(uow.Current);

                    IList<Page> pages = pr.GetTopPost(5);
                    if (pages != null && pages.Count > 0)
                    {
                        int k = 0;
                        foreach (var p in pages)
                        {
                            PostViewModel pTemp = new PostViewModel();

                            pTemp.Id = p.Id;
                            pTemp.Data = p.Date.Value;
                            pTemp.Testo = p.BodyText;
                            pTemp.Titolo = p.Title;
                            pTemp.Autore = String.Format("{0} {1}", p.Author.Name, p.Author.Surname);
                            pTemp.Categoria = p.Category.Name;
                            pTemp.IdCategoria = p.Category.Id;
                            pTemp.ImageName = p.ImageName;
                            pTemp.Visualizzazioni = p.Views;
                            pTemp.NumComments = p.Comments.Count;

                            m.PostPreview.Add(pTemp);

                            k++;
                        }
                    }
                    else
                    {
                        PostViewModel pTemp = new PostViewModel();

                        pTemp.Id = 0;
                        pTemp.Data = DateTime.Today;
                        pTemp.Titolo = "OOPS...";
                        pTemp.Testo = "Sembra non siano presenti articoli...";
                        pTemp.Autore = "Pasquale Garzillo";

                        m.PostPreview.Add(pTemp);
                    }

                    if (pageId != 0)
                    {
                        Page p = pr.GetById(pageId);

                        PostViewModel pTemp = new PostViewModel();

                        pTemp.Id = p.Id;
                        pTemp.Data = p.Date.Value;
                        pTemp.Testo = p.BodyText;
                        pTemp.Titolo = p.Title;
                        pTemp.Autore = String.Format("{0} {1}", p.Author.Name, p.Author.Surname);
                        pTemp.Categoria = p.Category.Name;
                        pTemp.IdCategoria = p.Category.Id;
                        pTemp.ImageName = p.ImageName;
                        pTemp.Tags = p.Tags != null && p.Tags.Count > 0 ? p.Tags.Select(x => x.Name).ToList() : null;
                        pTemp.Visualizzazioni = p.Views;
                        pTemp.NumComments = p.Comments.Count;

                        if (pTemp.Tags != null && pTemp.Tags.Count > 0)
                        {
                            var tags = new TagCloudAnalyzer()
                                     .ComputeTagCloud(pTemp.Tags);
                            pTemp.TagCloud = tags;
                        }

                        if (p.Comments != null && p.Comments.Count > 0)
                        {
                            pTemp.Comments = (from c in p.Comments
                                              select new CommentViewModel() { UserMail = c.UserMail, TextComment = c.TextComment, UserName = c.UserName }).ToList<CommentViewModel>();
                        }


                        m.DetailedPost = pTemp;
                    }

                    //Carica le ultime 5 categoria con maggiori post
                    CategoryRepository cr = new CategoryRepository(uow.Current);
                    IList<Category> tempCats = cr.GetTopCategoryByPostCount(3);
                    if (tempCats != null && tempCats.Count > 0)
                    {
                        foreach (var c in tempCats)
                        {
                            CategoryViewModel cvTemp = new CategoryViewModel();

                            cvTemp.Id = c.Id;
                            cvTemp.Nome = c.Name;
                            cvTemp.Descrizione = c.Description;
                            cvTemp.FileName = c.ImageName;

                            m.CategoriesPreview.Add(cvTemp);
                        }
                    }
                    else
                    {
                        CategoryViewModel cvTemp = new CategoryViewModel();

                        cvTemp.Id = 0;
                        cvTemp.Nome = "OOPS...";
                        cvTemp.Descrizione = "Sembra non siano presenti categorie...";

                        m.CategoriesPreview.Add(cvTemp);
                    }
                }
                return View(m);

            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}
