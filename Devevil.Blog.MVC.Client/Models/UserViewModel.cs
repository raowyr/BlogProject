using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class UserViewModel : BaseViewModel
    {
        private string _email;
        private string _password;
        private string _nome;
        private string _cognome;
        private DateTime _nascita;

        [Required]
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        [Required]
        public string Cognome
        {
            get { return _cognome; }
            set { _cognome = value; }
        }

        [Required, EmailAddress]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        [Required]
        public DateTime Nascita
        {
            get { return _nascita; }
            set { _nascita = value; }
        }

    }
}