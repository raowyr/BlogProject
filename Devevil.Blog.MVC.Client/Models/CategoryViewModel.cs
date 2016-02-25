using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class CategoryViewModel
    {
        private int _id;
        private string _categoryName;
        private string _categoryDescription;

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
                return _categoryName;
            }

            set
            {
                _categoryName = value;
            }
        }

        [Required]
        public string Descrizione
        {
            get
            {
                return _categoryDescription;
            }

            set
            {
                _categoryDescription = value;
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