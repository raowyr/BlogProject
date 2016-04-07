using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class CommentViewModel
    {
        private string _userName;

        [Required(ErrorMessage="Il nome utente è obbligatorio")]
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private string _userMail;

        [EmailAddress]
        public string UserMail
        {
            get { return _userMail; }
            set { _userMail = value; }
        }
        private string _textComment;

        [Required(ErrorMessage="Il testo del commento è obbligatorio")]
        public string TextComment
        {
            get { return _textComment; }
            set { _textComment = value; }
        }

    }
}