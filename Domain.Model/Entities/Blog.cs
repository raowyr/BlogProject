using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Support.Entities;

namespace Domain.Model.Entities
{
    public class Blog : EntityBase<int, Blog>
    {
        private string _name;
        private string _description;

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
