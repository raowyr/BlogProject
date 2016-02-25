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
        private string categoryName;
        private string categoryDescription;

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        [Required]
        public string CategoryName
        {
            get
            {
                return categoryName;
            }

            set
            {
                categoryName = value;
            }
        }

        [Required]
        public string CategoryDescription
        {
            get
            {
                return categoryDescription;
            }

            set
            {
                categoryDescription = value;
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