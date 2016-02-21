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
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            HomePageViewModel m = new HomePageViewModel();

            try {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    //Carica gli ultimi 5 POST
                    PageRepository pr = new PageRepository(uow.Current);
                    IList<Page> pages = pr.GetTopPost(5);
                    if (pages != null)
                    {
                        if (pages.Count > 0)
                        {
                            int k = 0;
                            foreach (var p in pages)
                            {
                                PostViewModel pTemp = new PostViewModel();

                                pTemp.Id = p.Id;
                                pTemp.Data = p.Date.Value;
                                pTemp.Testo = p.BodyText;
                                pTemp.Titolo = p.Title;

                                if (k == 0)
                                    m.PostDetail.Add(pTemp);
                                else if (k == 1)
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
                            pTemp.Testo = "Non è presente alcun articolo!";
                            pTemp.Titolo = "Non sono presenti articoli";

                            m.PostDetail.Add(pTemp);
                        }
                    }
                    //Carica le ultime due categoria con maggiori post
                    CategoryRepository cr = new CategoryRepository(uow.Current);
                    IList<Category> tempCats = cr.GetTopCategoryByPostCount(2);
                    if (tempCats != null && tempCats.Count>0)
                    {
                        foreach (var c in tempCats)
                        {
                            CategoryViewModel cvTemp = new CategoryViewModel();

                            cvTemp.Id = c.Id;
                            cvTemp.CategoryName = c.Name;
                            cvTemp.CategoryDescription = c.Description;

                            m.CategoriesPreview.Add(cvTemp);
                        }
                    }
                }
            } catch (Exception ex)
            {
                //Errore durante recupero dei dati
                PostViewModel pTemp = new PostViewModel();

                pTemp.Id = 0;
                pTemp.Data = DateTime.Today;
                pTemp.Testo = "Non è presente alcun articolo!";
                pTemp.Titolo = "Non sono presenti articoli";

                m.PostDetail.Add(pTemp);
            }
            
            m.Aforisma = GetAforismaRandom();

            return View(m);
        }

        private string GetAforismaRandom()
        {
            return "Aforsima";
        }

    }
}
