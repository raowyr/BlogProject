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

        public int GetNumberOfPagesByCategory(int prmIdCategory)
        {
            var linq = (from pag in Session.Query<Page>()
                        where pag.Category.Id == prmIdCategory
                        select pag).Count();

            return linq;
        }

        public int GetNumberOfTotalPages()
        {
            var linq = (from pag in Session.Query<Page>()
                        select pag).Count();

            return linq;
        }

        public IList<Page> GetPostByCategoryOrderedAndPaginated(int prmIdCategory, int prmStartRow, int prmPageSize)
        {
            var linq = (from pag in Session.Query<Page>()
                        where pag.Category.Id == prmIdCategory
                        orderby pag.Date descending
                        select pag)
                        .Skip((prmStartRow - 1) * prmPageSize)
                        .Take(prmPageSize);

            return linq.ToList();
        }

        public IList<Page> GetPostByViews(int prmStartRow, int prmPageSize)
        {
            var linq = (from pag in Session.Query<Page>()
                        orderby pag.Views descending
                        select pag)
                        .Skip((prmStartRow - 1) * prmPageSize)
                        .Take(prmPageSize);

            return linq.ToList();
        }

        public IList<Page> GetPostByQueryFind(string prmQuery)
        {
            IList<Page> toReturn = null;

            if (!String.IsNullOrEmpty(prmQuery))
            {
                foreach (var q in prmQuery.Split(' '))
                {
                    if (toReturn == null)
                        toReturn = new List<Page>();

                    if (!String.IsNullOrEmpty(q))
                    {
                        toReturn = toReturn.Union(from pag in Session.Query<Page>()
                                                  where pag.Title.Contains(q)
                                                  orderby pag.Views descending
                                                  select pag).ToList<Page>();
                    }
                }
            }

            return toReturn;
        }

    }
}
