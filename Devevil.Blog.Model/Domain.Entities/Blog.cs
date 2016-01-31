using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Infrastructure.Core.Entities;

namespace Devevil.Blog.Model.Domain.Entities
{
    public class Blog : EntityBase<int, Blog>
    {
        private string _name;
        private string _description;
        private IList<Page> _pages;

        public Blog()
        {
            _pages = new List<Page>();
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public virtual IList<Page> Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

        public virtual void AddPage(Page prmPage)
        {
            if (_pages != null)
            {
                prmPage.Blog = this;
                _pages.Add(prmPage);
            }
        }

        protected override bool IsValidState()
        {
            bool toReturn = true;

            if(String.IsNullOrEmpty(_name))
            {
                toReturn = false;
                AddWrongState("Nome del blog obbligatorio");
            }
            if(String.IsNullOrEmpty(_description))
            {
                toReturn = false;
                AddWrongState("Descrizione del blog obbligatoria");
            }

            return toReturn;
        }
    }
}
