using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class ListCategoryViewModel : BaseViewModel
    {
        private IList<PostViewModel> _postPreview;
        private IList<CategoryViewModel> _categories;

        private int _page;

        public int Page
        {
            get { return _page; }
            set { _page = value; }
        }

        public ListCategoryViewModel()
        {
            _postPreview = new List<PostViewModel>();
            _categories = new List<CategoryViewModel>();
        }

        public IList<PostViewModel> PostPreview
        {
            get { return _postPreview; }
            set { _postPreview = value; }
        }

        public IList<CategoryViewModel> Categories
        {
            get
            {
                return _categories;
            }

            set
            {
                _categories = value;
            }
        }
    }
}