using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Infrastructure.Core.Entities;

namespace Devevil.Blog.Model.Entities
{
    public class Author : EntityBase<int, Author>
    {
        private string _name;
        private string _surname;
        private DateTime _birthDate;
        private string _email;
        private IList<Page> _pages;

        public Author()
        {
            _pages = new List<Page>();
        }

        public virtual IList<Page> Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public virtual DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        public virtual string Email
        {
            get { return _email; }
            set { _email = value; }
        }

    }
}
