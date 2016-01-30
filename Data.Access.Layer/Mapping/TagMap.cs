using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Entities;
using FluentNHibernate.Mapping;

namespace Data.Access.Layer.Mapping
{
    public class TagMap : ClassMap<Tag>
    {
        public TagMap()
        {
            //Chiave primaria identity
            Id(x => x.Id).GeneratedBy.Identity();
            //colonna not nullable di lunghezza 255
            Map(x => x.Name).Length(255).Not.Nullable();
            HasManyToMany(x => x.Pages).Table("PagesTags");
        }
    }
}
