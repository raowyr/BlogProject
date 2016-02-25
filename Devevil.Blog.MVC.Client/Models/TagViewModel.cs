using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class TagViewModel
    {
        private int _id;
        private string _tagName;

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        [Required]
        public string Nome
        {
            get
            {
                return _tagName;
            }

            set
            {
                _tagName = value;
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }
    }
}