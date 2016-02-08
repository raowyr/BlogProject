using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Devevil.Blog.Nhibernate.DAL;
using Devevil.Blog.Nhibernate.DAL.Base;
using Devevil.Blog.Nhibernate.DAL.Repositories;
using Devevil.Blog.Model.Domain.Entities;
using System.Linq;

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
                //Creo il blog
                Devevil.Blog.Model.Domain.Entities.Blog b = new Devevil.Blog.Model.Domain.Entities.Blog(".Net Help", "Un blog dedicato allo sviluppo");
                //creo una categoria generica
                Category c = new Category("Nessuna categoria", "Categoria generica");
                //Creo un paio di TAG
                Tag t1 = new Tag("C#");
                Tag t2 = new Tag(".NET");
                //Creo un autore, afferente al blog "b"
                Author a = new Author("Pasquale", "Garzillo", Convert.ToDateTime("27/12/1987"), "prova@prova.it",true,"rofox2011",b);
                //Creo una pagina afferente al blog "b", che ha per autore "a" ed appartiene alla categoria "c"
                Page p = new Page("Prima pagina del blog", "Descrizione della prima pagina", DateTime.Today, "testo pagina", a, b, c);
                //Creo un commento per la pagina "p"
                Comment co = new Comment("Raowyr", "raowyr@sdn-napoli.it", "Testo commento", p);
                
                //Aggiunto i tag "t1" e "t2" alla pagina
                p.AddTag(t1);
                p.AddTag(t2);
                //Aggiungo il commento "co" alla pagina
                p.AddComment(co);

                //Aggiungo la pagina "p" al blog "b"
                b.AddPageToBlog(p);
                //Aggiungo l'autore "a" al blog "b"
                b.AddAuthorToBlog(a);

                //La categoria "c" è categoria della pagina "p"
                c.AddCategoryToPage(p);
                //L'autore "a" è autore della pagina "p"
                a.AddAuthoringPage(p);
                //Tag "t1" e "t2" sono tag delle pagina "p"
                t1.AddTagToPage(p);
                t2.AddTagToPage(p);

                br.Save(b);

                uow.Commit();

                Devevil.Blog.Model.Domain.Entities.Blog bb = br.GetById(b.Id);

                Assert.IsNotNull(bb);
            }
        }

        [TestMethod]
        public void CreateSuccessfulBlogAndRemoveLogicallyPageTest()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                BlogRepository br = new BlogRepository(uow.Current);
                //Creo il blog
                Devevil.Blog.Model.Domain.Entities.Blog b = new Devevil.Blog.Model.Domain.Entities.Blog(".Net Help", "Un blog dedicato allo sviluppo");
                //creo una categoria generica
                Category c = new Category("Nessuna categoria", "Categoria generica");
                //Creo un paio di TAG
                Tag t1 = new Tag("C#");
                Tag t2 = new Tag(".NET");
                //Creo un autore, afferente al blog "b"
                Author a = new Author("Pasquale", "Garzillo", Convert.ToDateTime("27/12/1987"), "prova@prova.it", true, "rofox2011", b);
                //Creo una pagina afferente al blog "b", che ha per autore "a" ed appartiene alla categoria "c"
                Page p = new Page("Prima pagina del blog", "Descrizione della prima pagina", DateTime.Today, "testo pagina", a, b, c);
                //Creo un commento per la pagina "p"
                Comment co = new Comment("Raowyr", "raowyr@sdn-napoli.it", "Testo commento", p);

                //Aggiunto i tag "t1" e "t2" alla pagina
                p.AddTag(t1);
                p.AddTag(t2);
                //Aggiungo il commento "co" alla pagina
                p.AddComment(co);

                //Aggiungo la pagina "p" al blog "b"
                b.AddPageToBlog(p);
                //Aggiungo l'autore "a" al blog "b"
                b.AddAuthorToBlog(a);

                //La categoria "c" è categoria della pagina "p"
                c.AddCategoryToPage(p);
                //L'autore "a" è autore della pagina "p"
                a.AddAuthoringPage(p);
                //Tag "t1" e "t2" sono tag delle pagina "p"
                t1.AddTagToPage(p);
                t2.AddTagToPage(p);

                br.Save(b);

                uow.Commit();
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                BlogRepository br = new BlogRepository(uow.Current);

                Devevil.Blog.Model.Domain.Entities.Blog b = br.GetById(1);
                b.RemovePageFromBlog(b.Pages[0]);

                br.SaveOrUpdate(b);

                uow.Commit();

                Devevil.Blog.Model.Domain.Entities.Blog bb = br.GetById(1);

                Assert.IsTrue(bb.Pages.Where(x=>x.IsDeleted == true).Count()>0);
            }
        }

        [TestMethod]
        public void CreateSuccessfulBlogAndThenRemoveLogicallyItselfTest()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                BlogRepository br = new BlogRepository(uow.Current);
                //Creo il blog
                Devevil.Blog.Model.Domain.Entities.Blog b = new Devevil.Blog.Model.Domain.Entities.Blog(".Net Help", "Un blog dedicato allo sviluppo");
                //creo una categoria generica
                Category c = new Category("Nessuna categoria", "Categoria generica");
                //Creo un paio di TAG
                Tag t1 = new Tag("C#");
                Tag t2 = new Tag(".NET");
                //Creo un autore, afferente al blog "b"
                Author a = new Author("Pasquale", "Garzillo", Convert.ToDateTime("27/12/1987"), "prova@prova.it", true, "rofox2011", b);
                //Creo una pagina afferente al blog "b", che ha per autore "a" ed appartiene alla categoria "c"
                Page p = new Page("Prima pagina del blog", "Descrizione della prima pagina", DateTime.Today, "testo pagina", a, b, c);
                //Creo un commento per la pagina "p"
                Comment co = new Comment("Raowyr", "raowyr@sdn-napoli.it", "Testo commento", p);

                //Aggiunto i tag "t1" e "t2" alla pagina
                p.AddTag(t1);
                p.AddTag(t2);
                //Aggiungo il commento "co" alla pagina
                p.AddComment(co);

                //Aggiungo la pagina "p" al blog "b"
                b.AddPageToBlog(p);
                //Aggiungo l'autore "a" al blog "b"
                b.AddAuthorToBlog(a);

                //La categoria "c" è categoria della pagina "p"
                c.AddCategoryToPage(p);
                //L'autore "a" è autore della pagina "p"
                a.AddAuthoringPage(p);
                //Tag "t1" e "t2" sono tag delle pagina "p"
                t1.AddTagToPage(p);
                t2.AddTagToPage(p);

                br.Save(b);

                uow.Commit();
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                BlogRepository br = new BlogRepository(uow.Current);

                Devevil.Blog.Model.Domain.Entities.Blog b = br.GetById(1);
                b.DeleteBlog();

                //br.Delete(b);

                uow.Commit();

                Devevil.Blog.Model.Domain.Entities.Blog bb = br.GetById(1);

                Assert.IsTrue(bb.IsDeleted == true);
            }
        }
    }
}
