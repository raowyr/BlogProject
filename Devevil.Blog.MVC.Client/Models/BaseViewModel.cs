using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public string FriendlyURL(string prmInputString)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            return rgx.Replace(prmInputString, "-");
        }
    }
}
