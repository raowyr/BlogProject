using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class UserLoginViewModel
    {
        private string _email;
        private string _password;

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        [Required, EmailAddress]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [Required]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

    }
}