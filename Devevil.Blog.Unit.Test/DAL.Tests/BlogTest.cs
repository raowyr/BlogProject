using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Devevil.Blog.Nhibernate.DAL;
using Devevil.Blog.Nhibernate.DAL.Base;
using Devevil.Blog.Nhibernate.DAL.Repositories;
using Devevil.Blog.Model.Domain.Entities;

namespace Devevil.Blog.Unit.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Start()
        {
            //Inizializza Nhibernate
            SessionManager.Instance.Configure();
            SessionManager.Instance.BuildSchema();
            //Log su standard output delle query eseguite da Nhibernate
            log4net.Config.XmlConfigurator.Configure();
        }

        [TestCleanup]
        public void Stop()
        {
            SessionManager.Instance.Close();
        }

        /// <summary>
        /// Creazione di un Blog con una pagina di default associata.
        /// Una Pagina deve necessariamente avere entità associate, quali:
        /// Un Autore
        /// Una Categoria di appartenenza
        /// (Eventualmente uno o più tag descrittivi per i contenuti della pagina)
        /// (Eventualmente uno più commenti associati alla pagina)
        /// </summary>
        [TestMethod]
        public void CreateSuccessfulBlogTest()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                BlogRepository br = new BlogRepository(uow.Current);
                Devevil.Blog.Model.Domain.Entities.Blog b = new Devevil.Blog.Model.Domain.Entities.Blog();
                b.Name = ".Net Help";
                b.Description = "Un blog dedicato allo sviluppo in ambiente .NET. Tanti articoli, tips and trick.";

                Author a = new Author();
                a.Name = "Pasquale";
                a.Surname = "Garzillo";
                a.BirthDate = Convert.ToDateTime("27/12/1987");
                a.Email = "prova@prova.it";

                Tag t = new Tag();
                t.Name = "c#";

                Category c = new Category();
                c.Name = "Generica";
                c.Description = "Categoria generica";

                Comment co = new Comment();
                co.UserName = "raowyr";
                co.UserMail = "prova@prova.it";
                co.TextComment = "commento di prova";

                Page p = new Page();
                p.Title = "Prima pagina del blog";
                p.Description = "Descrizione della prima pagina";
                p.Date = DateTime.Today;
                p.BodyText = "testo testo teso";
                p.Author = a;
                p.AddTag(t);
                p.Category = c;
                p.AddComment(co);

                b.AddPage(p);

                br.Save(b);

                uow.Commit();

                Devevil.Blog.Model.Domain.Entities.Blog bb = br.Load(b.Id);

                Assert.IsNotNull(bb);
            }
        }

        /// <summary>
        /// Creazione di un blog senza pagina di default associata.
        /// </summary>
        [TestMethod]
        public void CreateSuccessfulBlogWithoutPageTest()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                BlogRepository br = new BlogRepository(uow.Current);
                Devevil.Blog.Model.Domain.Entities.Blog b = new Devevil.Blog.Model.Domain.Entities.Blog();
                b.Name = ".Net Help";
                b.Description = "Un blog dedicato allo sviluppo in ambiente .NET. Tanti articoli, tips and trick.";

                br.Save(b);

                uow.Commit();
            }
        }
    }
}
