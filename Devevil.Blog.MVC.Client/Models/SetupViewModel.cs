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
        [Display(Name ="Nome amministratore")]
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        [Required]
        [Display(Name = "Cognome amministratore")]
        public string Cognome
        {
            get { return _cognome; }
            set { _cognome = value; }
        }

        [Required]
        [Display(Name = "Password account")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        [Required, EmailAddress]
        [Display(Name = "Email (username account)")]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [Required]
        [Display(Name = "Nome del Blog")]
        public string Blog
        {
            get { return _blog; }
            set { _blog = value; }
        }

        [Required]
        [Display(Name = "Descrizione breve del Blog")]
        public string Descrizione
        {
            get { return _descrizione; }
            set { _descrizione = value; }
        }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data di nascita dell' amministratore")]
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