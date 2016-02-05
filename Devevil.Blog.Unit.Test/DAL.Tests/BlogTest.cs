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
                Devevil.Blog.Model.Domain.Entities.Blog b = new Devevil.Blog.Model.Domain.Entities.Blog(".Net Help", "Un blog dedicato allo sviluppo");

                Category c = new Category("Nessuna categoria", "Categoria generica");

                Tag t = new Tag("C#");

                Author a = new Author("Pasquale", "Garzillo", Convert.ToDateTime("27/12/1987"), "prova@prova.it",true,"rofox2011");

                Page p = new Page("Prima pagina del blog", "Descrizione della prima pagina", DateTime.Today, "testo pagina", a, b, c);

                Comment co = new Comment("Raowyr", "raowyr@sdn-napoli.it", "Testo commento", p);

                p.AddComment(co);
                b.AddPageToBlog(p);

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
                //BlogRepository br = new BlogRepository(uow.Current);
                //Devevil.Blog.Model.Domain.Entities.Blog b = new Devevil.Blog.Model.Domain.Entities.Blog();
                //b.Name = ".Net Help";
                //b.Description = "Un blog dedicato allo sviluppo in ambiente .NET. Tanti articoli, tips and trick.";

                //br.Save(b);

                //uow.Commit();
            }
        }
    }
}
