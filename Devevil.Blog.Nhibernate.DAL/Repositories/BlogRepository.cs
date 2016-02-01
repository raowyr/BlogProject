using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Model.Domain.Entities;
using Devevil.Blog.Nhibernate.DAL.Base;
using NHibernate;


namespace Devevil.Blog.Nhibernate.DAL.Repositories
{
    public class BlogRepository : GenericRepository<Devevil.Blog.Model.Domain.Entities.Blog>
    {
        public BlogRepository(ISession session) : base(session) { }
    }
}
