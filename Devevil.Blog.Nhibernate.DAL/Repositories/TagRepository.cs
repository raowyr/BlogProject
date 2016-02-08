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
    public class TagRepository : GenericRepositoryWithoutDelete<Tag>
    {
        public TagRepository(ISession session) : base(session) { }

    }
}
