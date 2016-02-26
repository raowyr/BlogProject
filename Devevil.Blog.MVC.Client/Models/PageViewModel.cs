using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class PageViewModel
    {
        private int _id;
        private string _titolo;
        private string _testo;
        private DateTime _data;

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

        [Required]
        public string Titolo
        {
            get { return _titolo; }
            set { _titolo = value; }
        }

        [Required]
        public string Testo
        {
            get { return _testo; }
            set { _testo = value; }
        }

        [Required]
        public DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }


    }
}