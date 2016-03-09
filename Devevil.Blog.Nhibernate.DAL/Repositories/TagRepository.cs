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
    public class TagRepository : GenericRepositoryWithoutDelete<Tag>
    {
        public TagRepository(ISession session) : base(session) { }

        public Tag GetTagByName(string prmName)
        {
            var linq = (from t in Session.Query<Tag>()
                        where t.Name.StartsWith(prmName)
                        select t).FirstOrDefault();
            return linq;
        }
    }
}
