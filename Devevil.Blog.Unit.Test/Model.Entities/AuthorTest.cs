using Devevil.Blog.Model.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devevil.Blog.Unit.Test.Model.Entities
{
    [TestClass]
    public class AuthorTest
    {
        [TestMethod]
        public void AuthorValidStateTest()
        {
            Author a = new Author();
            a.Name = "Pasquale";
            a.Surname = "Garzillo";
            a.BirthDate = Convert.ToDateTime("27/12/1987");
            a.Email = "pasquale.garzillo@gmail.com";

            a.ValidateState();
        }
    }
}
