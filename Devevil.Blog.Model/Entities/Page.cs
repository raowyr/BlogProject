using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Infrastructure.Core.Entities;

namespace Devevil.Blog.Model.Entities
{
    public class Page : EntityBase<int,Page>
    {
        private string _title;
        private string _description;
        private DateTime _date;
        private string _bodyText;
        private Author _author;
        private IList<Tag> _tags;
        private Blog _blog;
        private Category _category;
        private IList<Comment> _comments;


        public Page() 
        {
            _tags = new List<Tag>();
        }

        public virtual string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public virtual DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public virtual string BodyText
        {
            get { return _bodyText; }
            set { _bodyText = value; }
        }

        public virtual Author Author
        {
            get { return _author; }
            set 
            {

                _author = value;
            }
        }

        public virtual IList<Tag> Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        public virtual Blog Blog
        {
            get { return _blog; }
            set { _blog = value; }
        }

        public virtual void AddTag(Tag prmTag)
        {
            if (_tags != null)
            {
                _tags.Add(prmTag);
            }
        }

        public virtual Category Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public virtual IList<Comment> Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        public virtual void AddComment(Comment prmComment)
        {
            prmComment.Page = this;
            if (_comments != null)
            {
                _comments.Add(prmComment);
            }
        }
    }
}
