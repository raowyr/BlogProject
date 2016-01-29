using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Model.Entities;
using Data.Access.Layer;
using Data.Access.Layer.Base;
using Data.Access.Layer.Repositories;

namespace Blog.Unit.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                BlogRepository br = new BlogRepository(uow.Current);
                Domain.Model.Entities.Blog b = new Domain.Model.Entities.Blog();
                b.Name = "prova";
                b.Description = "prova";
                br.Save(b);
                uow.Commit();
            }
        }
    }
}
