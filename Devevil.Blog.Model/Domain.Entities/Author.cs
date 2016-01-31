﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Infrastructure.Core.Entities;
using Devevil.Blog.Model.Business.Helpers;
using Devevil.Blog.Model.Domain.Exceptions;

namespace Devevil.Blog.Model.Domain.Entities
{
    public class Author : EntityBase<int, Author>
    {
        private string _name;
        private string _surname;
        private DateTime? _birthDate;
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

        public virtual DateTime? BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        public virtual string Email
        {
            get { return _email; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    if (EmailValidator.IsValidMail(value))
                        _email = value;
                    else
                        throw new AuthorBadMailException();
                }
            }
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
                AddWrongState("Nome obbligatorio");
            }
            if(String.IsNullOrEmpty(_surname))
            {
                toReturn = false;
                AddWrongState("Cognome obbligatorio");
            }
            if(!_birthDate.HasValue)
            {
                toReturn = false;
                AddWrongState("Data di nascita obbligatoria");
            }
            if (String.IsNullOrEmpty("Email"))
            {
                toReturn = false;
                AddWrongState("Email obbligatoria");
            }
            if (!String.IsNullOrEmpty("Email") && !EmailValidator.IsValidMail(_email))
            {
                toReturn = false;
                AddWrongState("Email non valida");
            }
            return toReturn;
        }
    }
}