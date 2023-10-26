using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMultitenantArchitecture.Aplication.DTO
{
    public class UserResponseDTO
    {
        public string AccessToken { get; set; }

        public List<TenantDTO> Tenant { get; set; }
    }
}
