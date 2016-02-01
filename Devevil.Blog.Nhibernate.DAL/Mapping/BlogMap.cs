using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Model.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Devevil.Blog.Nhibernate.DAL.Mapping
{
    public class BlogMap : ClassMap<Devevil.Blog.Model.Domain.Entities.Blog>
    {
        public BlogMap()
        {
            //Chiave primaria Identity di tipo Intero
            Id(x => x.Id).GeneratedBy.Identity();
            //Colonne not nullable
            Map(x => x.Name).Length(255).Not.Nullable();
            Map(x => x.Description).Length(255).Not.Nullable();
            //Relazione uno a molti. Una qualsiasi modifica al blog ha effetto su tutte le pagine ad esso afferenti
            //ad esempio se elimino il blog, elimino anche tutte le pagine
            HasMany(x => x.Pages).Inverse().Cascade.All();
        }
    }
}
