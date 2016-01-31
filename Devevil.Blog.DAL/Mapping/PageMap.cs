using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Model.Entities;
using FluentNHibernate.Mapping;

namespace Devevil.Blog.DAL.Mapping
{
    public class PageMap : ClassMap<Page>
    {
        public PageMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Title).Length(255).Not.Nullable();
            Map(x => x.Description).Length(255).Not.Nullable();
            Map(x => x.Date).Not.Nullable();
            Map(x => x.BodyText).Not.Nullable();
            References(x => x.Author).Column("AuthorId").Not.Nullable().Cascade.All();
            References(x => x.Blog).Column("BlogId").Not.Nullable();
            References(x => x.Category).Column("CategoryId").Not.Nullable().Cascade.All();
            HasManyToMany(x => x.Tags).Cascade.All().Table("PagesTags");
            HasMany(x => x.Comments).Cascade.All();
        }
    }
}
