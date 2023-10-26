using CleanMultitenantArchitecture.Domain.Entities;
using CleanMultitenantArchitecture.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;


namespace CleanMultitenantArchitecture.Infraestructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductDbContext _prodDbContext;
        private readonly OrganizationDbContext _orgDbContext;

        public UnitOfWork(ProductDbContext prodDbContext, OrganizationDbContext orgDbContext)
        {
            _prodDbContext = _prodDbContext;
            _orgDbContext = _orgDbContext;
        }

        public IBaseRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
            where TKey : struct
        {
            if (typeof(TEntity) == typeof(Organization))
            {
                return new BaseRepository<TEntity,TKey>(_orgDbContext);
            }
            else if (typeof(TEntity) == typeof(Product))
            {
                return new BaseRepository<TEntity,TKey>(_prodDbContext);
            }
            throw new ArgumentException($"Entity type {typeof(TEntity)} is not supported.");
        }

        public async Task<int> SaveChangesAsync()
        {
            var result = await Task.WhenAll(
              _orgDbContext.SaveChangesAsync(),
              _prodDbContext.SaveChangesAsync()
            );

            return result.Sum();
        }
    }

}
