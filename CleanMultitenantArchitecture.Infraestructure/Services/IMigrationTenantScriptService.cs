using CleanMultitenantArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMultitenantArchitecture.Infraestructure.Services
{
    public interface IMigrationTenantScriptService
    {

        void runScript(Organization organization);
    }
}
