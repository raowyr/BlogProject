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
        [TestMethod]
        public void AuthorValidStateTest()
        {
            //Author a = new Author();
            //a.Name = "Pasquale";
            //a.Surname = "Garzillo";
            //a.BirthDate = Convert.ToDateTime("27/12/1987");
            //a.Email = "pasquale.garzillo@gmail.com";

            //a.ValidateState();
            //Assert.IsTrue(true);
        }

        [TestMethod]
        public void AuthorInvalidStateNameTest()
        {
            try
            {
                //Author a = new Author();
                //a.Surname = "Garzillo";
                //a.BirthDate = Convert.ToDateTime("27/12/1987");
                //a.Email = "pasquale.garzillo@gmail.com";
                //a.ValidateState();

                //Assert.Fail();
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
                //Author a = new Author();
                //a.Name = "Pasquale";
                //a.BirthDate = Convert.ToDateTime("27/12/1987");
                //a.Email = "pasquale.garzillo@gmail.com";
                //a.ValidateState();

                //Assert.Fail();
            }
            catch (EntityInvalidStateException ex)
            {
                //OK, Assert true
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void AuthorInvalidStateBithDateTest()
        {
            try
            {
                //Author a = new Author();
                //a.Name = "Pasquale";
                //a.Surname = "Garzillo";
                //a.Email = "pasquale.garzillo@gmail.com";

                //a.ValidateState();
                //Assert.Fail();
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
                //Author a = new Author();
                //a.Name = "Pasquale";
                //a.Surname = "Garzillo";
                //a.BirthDate = Convert.ToDateTime("27/12/1987");
                //a.ValidateState();
                //Assert.Fail();
            }
            catch (EntityInvalidStateException ex)
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
                //Author a = new Author();
                //a.Name = "Pasquale";
                //a.Surname = "Garzillo";
                //a.BirthDate = Convert.ToDateTime("27/12/1987");
                //a.Email = "@";
                //a.ValidateState();
                //Assert.Fail();
            }
            catch (AuthorBadMailException ex)
            {
                //OK, Assert true
                Assert.IsTrue(true);
            }
        }
    }
}
