using System;
using Devevil.Blog.Model.Domain.Entities;
using Devevil.Blog.Nhibernate.DAL;
using Devevil.Blog.Nhibernate.DAL.Base;
using Devevil.Blog.Nhibernate.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Devevil.Blog.Unit.Test.DAL.Tests
{
    [TestClass]
    public class CategoryRepositoryTest
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
        public void CreateNewCategorySuccessfulTest()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                CategoryRepository cr = new CategoryRepository(uow.Current);
                Category c = new Category("Nome categoria di test", "descrizione di test");

                cr.Save(c);

                uow.Commit();
            }
        }
    }
}
