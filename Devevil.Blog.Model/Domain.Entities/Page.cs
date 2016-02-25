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
    public class Page : EntityBase<int,Page>
    {
        private string _title;
        private string _description;
        private DateTime? _date;
        private string _bodyText;
        private Author _author;
        private IList<Tag> _tags;
        private Blog _blog;
        private Category _category;
        private IList<Comment> _comments;
        private bool _isDeleted;
        private string _imagePath;

        public virtual string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }

        public virtual bool IsDeleted
        {
            get { return _isDeleted; }
        }

        protected Page() { }

        public Page(string prmTitle, string prmDescription, DateTime prmDate, string prmBodyText, Author prmAuthor, Blog prmBlog, Category prmCategory)
        {
            _title = prmTitle;
            _description = prmDescription;
            _date = prmDate;
            _bodyText = prmBodyText;

            //if (prmAuthor != null)
            //{
            //    prmAuthor.AddAuthoringPage(this);
                _author = prmAuthor;
            //}

            //if (prmBlog != null)
            //{
            //    prmBlog.AddPageToBlog(this);
                _blog = prmBlog;
            //}

            //if (prmCategory != null)
            //{
            //    prmCategory.AddCategoryToPage(this);
                _category = prmCategory;
            //}

            _tags = new List<Tag>();
            _comments = new List<Comment>();
            _isDeleted = false;

            if (!IsValidState())
                throw new EntityInvalidStateException();
        }

        public virtual void SetImagePath(string prmPath)
        {
            _imagePath = prmPath;
        }

        public virtual string Title
        {
            get { return _title; }
        }

        public virtual string Description
        {
            get { return _description; }
        }

        public virtual DateTime? Date
        {
            get { return _date; }
        }

        public virtual string BodyText
        {
            get { return _bodyText; }
        }

        public virtual Author Author
        {
            get { return _author; }
        }

        public virtual void ReferencesToAuthor(Author prmAuthor)
        {
            if (prmAuthor != null)
            {
                _author = prmAuthor;
            }
            else
                throw new ArgumentNullException();
        }

        public virtual IList<Tag> Tags
        {
            get { return _tags; }
        }

        public virtual Blog Blog
        {
            get { return _blog; }
        }

        public virtual void ReferencesToBlog(Blog prmBlog)
        {

                _blog = prmBlog;

        }

        public virtual void AddTag(Tag prmTag)
        {
            if (_tags != null)
            {
                if (prmTag != null)
                {
                    if (!_tags.Contains(prmTag))
                    {
                        _tags.Add(prmTag);
                        prmTag.AddTagToPage(this);
                    }   
                }
                else
                    throw new ArgumentNullException();
            }
        }

        public virtual Category Category
        {
            get { return _category; }
        }

        public virtual void ReferencesToCategory(Category prmCategory)
        {
            if (prmCategory != null)
            {
                _category = prmCategory;
            }
            else
                throw new ArgumentNullException();
        }

        public virtual IList<Comment> Comments
        {
            get { return _comments; }
        }

        public virtual void AddComment(Comment prmComment)
        {
            if (_comments != null)
            {
                if (prmComment != null)
                {
                    if (!_comments.Contains(prmComment))
                    {
                        _comments.Add(prmComment);
                        prmComment.IsCommentOfPage(this);
                    }
                }
                else
                    throw new ArgumentNullException();
            }
            else
                throw new EntityInvalidStateException();
        }

        public virtual void DeletePage()
        {
            _isDeleted = true;
        }

        protected override bool IsValidState()
        {
            bool toReturn = true;

            if (String.IsNullOrEmpty(_title))
            {
                toReturn = false;
                AddWrongState("Titolo della pagina obbligatorio");
            }

            if (String.IsNullOrEmpty(_description))
            {
                toReturn = false;
                AddWrongState("Descrizione della pagina obbligatoria");
            }

            if (!_date.HasValue)
            {
                toReturn = false;
                AddWrongState("Data di creazione della pagina obbligatoria");
            }

            if (String.IsNullOrEmpty(_bodyText))
            {
                toReturn = false;
                AddWrongState("Testo della pagina obbligatorio");
            }

            if(_author==null)
            {
                toReturn = false;
                AddWrongState("Autore della pagina obbligatorio");
            }
            if (_blog == null)
            {
                toReturn = false;
                AddWrongState("Blog di afferenza obbligatorio");
            }
            if(_category == null)
            {
                toReturn = false;
                AddWrongState("Categoria di appartenenza obbligatoria");
            }

            return toReturn;
        }
    }
}
