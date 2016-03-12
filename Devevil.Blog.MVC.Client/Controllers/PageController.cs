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

                        PostViewModel pTemp = new PostViewModel();

                        pTemp.Id = p.Id;
                        pTemp.Data = p.Date.Value;
                        pTemp.Testo = p.BodyText;
                        pTemp.Titolo = p.Title;
                        pTemp.Autore = String.Format("{0} {1}", p.Author.Name, p.Author.Surname);
                        pTemp.Categoria = p.Category.Name;
                        pTemp.IdCategoria = p.Category.Id;
                        pTemp.Tags = p.Tags!=null && p.Tags.Count>0 ? p.Tags.Select(x => x.Name).ToList() : null;

                        m.DetailedPost = pTemp;
                    }
                   
                    //Carica le ultime 5 categoria con maggiori post
                    CategoryRepository cr = new CategoryRepository(uow.Current);
                    IList<Category> tempCats = cr.GetTopCategoryByPostCount(5);
                    if (tempCats != null && tempCats.Count > 0)
                    {
                        foreach (var c in tempCats)
                        {
                            CategoryViewModel cvTemp = new CategoryViewModel();

                            cvTemp.Id = c.Id;
                            cvTemp.Nome = c.Name;
                            cvTemp.Descrizione = c.Description;

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
                return Error(ex.Message);
            }
        }

    }
}
