using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Devevil.Blog.MVC.Support;

namespace Devevil.Blog.MVC.Client.Models
{
    public class PageViewModel : BaseViewModel
    {
        private int _id;
        private string _titolo;
        private string _descrizione;
        private DateTime _data;

        private IEnumerable<SelectListItem> _authors;
        private string _selectedAuthor;
        private string _autore;

        private IEnumerable<SelectListItem> _blogs;
        private string _selectedBlog;

        private IEnumerable<SelectListItem> _categories;
        private string _selectedCategory;
        private string _category;

        private string _body;

        private HttpPostedFileBase _file;
        private string _fileName;

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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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

        public string Autore
        {
            get { return _autore; }
            set { _autore = value; }
        }

        [Required]
        public string SelectedBlog
        {
            get { return _selectedBlog; }
            set { _selectedBlog = value; }
        }

        public IEnumerable<SelectListItem> Blogs
        {
            get { return _blogs; }
            set { _blogs = value; }
        }

        [Required]
        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; }
        }

        public IEnumerable<SelectListItem> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }

        public string Categoria
        {
            get { return _category; }
            set { _category = value; }
        }

        [Required]
        [AllowHtml]
        public string Body
        {
            get { return _body; }
            set { _body = value; }
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