using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class TagViewModel : BaseViewModel
    {
        private int _id;
        private string _tagName;

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