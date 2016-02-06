using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Infrastructure.Core.Entities;
using Devevil.Blog.Infrastructure.Core.Entities.Exception;

namespace Devevil.Blog.Model.Domain.Entities
{
    public class Blog : EntityBase<int, Blog>
    {
        private string _name;
        private string _description;
        private IList<Page> _pages;
        private IList<Author> _authors;

        public virtual IList<Author> Authors
        {
            get { return _authors; }
        }

        protected Blog() { }

        public Blog(string prmName, string prmDescription)
        {
            _name = prmName;
            _description = prmDescription;
            _pages = new List<Page>();
            _authors = new List<Author>();
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
                if (prmPage != null)
                {
                    if (!_pages.Contains(prmPage))
                    {
                        _pages.Add(prmPage);
                        prmPage.ReferencesToBlog(this);
                    }
                }
                else
                    throw new ArgumentNullException();
            }
            else
                throw new EntityInvalidStateException();
        }

        public virtual void AddAuthorToBlog(Author prmAuthor)
        {
            if (_authors != null)
            {
                if (prmAuthor != null)
                {
                    if (!_authors.Contains(prmAuthor))
                    {
                        _authors.Add(prmAuthor);
                        prmAuthor.ReferencesToBlog(this);
                    }
                }
                else
                    throw new ArgumentNullException();
            }
            else
                throw new EntityInvalidStateException();
        }

        public virtual void RemovePageFromBlog(Page prmPage)
        {
            if (_pages != null)
            {
                if (prmPage != null)
                {
                    _pages.Remove(prmPage);
                    prmPage.ReferencesToBlog(null);
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
