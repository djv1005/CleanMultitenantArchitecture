using CleanMultitenantArchitecture.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMultitenantArchitecture.Infraestructure.Services
{
    public class TenantService : ITenantService
    {
        private IOrganizationRepository _organizationRepository { get; set; }

        public TenantService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public string? TenantConnectionString { get; set; }

        public async Task<bool> SetTenantConnectionString(long tenant, string connection)
        {
            //
            var existTenat = await _organizationRepository.GetByIdAsync(tenant);
            if(existTenat != null)
            {
                TenantConnectionString = connection;
                return true;
            }
            else
            {
                throw new Exception("Invalid TenantId");
            }
        }

        //THIS IS TO MODIFY APPSETTING.JSON DINAMICALLY TO ADD CONNECTIONSTRING OF NEW TENANTS
        public void AddTenantConnectionString(string tenantName, string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
