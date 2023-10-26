using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMultitenantArchitecture.Domain.IRepositories
{
    public interface IUnitOfWork
    {

        IBaseRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
         where TKey : struct;
        Task<int> SaveChangesAsync();

    }
}
