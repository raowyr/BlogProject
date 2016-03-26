using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Devevil.Blog.MVC.Support;

namespace Devevil.Blog.MVC.Client.Models
{
    public class CategoryViewModel : BaseViewModel
    {
        private int _id;
        private string _categoryName;
        private string _categoryDescription;
        private HttpPostedFileBase _file;
        private string _fileName;

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

        public string NomeCategoriaURLFirendly
        {
            get
            {
                return FriendlyURL(_categoryName);
            }
        }
    }
}