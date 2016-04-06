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
    public class CategoryController : BaseController
    {
        //
        // GET: /Category/

        public ActionResult Index()
        {
            try
            {
                ListCategoryViewModel m = new ListCategoryViewModel();
                using (UnitOfWork uow = new UnitOfWork())
                {
                    //Carica gli ultimi 5 POST
                    PageRepository pr = new PageRepository(uow.Current);
                    CategoryRepository cr = new CategoryRepository(uow.Current);

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

                    IList<Category> categories = cr.FindAll().ToList();
                    if (categories != null && categories.Count > 0)
                    {
                        foreach (var c in categories)
                        {
                            CategoryViewModel cvm = new CategoryViewModel();
                            cvm.Descrizione = c.Description;
                            cvm.Nome = c.Name;
                            cvm.FileName = c.ImageName;
                            cvm.Id = c.Id;

                            m.Categories.Add(cvm);
                        }
                    }
                    else 
                    {
                        CategoryViewModel cvm = new CategoryViewModel();
                        cvm.Descrizione = "Sembra non siano presenti articoli...";
                        cvm.Nome = "OOPS...";

                        m.Categories.Add(cvm);
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
