using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class HomePageModel
    {
        private string _aforisma;
        private IList<PostModel> _postPreview;

        public HomePageModel()
        {
            _postPreview = new List<PostModel>();
        }

        public IList<PostModel> PostPreview
        {
            get { return _postPreview; }
            set { _postPreview = value; }
        }

        public string Aforisma
        {
            get { return _aforisma; }
            set { _aforisma = value; }
        }
    }
}