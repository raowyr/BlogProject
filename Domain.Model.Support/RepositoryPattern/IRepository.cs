using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Support.RepositoryPattern
{
    public interface IRepository<T>
    {
        T Load(object id);
        T GetById(object id);
        ICollection<T> FindAll();
        int Count();
        IList<T> FindPage(int pageStartRow, int pageSize);
        IList<T> FindSortedPage(int pageStartRow, int pageSize, string sortBy, bool descending);
        void Save(T entity);
        void SaveOrUpdate(T entity);
        void Delete(T entity);
    }
}
