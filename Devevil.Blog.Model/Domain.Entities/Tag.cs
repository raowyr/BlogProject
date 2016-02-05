using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Infrastructure.Core.Entities;
using Devevil.Blog.Infrastructure.Core.Entities.Exception;

namespace Devevil.Blog.Model.Domain.Entities
{
    public class Tag : EntityBase<int,Tag>
    {
        private string _name;
        private IList<Page> _pages;

        protected Tag() { }

        public Tag(string prmtagName)
        {
            _name = prmtagName;
            _pages = new List<Page>();
            if (!IsValidState())
                throw new EntityInvalidStateException();
        }

        public virtual string Name
        {
            get { return _name; }
        }

        public virtual IList<Page> Pages
        {
            get { return _pages; }
        }

        public virtual void AddTagToPage(Page prmPage)
        {
            if (_pages != null)
            {
                if (prmPage != null)
                {
                    prmPage.AddTag(this);
                    _pages.Add(prmPage);
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
                AddWrongState("Nome del tag obbligatorio");
            }

            return toReturn;
        }
    }
}
