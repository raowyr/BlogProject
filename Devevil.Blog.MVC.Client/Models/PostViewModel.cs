using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class PostViewModel : BaseViewModel
    {
        private int _id;
        private string _titolo;
        private string _autore;
        private string _categoria;
        private int _idCategoria;
        private IList<string> _tags;

        public IList<string> Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        public int IdCategoria
        {
            get { return _idCategoria; }
            set { _idCategoria = value; }
        }

        public string Categoria
        {
            get { return _categoria; }
            set { _categoria = value; }
        }

        public string Autore
        {
            get { return _autore; }
            set { _autore = value; }
        }

        public string Titolo
        {
            get { return _titolo; }
            set { _titolo = value; }
        }

        public string TitoloURLFirendly
        {
            get
            {
                Regex rgx = new Regex("[^a-zA-Z0-9]");
                return rgx.Replace(_titolo, "-");
            }
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