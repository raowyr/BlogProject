using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devevil.Blog.Infrastructure.Core.RepositoryPattern
{
    public interface IRepositoryRead<T>
    {
        T Load(object id);
        T GetById(object id);
    }
}
