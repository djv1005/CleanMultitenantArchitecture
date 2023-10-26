using CleanMultitenantArchitecture.Domain.Entities;
using CleanMultitenantArchitecture.Domain.IRepositories;
using CleanMultitenantArchitecture.Infraestructure.Data;



namespace CleanMultitenantArchitecture.Infraestructure.Repositories
{
    public class OrganizationRepository : BaseRepositoryOrganization<Organization, long>, IOrganizationRepository
    {
        public OrganizationRepository(OrganizationDbContext context) : base(context)
        {
        }
    }
}
