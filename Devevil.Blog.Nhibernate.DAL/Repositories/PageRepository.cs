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
    public class PageRepository : GenericRepositoryWithoutDelete<Page>
    {
        public PageRepository(ISession session) : base(session) { }

        public IList<Page> GetTopPost(int prmNPost)
        {
            var linq = (from pag in Session.Query<Page>()
                        orderby pag.Date descending
                        select pag)
                        .Take(prmNPost);
            return linq.ToList();
        }
    }
}
