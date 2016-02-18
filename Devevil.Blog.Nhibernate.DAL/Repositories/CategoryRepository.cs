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
    public class CategoryRepository : GenericRepositoryWithoutDelete<Category>
    {
        public CategoryRepository(ISession session) : base(session) { }

        public IList<Category> GetTopCategoryByPostCount(int prmNCategory)
        {
            var linq = (from cat in Session.Query<Category>()
                        orderby cat.Pages.Count descending
                        select cat)
                        .Take(prmNCategory);
            return linq.ToList();
        }
    }
}
