using CleanMultitenantArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMultitenantArchitecture.Domain.IRepositories
{
    public interface IUserRepository:IBaseRepository<User,long>
    {
        Task<User> validateLoggingAsync(string username, string pwd);

    }
}
