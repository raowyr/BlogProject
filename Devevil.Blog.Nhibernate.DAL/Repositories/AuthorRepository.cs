using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Model.Domain.Entities;
using Devevil.Blog.Nhibernate.DAL.Base;
using NHibernate;
using NHibernate.Linq;

namespace Devevil.Blog.Nhibernate.DAL.Repositories
{
    public class AuthorRepository : GenericRepositoryWithoutDelete<Author>
    {
        public AuthorRepository(ISession session) : base(session) { }

        public Author GetAuthorByEmail(string prmMail)
        {
            var linq = (from aa in Session.Query<Author>()
                        where aa.Email == prmMail
                        select aa);
            return linq.FirstOrDefault();
        }
    }
}
