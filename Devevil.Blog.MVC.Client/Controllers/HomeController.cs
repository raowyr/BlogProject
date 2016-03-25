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
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        [AllowAnonymous]
        public ActionResult Index()
        {
            try 
            {
                HomePageViewModel m = new HomePageViewModel();
                using (UnitOfWork uow = new UnitOfWork())
                {
                    //Carica gli ultimi 10 POST
                    PageRepository pr = new PageRepository(uow.Current);

                    IList<Page> pages = pr.GetTopPost(10);
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

                            if (k < 5)
                                m.PostDetail.Add(pTemp);
                            else
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

                        m.PostDetail.Add(pTemp);
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
                //Errore durante recupero dei dati
                //Errore gestibile, non è necessario reindirizzare alla pagine di errore
                //PostViewModel pTemp = new PostViewModel();

                //pTemp.Id = 0;
                //pTemp.Data = DateTime.Today;
                //pTemp.Titolo = "OOPS...";
                //pTemp.Testo = "Sembra non siano presenti articoli...";
                //pTemp.Autore = "Pasquale Garzillo";

                //m.PostDetail.Add(pTemp);

                //CategoryViewModel cvTemp = new CategoryViewModel();

                //cvTemp.Id = 0;
                //cvTemp.Nome = "OOPS...";
                //cvTemp.Descrizione = "Sembra non siano presenti categorie...";

                //m.CategoriesPreview.Add(cvTemp);
                return Error(ex.Message);
            }
        }
    }
}
