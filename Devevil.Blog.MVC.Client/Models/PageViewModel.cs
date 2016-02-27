using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Devevil.Blog.MVC.Support;

namespace Devevil.Blog.MVC.Client.Models
{
    public class PageViewModel
    {
        private int _id;
        private string _titolo;
        private string _descrizione;
        private string _testo;
        private DateTime _data;

        private IEnumerable<SelectListItem> _authors;
        private string _selectedAuthor;
        private string _autore;

        private HttpPostedFileBase _file;
        private string _fileName;

        private string _message;



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
        public string Descrizione
        {
            get { return _descrizione; }
            set { _descrizione = value; }
        }

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

        [Required]
        public string SelectedAuthor
        {
            get { return _selectedAuthor; }
            set { _selectedAuthor = value; }
        }

        public IEnumerable<SelectListItem> Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public string Autore
        {
            get { return _autore; }
            set { _autore = value; }
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        [FileSize(2097152)]
        [FileTypes("jpg,jpeg,png,bmp,gif")]
        public HttpPostedFileBase File
        {
            get { return _file; }
            set { _file = value; }
        }
    }
}