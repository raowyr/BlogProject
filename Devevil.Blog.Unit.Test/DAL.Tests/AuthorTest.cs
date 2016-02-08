using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Model.Domain.Entities;
using Devevil.Blog.Nhibernate.DAL;
using Devevil.Blog.Nhibernate.DAL.Base;
using Devevil.Blog.Nhibernate.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Devevil.Blog.Unit.Test.DAL.Tests
{
    [TestClass]
    public class AuthorTest
    {
        Blog.Model.Domain.Entities.Blog b;
        [TestInitialize]
        public void Start()
        {
            //Inizializza Nhibernate
            SessionManager.Instance.Configure();
            SessionManager.Instance.BuildSchema();
            //Log su standard output delle query eseguite da Nhibernate
            log4net.Config.XmlConfigurator.Configure();

            b = new Blog.Model.Domain.Entities.Blog("Blog test", "test");
        }

        [TestCleanup]
        public void Stop()
        {
            SessionManager.Instance.Close();
        }

        [TestMethod]
        public void CreateNewAuthorTest()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                AuthorRepository ar = new AuthorRepository(uow.Current);
                BlogRepository br = new BlogRepository(uow.Current);

                br.Save(b);
                Author a = new Author("Pasquale", "Garzillo", Convert.ToDateTime("27/12/1987"), "pgarzillo@sdn-napoli.it", true, "rofox2011", b);

                ar.Save(a);

                uow.Commit();
            }
        }
    }
}
