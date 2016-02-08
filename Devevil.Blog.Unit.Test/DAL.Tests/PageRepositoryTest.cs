using System;
using Devevil.Blog.Model.Domain.Entities;
using Devevil.Blog.Nhibernate.DAL;
using Devevil.Blog.Nhibernate.DAL.Base;
using Devevil.Blog.Nhibernate.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Devevil.Blog.Unit.Test.DAL.Tests
{
    [TestClass]
    public class PageRepositoryTest
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

        [TestMethod]
        public void CreateNewPageSuccessfulTest()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                PageRepository pr = new PageRepository(uow.Current);
                BlogRepository br = new BlogRepository(uow.Current);

                Blog.Model.Domain.Entities.Blog b = new Blog.Model.Domain.Entities.Blog("Nombe blog", "Desc blog");

                Category c = new Category("Categoria 1", "Desc 1");
                Author a = new Author("Nome autore", "Cognome autore", Convert.ToDateTime("27/12/1987"), "pgarz@sdn-napoli.it", true, "pass", b);

                Page p = new Page("Nome pagina", "descr pagine", DateTime.Now, "test", a, b, c);

                br.Save(b);
                pr.Save(p);

                uow.Commit();
            }
        }
    }
}
