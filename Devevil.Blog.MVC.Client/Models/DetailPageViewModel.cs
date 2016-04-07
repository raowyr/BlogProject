using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class DetailPageViewModel : BaseViewModel
    {
        private IList<PostViewModel> _postPreview;
        private IList<CategoryViewModel> _categoriesPreview;

        private PostViewModel _detailedPost;
        public PostViewModel DetailedPost
        {
            get { return _detailedPost; }
            set { _detailedPost = value; }
        }

        private int _page;
        public int Page
        {
            get { return _page; }
            set { _page = value; }
        }

        public DetailPageViewModel()
        {
            _postPreview = new List<PostViewModel>();
            _categoriesPreview = new List<CategoryViewModel>();
        }

        public IList<PostViewModel> PostPreview
        {
            get { return _postPreview; }
            set { _postPreview = value; }
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

        private CommentViewModel _comment;
        public CommentViewModel Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }
    }
}