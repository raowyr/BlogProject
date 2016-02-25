using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Infrastructure.Core.Entities;
using Devevil.Blog.Infrastructure.Core.Entities.Exception;

namespace Devevil.Blog.Model.Domain.Entities
{
    public class Category : EntityBase<int,Category>
    {
        private string _name;
        private string _description;
        private IList<Page> _pages;

        protected Category() { }

        public Category(string prmName, string prmDescription)
        {
            _name = prmName;
            _description = prmDescription;
            _pages = new List<Page>();
            if (!IsValidState())
                throw new EntityInvalidStateException();
        }

        public virtual void ModifyCategory(string prmName, string prmDescription)
        {
            _name = prmName;
            _description = prmDescription;

            if (!IsValidState())
                throw new EntityInvalidStateException();
        }

        public virtual string Name
        {
            get { return _name; }
            //set { _name = value; }
        }

        public virtual string Description
        {
            get { return _description; }
            //set { _description = value; }
        }

        public virtual IList<Page> Pages
        {
            get { return _pages; }
            //set { _pages = value; }
        }

        public virtual void AddCategoryToPage(Page prmPage)
        {
            if (_pages != null)
            {
                if (prmPage != null)
                {
                    if (!_pages.Contains(prmPage))
                    {
                        _pages.Add(prmPage);
                        prmPage.ReferencesToCategory(this);
                    }
                }
                else
                    throw new ArgumentNullException();
            }
            else
                throw new EntityInvalidStateException();
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
