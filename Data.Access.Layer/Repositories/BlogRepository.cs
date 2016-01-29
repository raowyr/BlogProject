using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Entities;
using Data.Access.Layer.Base;
using NHibernate;

namespace Data.Access.Layer.Repositories
{
    public class BlogRepository : GenericRepository<Blog>
    {
        public BlogRepository(ISession session) : base(session) { }
    }
}
