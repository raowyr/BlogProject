using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Infrastructure.Core.Entities;
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

        public virtual string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public virtual string UserMail
        {
            get { return _userMail; }
            set {
                if (value != null)
                {
                    if (StringValidator.CheckIsValidMail(value))
                        _userMail = value;
                    else
                        throw new UserBadMailException();
                }
            }
        }

        public virtual string TextComment
        {
            get { return _textComment; }
            set { _textComment = value; }
        }

        public virtual Page Page
        {
            get { return _page; }
            set {
                if(value!= null)
                    _page = value;
            }
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

            return toReturn;
        }
    }
}
