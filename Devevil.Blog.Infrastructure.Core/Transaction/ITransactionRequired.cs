using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devevil.Blog.Infrastructure.Core.Transaction
{
    public interface ITransactionRequired
    {
        void Commit();
        void RollbackTransaction();
    }
}
