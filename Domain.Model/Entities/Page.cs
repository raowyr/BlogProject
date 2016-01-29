using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entities
{
    public class Page
    {
        private string _title;
        private string _description;
        private DateTime _date;
        private string _bodyText;
        private Author _author;
        private IList<Tag> _tags;
        private Blog _blog;

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
            set { _author = value; }
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

    }
}
