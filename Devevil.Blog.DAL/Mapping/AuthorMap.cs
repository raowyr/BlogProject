using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Devevil.Blog.Model.Domain.Entities;

namespace Devevil.Blog.DAL.Mapping
{
    public class AuthorMap : ClassMap<Author>
    {
        public AuthorMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Length(255).Not.Nullable();
            Map(x => x.Surname).Length(255).Not.Nullable();
            Map(x => x.Email).Length(255).Not.Nullable();
            Map(x => x.BirthDate).Not.Nullable();
            HasMany(x => x.Pages).Inverse();
        }
    }
}
