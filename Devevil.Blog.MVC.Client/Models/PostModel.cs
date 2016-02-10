using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class PostModel
    {
        private string _titolo;

        public string Titolo
        {
            get { return _titolo; }
            set { _titolo = value; }
        }

        private string _testo;

        public string Testo
        {
            get { return _testo; }
            set { _testo = value; }
        }
        private DateTime _data;

        public DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }


    }
}