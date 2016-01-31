using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Infrastructure.Core.Entities;

namespace Devevil.Blog.Model.Entities
{
    public class Tag : EntityBase<int,Tag>
    {
        private string _name;
        private IList<Page> _pages;

        public Tag()
        {
            _pages = new List<Page>();
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual IList<Page> Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

    }
}
