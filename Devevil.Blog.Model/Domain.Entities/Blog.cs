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

        protected Blog() { }

        public Blog(string prmName, string prmDescription)
        {
            _name = prmName;
            _description = prmDescription;
            _pages = new List<Page>();
        }

        public virtual string Name
        {
            get { return _name; }
        }

        public virtual string Description
        {
            get { return _description; }
        }

        public virtual IList<Page> Pages
        {
            get { return _pages; }
        }

        public virtual void AddPageToBlog(Page prmPage)
        {
            if (_pages != null)
            {
                prmPage.ReferencesToBlog(this);
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
