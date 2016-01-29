using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Support.RepositoryPattern
{
    public interface ITransactionRequired
    {
        void Commit();
        void RollbackTransaction();
    }
}
