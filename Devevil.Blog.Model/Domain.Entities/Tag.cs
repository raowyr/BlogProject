﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devevil.Blog.Infrastructure.Core.Entities;

namespace Devevil.Blog.Model.Domain.Entities
{
    public class Tag : EntityBase<int,Tag>
    {
        private string _name;
        private IList<Page> _pages;

        public Tag()
        {
            _pages = new List<Page>();
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual IList<Page> Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

        public virtual void AddPage(Page prmPage)
        {
            if (_pages != null && prmPage != null)
                _pages.Add(prmPage);
            else
                throw new ArgumentNullException();
        }

        protected override bool IsValidState()
        {
            bool toReturn = true;

            if (String.IsNullOrEmpty(_name))
            {
                toReturn = false;
                AddWrongState("Nome del tag obbligatorio");
            }

            return toReturn;
        }
    }
}