using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Model.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Devevil.Blog.Nhibernate.DAL.Mapping
{
    public class CommentMap : ClassMap<Comment>
    {
        public CommentMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.UserName).Length(255).Not.Nullable();
            Map(x => x.UserMail).Length(255);
            Map(x => x.TextComment).Not.Nullable();
            References(x => x.Page).Not.Nullable();
        }
    }
}
