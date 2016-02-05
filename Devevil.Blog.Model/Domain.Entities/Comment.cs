using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Infrastructure.Core.Entities;
using Devevil.Blog.Infrastructure.Core.Entities.Exception;
using Devevil.Blog.Model.Domain.Exceptions;
using Devevil.Blog.Support.Validator;

namespace Devevil.Blog.Model.Domain.Entities
{
    public class Comment : EntityBase<int,Comment>
    {
        private string _userName;
        private string _userMail;
        private string _textComment;
        private Page _page;

        protected Comment() { }

        public Comment(string prmUsername, string prmUserMail, string prmText, Page prmPage)
        {
            _userName = prmUsername;

            if (StringValidator.CheckIsValidMail(prmUserMail))
                _userMail = prmUserMail;
            else
                throw new UserBadMailException();

            _textComment = prmText;
            _page = prmPage;

            if (!IsValidState())
                throw new EntityInvalidStateException();
        }

        public virtual string UserName
        {
            get { return _userName; }
        }

        public virtual string UserMail
        {
            get { return _userMail; }
        }

        public virtual string TextComment
        {
            get { return _textComment; }
        }

        public virtual Page Page
        {
            get { return _page; }
        }

        public virtual void IsCommentOfPage(Page prmPage)
        {
            if (prmPage != null)
            {
                _page = prmPage;
            }
            else
                throw new ArgumentNullException();
        }

        protected override bool IsValidState()
        {
            bool toReturn = true;

            if (String.IsNullOrEmpty(_userName))
            {
                toReturn = false;
                AddWrongState("Username dell'utente obbligatorio");
            }
            if (String.IsNullOrEmpty(_userMail))
            {
                toReturn = false;
                AddWrongState("Email dell'utente obbligatoria");
            }
            if (String.IsNullOrEmpty(_textComment))
            {
                toReturn = false;
                AddWrongState("Testo del commento obbligatorio");
            }
            if (_page == null)
            {
                toReturn = false;
                AddWrongState("Pagina da associare al commento obbligatoria");
            }

            return toReturn;
        }
    }
}
