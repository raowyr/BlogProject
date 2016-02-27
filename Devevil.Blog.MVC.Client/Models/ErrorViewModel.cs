using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Devevil.Blog.MVC.Client.Models
{
    public class ErrorViewModel
    {
        private string _refferalUrl;

        public string RefferalUrl
        {
            get { return _refferalUrl; }
            set { _refferalUrl = value; }
        }

    }
}
