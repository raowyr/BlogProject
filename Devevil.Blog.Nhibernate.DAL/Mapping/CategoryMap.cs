using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Model.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Devevil.Blog.Nhibernate.DAL.Mapping
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Length(255).Not.Nullable();
            Map(x => x.Description).Length(255).Not.Nullable();
            Map(x => x.ImagePath).Length(255);
            HasMany(x => x.Pages).Inverse();
        }
    }
}
