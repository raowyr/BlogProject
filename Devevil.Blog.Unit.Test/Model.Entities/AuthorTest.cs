using Devevil.Blog.Model.Domain.Entities;
using Devevil.Blog.Model.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Infrastructure.Core.Entities.Exception;

namespace Devevil.Blog.Unit.Test.Model.Entities
{
    [TestClass]
    public class AuthorTest
    {
        private Blog.Model.Domain.Entities.Blog b;

        [TestInitialize]
        public void Init()
        {
            b = new Blog.Model.Domain.Entities.Blog("Blog test", "test");
        }

        [TestMethod]
        public void AuthorValidStateTest()
        {
            try
            {
                Author a = new Author("Pasquale", "Garzillo", Convert.ToDateTime("27/12/1987"), "pgarzillo@sdn-napoli.it", true, "rofox2011", b);
            }
            catch (EntityInvalidStateException ex)
            {
                Assert.Fail();
            }
            catch (AuthorBadMailException bex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void AuthorInvalidStateNameTest()
        {
            try
            {
                Author a = new Author("", "Garzillo", Convert.ToDateTime("27/12/1987"), "pgarzillo@sdn-napoli.it", true, "rofox2011",b);
                Assert.Fail();
            }
            catch (EntityInvalidStateException ex)
            {
                //OK, Assert true
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void AuthorInvalidStateSurnameTest()
        {
            try
            {
                Author a = new Author("Pasquale", "", Convert.ToDateTime("27/12/1987"), "pgarzillo@sdn-napoli.it", true, "rofox2011",b);
                Assert.Fail();
            }
            catch (EntityInvalidStateException ex)
            {
                //OK, Assert true
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void AuthorInvalidStateEmailTest()
        {
            try
            {
                Author a = new Author("Pasquale", "Garzillo", Convert.ToDateTime("27/12/1987"), "", true, "rofox2011",b);
                Assert.Fail();
            }
            catch (AuthorBadMailException ex)
            {
                //OK, Assert true
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void AuthorInvalidFormatEmailTest()
        {
            try
            {
                Author a = new Author("Pasquale", "Garzillo", Convert.ToDateTime("27/12/1987"), "@.it", true, "rofox2011",b);
                Assert.Fail();
            }
            catch (AuthorBadMailException ex)
            {
                //OK, Assert true
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void AuthorInvalidStatePasswordTest()
        {
            try
            {
                Author a = new Author("Pasquale", "Garzillo", Convert.ToDateTime("27/12/1987"), "google@google.it", true, "",b);
                Assert.Fail();
            }
            catch (EntityInvalidStateException ex)
            {
                //OK, Assert true
                Assert.IsTrue(true);
            }
        }
    }
}
