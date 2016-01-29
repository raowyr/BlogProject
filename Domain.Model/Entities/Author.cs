﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entities
{
    public class Author
    {
        private string _name;
        private string _surname;
        private DateTime _birthDate;
        private string _email;

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public virtual DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

    }
}
