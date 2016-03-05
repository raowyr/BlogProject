using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Devevil.Blog.MVC.Client.Models
{
    public class BaseViewModel
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
