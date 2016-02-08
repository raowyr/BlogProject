using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devevil.Blog.Infrastructure.Core.RepositoryPattern
{
    public interface IRepositoryDelete<T>
    {
        void Delete(T entity);
    }
}
