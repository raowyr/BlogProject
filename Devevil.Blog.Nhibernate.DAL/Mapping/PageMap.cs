using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Model.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Devevil.Blog.Nhibernate.DAL.Mapping
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
            Map(x => x.IsDeleted).Not.Nullable();
            Map(x => x.ImagePath).Length(255);
            References(x => x.Author).Column("AuthorId").Cascade.SaveUpdate();
            References(x => x.Blog).Column("BlogId");
            References(x => x.Category).Column("CategoryId").Cascade.SaveUpdate();
            HasManyToMany(x => x.Tags).Table("PagesTags").Cascade.SaveUpdate();
            HasMany(x => x.Comments).Inverse().Cascade.All();
        }
    }
}
