using CleanMultitenantArchitecture.Domain.Entities;
using CleanMultitenantArchitecture.Domain.IRepositories;
using CleanMultitenantArchitecture.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanMultitenantArchitecture.Infraestructure.Repositories
{
    public class UserRepository : BaseRepositoryOrganization<User,long>, IUserRepository
    {
        public UserRepository(OrganizationDbContext context) : base(context)
        {
        }

        public async Task<User> validateLoggingAsync(string email, string pwd)
        {
            var valid = await _context.Users.Include(t=> t.UserOrganizations)
                .ThenInclude(t=> t.Organization)
                .FirstOrDefaultAsync(t => t.Email == email &&
            t.Password == pwd);

            if(valid != null)
            {
                return valid;
            }
            return null;
        }
    }
}
