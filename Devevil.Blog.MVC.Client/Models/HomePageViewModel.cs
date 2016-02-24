using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class HomePageViewModel
    {
        private IList<PostViewModel> _postPreview;
        private IList<PostViewModel> _postDetail;
        private IList<CategoryViewModel> _categoriesPreview;

        private int _page;

        public int Page
        {
            get { return _page; }
            set { _page = value; }
        }

        private MessageViewModel _message;

        public HomePageViewModel()
        {
            _postPreview = new List<PostViewModel>();
            _postDetail = new List<PostViewModel>();
            _categoriesPreview = new List<CategoryViewModel>();
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

        public MessageViewModel Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public IList<CategoryViewModel> CategoriesPreview
        {
            get
            {
                return _categoriesPreview;
            }

            set
            {
                _categoriesPreview = value;
            }
        }
    }
}