using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class MessageViewModel
    {
        private string _name;

        [Required]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _email;

        [Required, EmailAddress]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _message;

        [Required]
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}