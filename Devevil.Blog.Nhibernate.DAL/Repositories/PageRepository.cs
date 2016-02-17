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

        public IList<Page> GetTop5Post()
        {
            var linq = (from pag in Session.Query<Page>()
                        where pag.Date.Value.Year == DateTime.Today.Year
                        orderby pag.Date descending
                        select pag)
                        .Take(5);
            return linq.ToList();
        }
    }
}
