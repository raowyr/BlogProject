using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Devevil.Blog.MVC.Client.Models
{
    public class ListPageViewModel : BaseViewModel
    {
        private IList<PostViewModel> _postPreview;
        private IList<CategoryViewModel> _categoriesPreview;
        private IList<PostViewModel> _posts;
        private int _page;
        private int _totalPages;

        private int _idCategory;
        private string categoryName;

        public int IdCategory
        {
            get { return _idCategory; }
            set { _idCategory = value; }
        }

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }

        public IList<PostViewModel> Posts
        {
            get { return _posts; }
            set { _posts = value; }
        }

        public int Page
        {
            get { return _page; }
            set { _page = value; }
        }

        public ListPageViewModel()
        {
            _postPreview = new List<PostViewModel>();
            _categoriesPreview = new List<CategoryViewModel>();
            _posts = new List<PostViewModel>();
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
    }
}