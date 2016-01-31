using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Infrastructure.Core.Entities;

namespace Devevil.Blog.Model.Domain.Entities
{
    public class Category : EntityBase<int,Category>
    {
        private string _name;
        private string _description;
        private IList<Page> _pages;

        public Category()
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
            if (_pages != null && prmPage != null)
                _pages.Add(prmPage);
            else
                throw new ArgumentNullException();
        }

        protected override bool IsValidState()
        {
            bool toReturn = true;

            if (String.IsNullOrEmpty(_name))
            {
                toReturn = false;
                AddWrongState("Nome della categoria obbligatorio");
            }
            if (String.IsNullOrEmpty(_description))
            {
                toReturn = false;
                AddWrongState("Descrizione della categoria obbligatoria");
            }

            return toReturn;
        }
    }
}
