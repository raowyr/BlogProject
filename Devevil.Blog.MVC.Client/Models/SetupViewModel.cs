using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class SetupViewModel
    {
        private string _username;
        private string _password;
        private string _email;
        private string _blog;
        private string _descrizione;


        [Required]
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        [Required]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        [Required, EmailAddress]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [Required]
        public string Blog
        {
            get { return _blog; }
            set { _blog = value; }
        }

        [Required]
        public string Descrizione
        {
            get { return _descrizione; }
            set { _descrizione = value; }
        }
    }
}