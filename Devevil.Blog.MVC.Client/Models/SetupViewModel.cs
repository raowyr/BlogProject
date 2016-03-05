using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class SetupViewModel : BaseViewModel
    {
        private string _nome;
        private string _cognome;
        private DateTime _nascita;

        private string _password;
        private string _email;
        private string _blog;
        private string _descrizione;

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

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Nascita
        {
            get
            {
                return _nascita;
            }

            set
            {
                _nascita = value;
            }
        }

    }
}