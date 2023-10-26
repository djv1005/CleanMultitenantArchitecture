using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMultitenantArchitecture.Domain.Entities
{
    public interface IMustHaveTenant
    {
        long TenantId { get; set; }
    }
}
