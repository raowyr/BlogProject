using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Entities;
using FluentNHibernate.Mapping;

namespace Data.Access.Layer.Mapping
{
    public class CommentMap : ClassMap<Comment>
    {
        public CommentMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.UserName).Length(255).Not.Nullable();
            Map(x => x.UserMail).Length(255).Not.Nullable();
            Map(x => x.TextComment).Not.Nullable();
            References(x => x.Page).Not.Nullable();
        }
    }
}
