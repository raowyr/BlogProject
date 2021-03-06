﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Model.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Devevil.Blog.Nhibernate.DAL.Mapping
{
    public class TagMap : ClassMap<Tag>
    {
        public TagMap()
        {
            //Chiave primaria identity
            Id(x => x.Id).GeneratedBy.Identity();
            //colonna not nullable di lunghezza 255
            Map(x => x.Name).Length(255).Not.Nullable();
            HasManyToMany(x => x.Pages).Table("PagesTags").Inverse().Cascade.SaveUpdate();
        }
    }
}
