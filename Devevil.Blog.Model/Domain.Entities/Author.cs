using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Infrastructure.Core.Entities;
using Devevil.Blog.Infrastructure.Core.Entities.Exception;
using Devevil.Blog.Model.Domain.Exceptions;
using Devevil.Blog.Support.Security;
using Devevil.Blog.Support.Validator;

namespace Devevil.Blog.Model.Domain.Entities
{
    public class Author : EntityBase<int, Author>
    {
        private string _name;
        private string _surname;
        private DateTime? _birthDate;
        private string _email;
        private IList<Page> _pages;
        private bool _isAdministrator;
        private string _password;
        private Blog _blog;

        protected Author() { }

        /// <summary>
        /// Crea un oggetto Autore
        /// </summary>
        /// <param name="prmName"></param>
        /// <param name="prmSurname"></param>
        /// <param name="prmBirthDate"></param>
        /// <param name="prmEmail"></param>
        /// <param name="prmIsAdministrator"></param>
        /// <param name="prmPassword"></param>
        /// <exception cref="AuthorBadMailException"></exception>
        /// <exception cref="EntityInvalidStateException"></exception>
        public Author(string prmName, string prmSurname, DateTime prmBirthDate, string prmEmail, bool prmIsAdministrator, string prmPassword, Blog prmBlog)
        {
            _name = prmName;
            _surname = prmSurname;
            _birthDate = prmBirthDate;

            if(StringValidator.CheckIsValidMail(prmEmail))
                _email = prmEmail;
            else
                throw new AuthorBadMailException();

            _isAdministrator = prmIsAdministrator;

            if(!String.IsNullOrEmpty(prmPassword))
                _password = PasswordHashManager.CreateHash(prmPassword);

            _blog = prmBlog;

            _pages = new List<Page>();

            if (!IsValidState())
                throw new EntityInvalidStateException();
        }

        public virtual IList<Page> Pages
        {
            get { return _pages; }
        }

        public virtual string Name
        {
            get { return _name; }
        }

        public virtual string Surname
        {
            get { return _surname; }
        }

        public virtual DateTime? BirthDate
        {
            get { return _birthDate; }
        }

        public virtual string Email
        {
            get { return _email; }
        }

        public virtual void AddAuthoringPage(Page prmPage)
        {
            if (_pages != null)
            {
                if (prmPage != null)
                {
                    if (!_pages.Contains(prmPage))
                    {
                        _pages.Add(prmPage);
                        prmPage.ReferencesToAuthor(this);
                    }
                }
                else
                    throw new ArgumentNullException();
            }
            else
                throw new EntityInvalidStateException();
        }

        public virtual bool IsAdministrator
        {
            get { return _isAdministrator; }
        }

        public virtual string Password
        {
            get { return _password; }
        }

        public virtual bool ValidatePassword(string prmPlainPassword)
        {
            bool toReturn = false;

            if (PasswordHashManager.ValidatePassword(prmPlainPassword, _password))
                toReturn = true;

            return toReturn;
        }

        public virtual Blog Blog
        {
            get { return _blog; }
        }

        public virtual void ReferencesToBlog(Blog prmBlog)
        {
            _blog = prmBlog;
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
            if (String.IsNullOrEmpty(_email))
            {
                toReturn = false;
                AddWrongState("Email obbligatoria");
            }
            if (!StringValidator.CheckIsValidMail(_email))
            {
                toReturn = false;
                AddWrongState("Email non valida");
            }
            if (String.IsNullOrEmpty(_password))
            {
                toReturn = false;
                AddWrongState("Password obbligatoria");
            }
            if (_blog == null)
            {
                toReturn = false;
                AddWrongState("Il blog è obbligatorio");
            }
            return toReturn;
        }


    }
}
