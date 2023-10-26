using CleanMultitenantArchitecture.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMultitenantArchitecture.Infraestructure.Services
{
    public interface ITenantService
    {
        string TenantConnectionString { get; set; }

        public Task<bool> SetTenantConnectionString(long tenant,string connection);

        public void AddTenantConnectionString(string tenantName, string connectionString);
    }
}
