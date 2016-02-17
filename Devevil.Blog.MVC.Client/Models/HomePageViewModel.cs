using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class HomePageViewModel
    {
        private string _aforisma;
        private IList<PostViewModel> _postPreview;
        private IList<PostViewModel> _postDetail;


        private MessageViewModel _message;

        public HomePageViewModel()
        {
            _postPreview = new List<PostViewModel>();
            _postDetail = new List<PostViewModel>();
        }

        public IList<PostViewModel> PostPreview
        {
            get { return _postPreview; }
            set { _postPreview = value; }
        }

        public IList<PostViewModel> PostDetail
        {
            get { return _postDetail; }
            set { _postDetail = value; }
        }

        public string Aforisma
        {
            get { return _aforisma; }
            set { _aforisma = value; }
        }

        public MessageViewModel Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}