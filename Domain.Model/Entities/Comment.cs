using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Support.Entities;

namespace Domain.Model.Entities
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
            set { _userMail = value; }
        }

        public virtual string TextComment
        {
            get { return _textComment; }
            set { _textComment = value; }
        }

        public virtual Page Page
        {
            get { return _page; }
            set { _page = value; }
        }
    }
}
