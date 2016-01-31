﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Infrastructure.Core.Entities;
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


        public Page() 
        {
            _tags = new List<Tag>();
            _comments = new List<Comment>();
        }

        public virtual string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public virtual DateTime? Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public virtual string BodyText
        {
            get { return _bodyText; }
            set { _bodyText = value; }
        }

        public virtual Author Author
        {
            get { return _author; }
            set 
            {
                if (value != null)
                {
                    value.AddPage(this);
                    _author = value;
                }
                else
                    throw new PageAuthorNullException();
            }
        }

        public virtual IList<Tag> Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        public virtual Blog Blog
        {
            get { return _blog; }
            set { _blog = value; }
        }

        public virtual void AddTag(Tag prmTag)
        {
            if (_tags != null)
            {
                if (prmTag != null)
                {
                    prmTag.AddPage(this);
                    _tags.Add(prmTag);
                }
                else
                    throw new TagNullException();
            }
        }

        public virtual Category Category
        {
            get { return _category; }
            set {
                if (value != null)
                {
                    value.AddPage(this);
                    _category = value;
                }
                else
                    throw new CategoryNullException();
            }
        }

        public virtual IList<Comment> Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        public virtual void AddComment(Comment prmComment)
        {
            if (_comments != null)
            {
                prmComment.Page = this;
                _comments.Add(prmComment);
            }
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