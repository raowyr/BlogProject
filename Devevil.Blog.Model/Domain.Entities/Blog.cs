using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Infrastructure.Core.Entities;
using Devevil.Blog.Infrastructure.Core.Entities.Exception;
using Devevil.Blog.Model.Domain.Exceptions;

namespace Devevil.Blog.Model.Domain.Entities
{
    public class Blog : EntityBase<int, Blog>
    {
        private string _name;
        private string _description;
        private IList<Page> _pages;
        private IList<Author> _authors;
        private bool _isDeleted;

        public virtual bool IsDeleted
        {
            get { return _isDeleted; }
        }

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
            _isDeleted = false;
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
                    Page tmp = _pages.Where(x => x == prmPage).FirstOrDefault();
                    if (tmp != null)
                        tmp.DeletePage();
                    else
                        throw new PageNotFoundException();
                }
                else
                    throw new ArgumentNullException();
            }
            else
                throw new EntityInvalidStateException();
        }

        public virtual void RemoveAuthorFromBlog(Author prmAuthor)
        {
            if (_pages != null && _authors!=null)
            {
                if (prmAuthor != null)
                {
                    var auth = _authors.Where(x => x == prmAuthor).FirstOrDefault();

                    if (auth != null)
                    {
                        auth.DeleteAuthor();

                        var tmpPages = _pages.Where(x => x.Author == prmAuthor);

                        if (tmpPages != null)
                        {
                            foreach (var p in tmpPages.ToList())
                                p.DeletePage();
                        }
                    }
                    else
                        throw new AuthorNotFoundException();
                }
                else
                    throw new ArgumentNullException();
            }
            else
                throw new EntityInvalidStateException();
        }

        public virtual void DeleteBlog()
        {
            if (_pages != null && _authors!=null)
            {
                foreach (var p in _pages)
                {
                    p.DeletePage();
                }
                foreach (var a in _authors)
                    RemoveAuthorFromBlog(a);
                _isDeleted = true;
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
