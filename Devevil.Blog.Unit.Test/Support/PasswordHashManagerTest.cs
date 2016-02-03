using System;
using Devevil.Blog.Support.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Devevil.Blog.Unit.Test.Support
{
    [TestClass]
    public class PasswordHashManagerTest
    {
        [TestMethod]
        public void HashTest()
        {
            string pwd = PasswordHashManager.CreateHash("hashthis");
            Assert.IsTrue(PasswordHashManager.ValidatePassword("hashthis",pwd));
        }

        /// <summary>
        /// Un hash della stessa stringa è diverso dal momento che viene utilizzato un algoritmo SALTED
        /// </summary>
        [TestMethod]
        public void HashSameStringDifferentHashObtainedTest()
        {
            string pwd = PasswordHashManager.CreateHash("hashthis");
            string pwdSame = PasswordHashManager.CreateHash("hashthis");

            Assert.IsFalse(pwd == pwdSame);
        }
    }
}
