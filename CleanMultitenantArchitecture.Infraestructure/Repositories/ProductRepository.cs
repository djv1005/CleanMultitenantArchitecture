using CleanMultitenantArchitecture.Domain.Entities;
using CleanMultitenantArchitecture.Domain.IRepositories;
using CleanMultitenantArchitecture.Infraestructure.Data;


namespace CleanMultitenantArchitecture.Infraestructure.Repositories
{
    // THE PROPER WAY IS IMPLEMENT IUNITOFWORK PATTERN
    public class ProductRepository : BaseRepositoryProduct<Product,long>, IProductRepository
    {
        public ProductRepository(ProductDbContext context) : base(context)
        {
        }
    }
}
