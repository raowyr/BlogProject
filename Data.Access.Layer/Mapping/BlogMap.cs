using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Entities;
using FluentNHibernate.Mapping;

namespace Data.Access.Layer.Mapping
{
    public class BlogMap : ClassMap<Blog>
    {
        public BlogMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
        }
    }
}
