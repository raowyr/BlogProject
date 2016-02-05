using System;
using Devevil.Blog.Infrastructure.Core.Entities.Exception;
using Devevil.Blog.Model.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Devevil.Blog.Unit.Test.Model.Entities
{
    [TestClass]
    public class TagTest
    {
        [TestMethod]
        public void TagValidStateTest()
        {
            Tag a = new Tag("C#");
            Assert.IsTrue(true);
        }

        //[TestMethod]
        //public void TagInvalidStateNameTest()
        //{
        //    try
        //    {
        //        Tag a = new Tag();

        //        a.ValidateState();
        //    }
        //    catch (EntityInvalidStateException ex)
        //    {
        //        Assert.IsTrue(true);
        //    }
        //}
    }
}
