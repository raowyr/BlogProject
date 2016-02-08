using System;
using Devevil.Blog.Model.Domain.Entities;
using Devevil.Blog.Nhibernate.DAL;
using Devevil.Blog.Nhibernate.DAL.Base;
using Devevil.Blog.Nhibernate.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Devevil.Blog.Unit.Test.DAL.Tests
{
    [TestClass]
    public class TagRepositoryTest
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
        public void CreateNewTagSuccessfulTest()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                TagRepository tr = new TagRepository(uow.Current);
                Tag c = new Tag("sql server");

                tr.Save(c);

                uow.Commit();
            }
        }
    }
}
