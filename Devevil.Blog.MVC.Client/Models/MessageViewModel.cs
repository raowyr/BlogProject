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

        [Required(ErrorMessage="Il nome del mittente è obbligatorio!")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _email;

        [Required(ErrorMessage="La mail del mittente è obbligatoria!"), EmailAddress]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _message;

        
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _body;

        [Required(ErrorMessage="Il messaggio è obbligatorio!")]
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }
    }
}