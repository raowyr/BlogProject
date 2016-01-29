using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entities
{
    public class Tag
    {
        private string _name;

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }


    }
}
